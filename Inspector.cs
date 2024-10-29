using UnityEngine;
using System.Collections;
using UnityEditor;

namespace A6Z6.Editor.UI
{
    /// <summary>
    /// This is a somwhat opinionated inspector that provides a header with a title and optional help button.
    ///
    /// Override <see cref="summary"/> to provide a summary at the top of the inspector.
    /// Override <see cref="supportUrl"/> to provide a URL for help. Will display a "Help" button in the header.
    /// Override <see cref="LooksLikeControls"/> to return true (default: false) if the inspector looks like a control panel.
    /// Override <see cref="LabelWidth"/> to set the label width for the inspector. Default: 200.
    /// Override <see cref="Title"/> to set the title for the inspector. Default: the name of the target type.
    /// Override <see cref="Icon()"/> to set the icon for the inspector.
    ///
    /// Layout:
    /// Override <see cref="Header"/> to add buttons to the header.
    /// Override <see cref="HeaderButtons"/> to add buttons to the header.
    /// Override <see cref="BeforeMainContent"/> to add content before the main content.
    /// Override <see cref="AfterMainContent"/> to add content after the main content.
    /// Override <see cref="FooterContent"/> to add a toolbar.
    /// </summary>
	public class Inspector : BaseInspector
    {
        // public static Texture2D DefaultIcon
        // {
        //     get
        //     {
        //         if (_defaultIcon == null)
        //         {
        //             _defaultIcon = new Texture2D(64, 64, TextureFormat.RGBA32, true);
        //             _defaultIcon.LoadImage(System.Convert.FromBase64String(k_IconBase64));
        //             _defaultIcon.Apply(true);
        //         }
        //         return _defaultIcon;
        //     }
        // }
        //
        // private static Texture2D _defaultIcon;
        //
        // public const string k_IconBase64 = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAAHdbkFIAAAABGdBTUEAALGPC/xhBQAAIC1JREFUeAG9Owd4VMXWM3Pv3U0vQIBQlCbFCBiQIgQlCCIgSAIEQkJCgIQOFvS3YlR8D30qGEpoSSAhlAQIAj5QQXhUiSAqBnlSRJokdFL37r13/nNm9y6bDSjPp2++L7ntzOlz5syZWUI8WueoDS09XjkeOed05OM5HJ/aD8kvMoEY3qQ/sLiKUsqDbLp4H1ClP4g3QYkFooN4mdZ66RXEkprKRaeug/PU5nHru8viK0Krev7CyHzf8rLy0oQe2aSkUiMak4gA+Cpwrtb19AR5GV3WItCbEBvgsMv0sirZZwqAIJshIabAKtsJG5OJKlEA4KSKaU8KgNZVz1MECLaTCapNX2QDgLe/G1sX3/1uEz0RKrnbCn65rhFcbHgfueQtN/l57VDxja1ssTIAAWqpRnFBweibwSpPg/uj+A6bHMJLveB6K6BKq/ds5+VGgI9Mquht/ZB99dLrrm+40EDo1NRUoSS8f3zg2li8snrlwH2VkYUPgatD9VfCM0R3/yptFb4jqF685oWlWvD60aN5oCpCpvb7pxWvrrY/8GMX4XcfWrrQ9eFuN1FP5vR5su8q0enhqDwg5KB0N3jx/p2HlvKXwzNdlEY8kdNjQJ9c1zMC3Re7jockbvzRRCSk3tA8ve7KZks6BdsMEqzqZObDWX0QILBC311L158Y/MRKgaTbwDW8tmrMDqzSWocmrn/MREI+AwQbGyyoRumddsv4yx2y1iFQUkQ2j4l0IOnZf00JvmszLF/ACw7qlxEy+OJkl9kzm6bzWroUOvubpKFvPJzBs/YmUKBKRj620gjWhMlIHZsRgYgEArSV2T4Lmcf97JU+IcQX0IIYzmECLgLeqGcFVmgh+B48UzQXVXwq8pmzsEJmEzrdms52hi3wKymjb5ZJbMY571pWC7lpe7UomT7bOYvrtet5zdva34Z9qiHAF+5tG3BTrjBSbmEk4czE34R17+e6vxfb3xXr031W8UpvUr+USBsrrKz5D/nD7jgCayCY3HVFCbB8qUymbdd9GU+Ri0ei8o0yhZIqK+v4y8oh37hYhBsXgvfbZXYoo/xwuQ9r8+GBpOMIFPVELq+0sAvbtsY2wudWsRt63WJ8x8WV0QwDGb4TbRnY3ZQXr9M7ZfHk7tmXzO+9+q3iXQetdcE0SNgQ4Ttmo0AgOFh530JeYZFIhVXef4sa3Wb+kEyndl3Ru1ymX4AoxOZjbWfxJf8+Y+c2fC63KCfLZdLianYUFY7krXOScnI8tapaN7x/t8vSej6a/r63Ti7Ac6WlSp1VXqrv9NI4gXeHz66KesBLd3ikQOAF7pnXbHEgArz4YwqVSskli8bDFxxIaKSoclOLZgzyNvg/9mwZQb0gfrePzt/iDbDYBAIrPnQMLrM6/Ryxv/PtWPpKh0w1QNY65u5OoBZdJ8OG5QERveN3G4Y9LfqYCJDtmPwYHa8rm6a/4AVXbL5MivhoX8I/8d5L5QVwKQPKh/EZYbEJJX7jP5eXyQ6X7Vc8ha5oks7LwIUrrexoKaNtK63yjFJZOpV+YNTGIU/kXgSfCC0H+L0gklMEgzx2fRpFXWyuN4+jKJNPjKcWTse99f04+v7Xoz/00TWSAtEdxLv42baRoAs3HWDHH3w/fgmvA4unAiJOVjdZkOrjHLKzINRZdNJ/yf5EQWR4zxzuYxjPukQQwsC/b/3m8vb9Gsif7yrW+l6eSnNQFLD7xJMT6DsPZw2G+5alEnlv4cHRLg82+7quJ2qlBfyrVprgr6BRemf8kDcsT3rvwaUOnl2Qjpu7YtpZJ20TKHYgKlO3BgTGn4y/5dFXPN4VwZ2A3d/17r9mHUzVQ1Qwg11ixDvYGrAv85lSd5h7ub9nBib1zPO7xW2lmB9ojI4u2BG/wp1Az9Sd8vXvL9tVRomm0I9PrB4qjOQOc6f732Tg1Q6ZcwHhdDsgDfa5X0ndFakhktheK79UCYmE9zdtEvkQNPA2Eg71Ztb8/Bj4BKFn+IbXbNSYhdoJeqCVtSg1TLzHb+6tGgN5YXmWYvt1GyY5mkRf/78fkt99s0PmP6oImYEEAv2aKNj5jHbObsdESGJzmIWl3TL4z2gKLsvddmyJOdAxZbOP/UplucAjs8matVZmpXa9EmE0WXqhNPOZj0wmXAzg7HxLJ8UCSJJ4nYeDFBzeCPhW+8ykKsYzESEyghrRZXns0r3xmSaigU+umqVS8hr2V0FqTaZnviqIaWYG3max61+pYuRvoj/gubk8StB2MYCz+w2VFw8/N0m8W9Ri8XwgOBmJ2RlJfLkoOdskhteUjosVYvVSTaZaRp6SIMFyxEj43rPnTtkWUCJ8Aplq1NDPa+u8/rb7Etd3t+l0b/HKaEFHpJEmYowEZptwcvyUJc0XTZ52agJN67IyAMchMoMEUf3WBiG+qZsHugRI8skLgQhRYkqoSZcW7NoUSyOeXlsiy/QsEkfc3uBF1Lg9pF0M+EEeweHD5tAFu0HiHkDkuuqM2dMOijFMP2izhAfXCfK/eqM013aupPyljpnErjAbeJdVvVlJ6gZKgVcr+E2Fsqi8L2I3IkEfiHlVhD/YFRIrZN5Q9dfp7anUkYojoB/8aTAPPHV56mP4jC2/0QKeC9MdSmyTWVc7MnSZkJlFY5/B76mPrOhcZdMPzjky5rYmuq8gMqV+IyJzr9kYD1Y1nVAqHd79zxGPYJ+u0Wu789sKcExI+AEbTHdkb3DaDXOCxqAcd3YSHf3zRJryU0ohziHUdq3049aLTyGMNwRrc155pWt2F8jCOT6DKQPX7IyrBbGC+nBy2cvuMG2fp1ZHKTZjrzmdIk1wD0fzhQQPp5GI69OC9jdYcB/GY/SJfzaY1x0h0JvxuY61lv/04+Obp7dZ1kGx2Q4isnfbZf74twOjCucWjhYziUWnF7APMmlVeR3A2xFSlB1fbIst8DNYBApqtts+QMqIrjHRCYj9AgB0T/DHvPeFqfu3hZIfVELD0CdMN510PPlwestlXSRuHJxeNKbNa3SMwInznMS1gumds8izPXLu9yL0CljwbN7O+CcQAKb29txEgkREL7d/p70/6lzFyUG0O3p0p9JnXTCfwmSrMmmUKvEcjHAw3ufYKX9OldlZiAH3gUMWgUOGqRYpatbhJOGEkB7ByCAhIoRL9GTOrlEtzdiAZF3I3Xhw3aIKjwSmXVYprw0E9wFD3Vk95h9ZNFnk3nlN0ztXArOJbpnv+zBS7JKUrTKegEwasqXz7K/jv3Yh9bj5TQY8YMm+4LQBIOkW1I4m8bq6rDStIPzg8POTaFazRXtByu7w7eqU4ykh7lJ64nF//o8YcO+Iy8ouC+tUAUEFVD5g6C8TRPblDvOX3/d4Ju9Bc8j+UWJ/SAMDBuQGl3F2DU2BdobIufzbDTFJf4QJVxy4l84o7TNP5t4y7PSaldDt+zePoFaF3C8ZfHTb6DweNmyDWDneCy4T5p41MPqxlRlQZxgD06z+UI84S2oqdRvNhIRH50+F0ZKGGqnd0PA5MCem0iTyW9ffZeCFiOyIcp3vEbOczOxARAEm8tZvjx9uIu45OO/hcs6PCJMwWgSTThgkNGdP50Y3+b3RcFcGsOxRrJZVqBTsLJNp879KmocE4/vmhML0dxGnZq4oT5UTfRsypyvSm/s+iXkbYcKG5VmqGLMhQ5okvX0xZ/Cb+P5OrQYDaOfZ7TLO2DCygTohAT02+9Doh9wlQZhhvXINJIBTLGFG4Nat1dNuhGkeu8HAZETgsfIHry6LdhVJTGaqMbCw9ZK3oKAwExGX00ArCSlntptGJT7rjD0/p3D0nHER2dNA4o8RsR5i9bHdtO+2UfqIJrPi7VuGhyKjHaLz5oEAUyBU84eHDlEObCr4u43RFyGIkfCmQcquVEdyi0y4GMiCdbJIv7xY+OSilG9NDvH6SucVj6qGsR+lRXUbCuu+dPeo/SZMKqTk+7/6VaRfplYgHLf8dv3QEyYMaqRO4ie6XSLUu7a3X/EHfUWBxTUMFfBpGE5EsvEjC9osm2R2xGWRbNf2yJBFKJz8JAMM1fheWKyKciHCXT1YKcnQH785rjDrGczHxNEx5ZByX9wGQzYMijDSLVWU1fC7SwOrGy/gkDI/DFJ+CslbQ5QEpPhSZUYvsOHNmd+OCUb1oiSTu624DpoIBJidANPQRnlLkOzkJ5/HiZmu56DVqZWEvSk0KpOjYKK2wlEtrC3gPip7KyEXlwy8ggy4NICcW3ViH3tyfCMpkPgKTnW9F9RA9ZlRY2qZTojXhQdGB8ECIV/WeaSk6S0Vwt/a9EX8AyYMJKOpFk6eR61JOmkL9387A5XOcznRPyAd9+bSABbcVFkKizk74RgCLG6+CEOtDJz7C23I7PPXvxvbN7VTbrNbRD2FElEL7VJOWX1wrk/w2R7kU0fTynyr7MovTu+/oEqk4aGNw110GkPJ0NvfGvKTUwOujAgldm/oE5zRH2Fq7fJRmyXZhs5HvQVVQNVeRRSJpn/0dZLLT6AfHdlz5RfGjYorhgRFZ2YQLyXAd/PmgRWYDXvidX92MwEnTNWKChotzEUAZEhxJmDP/5iSoHA+HtXncFYy8eWI3GATUUqPnM9k3eiNffBv6+dxFInjd3Bc0TrGbq4TNiSPg0kJcVtDuzEgkL/AdGNkfqOFCBgkOdcFiAEXhYjc4iXXwSspr7r2QqfMW1MhE2aa8SR8z1GsvJ74hh2cDUdWxMC1ZVJFxWWF0089v99mAIcZk7c98+sUCiVnkJbDkCGds9tl+yIucCaQnpNXCxOvQvGdejMWI+vEH96Vp+9PYBl7EhIQUEjoJP5039W5SBDsbOkf/qP0LdSX8NnfTQW3GYAPFuJYQQ86P2mJTPh1BKa3yssymi26CB7vjdKY7c3DSflITCZyR9P7IbEWWhrVK7vXUKgSS6BNNMGuzcNh9nSsGz1HgYsBCdJpJ31Bwxl4CitqMwswEirp+lzTniYTyKDJNL6zyHogviN2ssNKScon2+NE4DHh8Yp43ZuLAUQOjlS0q+78PgIQHA5yfjL+8Hg7Zr0Wyt5BDcxvuZjPa5vV1gHDCe4QYHCa0Wn5RV6FkZKQ7N2jWO6X8UtNGLxiDhnZf00VMuiPL5zNxYCQhoKTqPrnu2qncUXnPmBjVwPE53EEKDp5h6j27z+E9Bv7cDvJfyM805AMI1SWSDsIt2IVZXZEhgb3XrWucH9LXRJCwj+R1DsgXAygBhijL0XcmE4hyBeCSawQwdqidAiqQLES1Tf+1PiZU44nM8Wg54TWNKOtlSkPvnd4DJWoVOzuhKMfz+mDTIL5hvjKcuj2rSO83L8jXlcgQvWaGy49rk7rsqdW2jWu8+Bt9RcYmxqmz7ETfhyRYXM63X1zWy/hukVq/eo3Cf/G97jCLgWJU6GgdamqohRX0zLMKat3jnJFQhxJYqMHO0C7rQHktNIIc7xGb0aHIYWKTFpBvH8OYsJikwETxp1p8x3C3CovL4W4UJixL6GaE6I27+qEMqFvS4aeV+T9ET8E5RcExL/eF6b8NAAquBAJ1yNTaxov5FjMQoLoExZR9yBkdtvMj2iZWowSBvg2UeZ9NboLwoBj44XE9cqehVkU4ti0KfaqeAn/XKrBF8jhCe+552AiaYiTCUzPhZ1vTBOIPqs3PwWynMWawjBda6IxdgyWYg/CnuJseH4Z0y5Vpsl2SpemQqAyCUx6dAWH76Kso1tZu9zt8a4tPoRxmQAf0LYtq55rVCvIB6Zj+KgbnQ8GLxiP30QoBheIPjepaX3m7c0M40GHg/GXFcJef/FYMrUQyybTTItBiy88kgXBCCIsJbOz9yRQT+KCJv67WzviN6enRulOTMU0ia2HxGPIk1emuaTD3QruTVqP+vck4YT/aJddV9dtxaCNI6DBcLhe+Puh0Y1dkfIOhKppwPN7eNlzu7A+ANLnwjgfgkOIu+11eg4pq11NQscELYQ3kur6zj6c1Oi3iCM9lzSexD2f0T/21E4rhTTLF+y+9alLU/uvg1nTUEhrVVLKK7l+DlMwm8QinzuevMuz/92e75kBE8HBhvNr31L5FWfG46icYv7ISC4Us+JNuHu9/scMmIi/qD8/xsbJWptMy6PPT/L/PVWb/Tyvf5gBT0T3+oym7D04v42NGwVQNG2pw9LPoOwIscrDDq0ZfPqPCnKv9D3h/icKSElZrFz8xfc5O2Hv6UDRAF/VHNfZBmwCgCIm6vDOAGWAQrgBNfI2xFgCWw9u07En63/O81+iALTy2D6rmlYYZJ1GeLgQFi3N6AVqZVGbNg0/5Glp7PNo1LrHAX4dVEtrg1KEogwoDmoyGXEyd8j5P0fk6lj+NAXgCvbLi7ZxKufpBiMULW1aG6x7ljHjqTVfjDruKXh1dhx525bvWsfolK0EJUjCKyBYC3yM6pAyJSa0jFrtWR/yxHOvz/+VAl7qsrSRQZQ1doN3R7cWAlNyFUoyQ5fuif9XUuRy2GKW34b3L6Ivo5s7lMJe6NA9NC3VWaQYOHBVnXKdZUIkHeiCg41jWOINP7xuyBYUpm3Mhv4aNdbC7oCvg5bwqK1eFq/Ek1n9L9+rwJ5w/5EC8FQU27h8hMaN5TB2FWEVdG2J5fnVkZNTPUpE7sTQxeMjc7vArL5B4zzUVJhDaQ7FcEn61If5jIYlpSgbuPd3v2+dsKG2puqZdkIHiSCK8QMUxpk04tLyQZt/z8vccf2uAjLDM0NuVGkrwDL9TGJgyXIg+IFG6KsgACgCtjBgFx8W8mukQJ8J722PuelOBO9TeucFVtrVhTDGR5qeYArvDIjgHYCI8ZQgXznrbgHwoRGrG3MdvI7wbqI/CA81vKuwbAgAfoRRULloFM6N8TeWR93w5MX9+Y4KyGq2tL+dGet0wr2FpaCHzqRNClHGTDme6EplTUSp3TMa2CrpKlDS46ZwXGLXIY97AzzlHRA6GJkVDEtsD5Wssdlfxlww+6N3DOy3ppfBeT6M+2Adxrxz7P8LzsmNVgmLgPfLb8cEEJAa2UEBXlM8t+rvj18fWsWkXOA9EoeT4J+Rm2CfgaXLBu8xaZrXGgrIa5EZUmm3lYCFnWMW3JPQbGs9acrYfWPdahomitvXGVBDkGV9sUZ5nMPFHa6NgsPgWePnR8Z98HmC28Gr233d7/r3X/UU7PBtRRygRFAcVMjgSijsRdW/Hn54yXgIBXdvjcZuq6WrlRmggMFIGz0Mr9SX1yldEF3NgPC6esPN80o7LxYWkEgpCO+PnfHPaZWrBpOipx8bK7T5QfuszjaDF+gMxjUwKdxZokeszDr0zcKRP7/YJaeJSg20bEeHUkAZEvsViujRWTvjDuJ4xRV70e4Wz0DkXw3Ks4LXOJRP2EbdakwwdLmLRvU1gF/sNKKXcUo3c29tzOHVI8WpzbARG5/SiLEWvM3f9EJUGgzLUhgu/kjb2yq7ypKm1DUUgJv3N2HzHhgmspWFxZyccIxD8MtYtWSUpvMMQCSZCjFjAsIaMnupjqLNxTKGidzzmgob+cXq2WmwRPpQ4DBd3SkwxBGbxnh8ux4nN5iFHE8c/Yblhdy08SwQdIBJXxgHBBQ4YUuZyyQurtUPBYjjvsRPuts0fS8qQPGuqQBXUcCdEC7i8GQSdx71oI49mRUAg39kUYslN8BwgQxgQCkCVteN968Z0tS/t10e9fL3id94RmIc5zO6Lo+EpfdM4c2Ah4GpwXPAwymsZ9FVDagn0ff+vaflTwB/1B0H9u87IK9HVaW+AXDURj6QPtAF+qBBSo98s25YB3yPLVX8h4WsBn4BEVKHcs2d2h0VgLUAXMojgTs1KFgZBNaG8K/wWsNL3YMvNZhIdZ4GwauxTu2H/tY2g7zddhm4OnnXoFIo7CmP+b+OWYJRhXJdZnTM+cY+2e6RPhW2dn4uqXrNrvOZEAy/i+m1kkTDH3jWHp3zHgP6rsIVO2RGwJcEeiN8bM8OsTmff5N/iRpGCIcM+k68ooDQn+CxQde+gBsgqK56wyGglWrF6DIG1B2gVhQ96PTEn9ytsbxJ+jWYeoLhUHNhyokJomaBWNBKaeHLW1TajU0G5a0hEiMOsCwU0Cgrhw31LrMLE4+543Knjv2Te63qYDP0AnDvxmJoAR94hbhx2PCSozdvjjnr3uexAWtKIHUOATqHv/ok5hHzG+J6KCa/vU4lWHQZTXC4KIG3C/MmXA0F4EnnckMrdoxRcB2ANO8hUXk2pFmrBRd/Pl4CwS4YZorCpNMTXQowkc4JW5pi52SxGKMSWQPlsRGoUIFLKBYFoi/d0NW51rqNGb9e8gbAviaiNSARsMAZhx1JwANpMdwzKWr9jpHiFIJJB6+94IgwxIMQ+H64vjfpdlY1XtYoewv7iJkMYHAmwXsqeYX8tLp6knVHJ8fCDpY7vIgcNujXycxCaXcogRVDfW7u5ZPH7FD2CHaUPrBaUrNB+VRUqbFMGlQrIPnNo+No6rdjmLckt4ey2WksOFG78X4gUVTl8iWo15LXoJINmwn8qJ8st8zaM4rlQBHJT+FTsfSG8OD1NQihlWG4yqKqaugdf63QbTBE3lK4cQbqieHfrRsGNKUIwCtwVNuZcGK7cwwABaBrwG6LWQXFPcH62CcPTjrLN/kVCFZWCCzhOfdDWYYyDmN1nG940Ao8b4ZaRSVCqVecM8J+Trf/Hm6b4/MbHbJ6aAbfLQKiwtvNPZDkKtel0TgEEQ3jEY5fCJXiGWeSIuniZNiXmgvn1QgUY0RsgdPBFxqG1G+xfHkknLVzNMT9SPR6YUxMMl37hiYAXGt4AJZ30fqo9Wr7BfgILQbOq4DWKnCmAOaKLFzuA3sZt8A7MsoPXdOWNVvEJbu+GK2CXnK3ZoW9bNPTFLvsnG88oJ37HagEpvOC+Mdy+HF+3g5Hz+ZC3bzYi7LuCqHXRemR00vuwpuYZIgggs5deKmhAOyIwqP7MhtPwiqxicy8Oit/4LK0fPjZlO1xZyYG4ZH1IG4NVijNF8KDgtB9iXb9yrxWy4ZhsmP2F1fIFpAxrCR7Kho3gZ99dMUYoHPJwQtMZ8IoPKMWtfrDESiavyOu/rrPY/eDe2vC25wVaHca/eD3FbJGkwQd6O++LWLC1RgCeIBMhwkOpxzYFp6hnqqasSc4DRYX5AiT5KERlyb+/FnofKdbCjcxcZGoM0m48IjJabo4BULQYh2EgzneqjM9r/aqUDK31RKM5psUP2VMOZTU0bURRgZ/frdjTmgFsefCSjFSPw1nyIC+y71xJtBpVAYcpHYRc95IcJgNmIW5HoYJxITB/bHcphVAxGhZRW7AzgjIAd9huFXbljHxVLeK861D67CLIbMwi4+XL7h4OmxOhBt27dTOOvMMWSPBZnAyEblfxVYOCga8BXvV8p8Eh7plL+964K7bmKYPst+yXZFU20H0MrQsMYzjmt12EWhEWgjL9/OnwXMOJVGLItVD90cLuu+ouNOCoeA4YgCn86J75xpcVYtgO7El8DfbiwR5wZGBCFMe937mfQ0PwA/CpUDrGHbCLo7H7V7ck56EGt4XurAnrMV3QKZIwes6b607D2p4bA/8xQ48P1Gs8FCrKBh4ENw52sTvE0rgrh8+YWqdtnpZKgSmN3TIXySFPfvid2PTPPMDjEdloET0SMYcuJCHST1WNVW5vg6MG67jUAML6xIcMOG014YdI792x9Ov31o4lerwEsdvvJCD262GB/jB9p0jCNbUOiKOuDR5J1jyBmoVtHxONmguuGEPCHznN9efzwsaLFDBAs+Z4+42qdt3mFrDMf8tJgwMlG3uTN+GBGOAlpGWpJFZ0zstN6Z1XWEwTTsFfcPBeulWTioxJoEnHM//Mq7QE48CG7YOOtW3Rk0aNTzAJsHGg60SVn4Qv8rtRT/6zF1vVyzJ7W5Oum52ckR3A7TOfo28MjUe3sejZbaHpve3GcZaGH+tHfk5WK3sah4ssRNjTo6pVrbCyIoOgmPT3Jg08eN1VvsVDbVydTVaF9Z0xNBJGHjdVXCHof/4OvFfpqAp3bKHWmChh2sS9xb7xKp6cLwrW7cT8WM7zN0DAoJrzDbo5XdsJb4L6pfqthzA2xunYPyDAFYKQg+FQIXuB8tkWtjNuX3njmRr6PwUyP4Wu5al8FEUORirhHfDR50evyW7VUYnm64dxOwPxkDrkthzJ4LzG0G5jSyH5auCmZtYfmMAAxg4EBX1d+d5VHdakx9dAZkgpsLkOGSXmE2uAhxWkYVi1gklOMCfnP9ZzDX3fuZ9jSFgfqhbPvlS86rn+7S0PU+PDmwkWxhMJ7rhw+waHgvxR9eEKazx3joZNWYXESDBIDAUSK0g4n8s+bIkExYLcUGWNWPTyiaLIFjZD6InYaxgmv14SG6oDic+ciXOVSujT79ydByzUos4+YEubIU9cs+Gx1UgSAbgd8DVmmm4iQs4GRvWCs7Ur8XpcnvckLsJj/hQ//9xOxY0/367quaBNTubRRDwjsu6zKO7l0zbt7P+gmRYpgoPsIbcPuxrElrdKqOBblPXgH56iHyd0U8VxTsx0aPchtudBmx3ikWRQqNePzL2k7ceyewDP0EUHijWDGhlRrer3nzUwl1Jrl+CmbR+7/qHFOCOdGfPVDnwm6BJUDn6GFd9wvUAq3B/AJTqS67Tzu798uCHQlD8OCjs6rbH6w6TDgqosFfBwgyiPOJ0XiFBgKPmbNzx5r457ktq9773ev9fK8CdEAbCwpAFD4D1N8BSOAyZdlrprC7TqL4XJh8xgxf+UkolxkHhQTJscv/s2ORGHEtaLekO3gE4cGyD8Ghlyr8iVjZixpFxv7jT/G/v/1QFeDKDP/0NLan9IhQ5ZglXBwsKgSTyNqxvT0CUzhHBVWbRdkoeh+E7HYMleoWAI2Tapcat0s2fMHni/zOe/1IFuDOIlt1Zb147jbANMK03Q8ExujsFdXoKKaKyHD38VPIJ01PccfwV9/8zBXgyv7NJlpetqnIs7D/YKoPrZscUOX6E5gn3Vz//P+75e7bTH0WnAAAAAElFTkSuQmCC";
        //
        // public override Texture2D Icon()
        // {
        //      return DefaultIcon;
        // }

        public virtual string summary
        {
            get
            {
                return "";
            }
        }

        public virtual string supportUrl
        {
            get
            {
                return null;
            }
        }

        public override bool LooksLikeControls
        {
            get
            {
                return false;
            }
        }

        public override float LabelWidth
        {
            get
            {
                return 200;
            }
        }

        public override string Title()
        {
            return ObjectNames.NicifyVariableName(target.GetType().Name);
        }

        public override void HeaderButtons()
        {
            if (!string.IsNullOrEmpty(supportUrl))
            {
                UI.Button("Help", delegate() { Application.OpenURL(supportUrl); });
            }
        }

        public override void BeforeMainContent()
        {
            if (!string.IsNullOrEmpty(summary))
            {
                EditorGUILayout.HelpBox(summary, MessageType.None);
            }
            base.BeforeMainContent();
        }
    }
}
