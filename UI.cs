using UnityEditor;
using UnityEngine;

namespace A6Z6.Editor.UI
{
    /// <summary>
    /// Many, many, many UI shortcuts and helpers.
    /// Relies on <see cref="System.Action"/> to create layout containers.
    /// </summary>
    public static class UI
    {
        private static GUIStyle _cachedBoxStyle = null;
        public static GUIStyle BoxStyle
        {
            get
            {
                if (_cachedBoxStyle == null)
                {
                    _cachedBoxStyle = EditorStyles.objectFieldThumb;
                    _cachedBoxStyle.stretchHeight = false;
                }
                return _cachedBoxStyle;
            }
        }

        private static GUIStyle _cachedPanelStyle = null;
        public static GUIStyle PanelStyle
        {
            get
            {
                if (_cachedPanelStyle == null)
                {
                    _cachedPanelStyle = EditorStyles.helpBox;
                    _cachedPanelStyle.stretchHeight = false;
                }
                return _cachedPanelStyle;
            }
        }

        public static void Disable()
        {
            SetState(false);
        }

        public static void Enable()
        {
            SetState(true);
        }

        public static void SetState(bool state)
        {
            EditorGUI.BeginDisabledGroup(!state);
            GUI.enabled = state;
            GUI.color = state ? Color.white : Alpha(0.7f);
        }

        public static Color Alpha(float alpha)
        {
            return Alpha(alpha, Color.white);
        }

        public static Color Alpha(float alpha, Color color)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static GUILayoutOption fixedHeight16 = GUILayout.Height(16f);
        public static GUILayoutOption fixedHeight32 = GUILayout.Height(32f);
        public static GUILayoutOption fixedHeight128 = GUILayout.Height(128f);
        public static GUILayoutOption fixedHeight192 = GUILayout.Height(192f);
        public static GUILayoutOption fixedHeight256 = GUILayout.Height(256f);
        public static GUILayoutOption fixedHeight64 = GUILayout.Height(64f);
        public static GUILayoutOption fixedWidth32 = GUILayout.Width(32f);
        public static GUILayoutOption fixedWidth64 = GUILayout.Width(64f);
        public static GUILayoutOption fixedWidth160 = GUILayout.Width(160f);
        public static GUILayoutOption fixedWidth128 = GUILayout.Width(128f);
        public static GUILayoutOption fixedWidth192 = GUILayout.Width(192f);
        public static GUILayoutOption fixedWidth256 = GUILayout.Width(256f);
        public static GUILayoutOption fixedWidth400 = GUILayout.Width(400f);
        public static Color orangeColor = Color.Lerp(Color.red, Color.yellow, 0.5f);

        public const float k_InspectorInset = 5;

        public static void BeginInspectorBoxWithHeader(UnityEditor.Editor inspector, Texture2D icon = null, string title = null, System.Action content = null, System.Action headerButtons = null)
        {
            if (_boxStyle == null)
            {
                _boxStyle = new GUIStyle("Box");
                //_boxStyle.normal.background = AppFramework.InspectorBG;
                _boxStyle.overflow = new RectOffset(3, 4, 4, 4);
                _boxStyle.border = new RectOffset(9, 2, 2, 2);
            }
            Header(inspector, icon, title, content, headerButtons);

            GUILayout.BeginHorizontal();
            GUILayout.Space(k_InspectorInset);
            GUILayout.BeginVertical();
            GUILayout.Space(k_InspectorInset);
            GUILayout.BeginVertical();
        }

        public static void BeginInspectorBox(UnityEditor.Editor inspector)
        {
            try
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(k_InspectorInset);
                GUILayout.BeginVertical();
                GUILayout.Space(k_InspectorInset);
                GUILayout.BeginVertical();
            }
            catch (System.InvalidOperationException e)
            {
                Debug.Log(e.ToString());
            }

        }

        public static void EndInspectorBox()
        {
            try
            {
                Space(12);
                GUILayout.EndVertical();
                GUILayout.Space(k_InspectorInset);
                GUILayout.EndVertical();
                GUILayout.Space(k_InspectorInset);
                GUILayout.EndHorizontal();
            }
            catch (System.InvalidOperationException e)
            {
                Debug.Log(e.ToString());
            }

        }

        public static void HorizontalBox(System.Action content, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(BoxStyle, options);
            content();
            GUILayout.EndHorizontal();
        }

        public static void HorizontalLeftAlignBox(System.Action content)
        {
            GUILayout.BeginHorizontal(BoxStyle);
            content();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public static void HorizontalRightAlignBox(System.Action content)
        {
            GUILayout.BeginHorizontal(BoxStyle);
            GUILayout.FlexibleSpace();
            content();
            GUILayout.EndHorizontal();
        }

        public static void HorizontalGroup(System.Action content, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
            content();
            GUILayout.EndHorizontal();
        }

        public static void HorizontalGroupLeftAlign(System.Action content)
        {
            GUILayout.BeginHorizontal();
            content();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public static void HorizontalGroupRightAlign(System.Action content)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            content();
            GUILayout.EndHorizontal();
        }

        public static void VerticalBox(System.Action content, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(BoxStyle, options);
            content();
            GUILayout.EndVertical();
        }

        public static void VerticalPanel(System.Action content, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(PanelStyle, options);
            content();
            GUILayout.EndVertical();
        }

        public static void VerticalGroup(System.Action content, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(options);
            content();
            GUILayout.EndVertical();
        }

        public static void Line()
        {
            GUI.DrawTexture(GUILayoutUtility.GetRect(512f, 2f), EditorGUIUtility.whiteTexture, ScaleMode.StretchToFill);
            EditorGUILayout.Space();
        }

        public static void Box(float height)
        {
            Rect pos = GUILayoutUtility.GetRect(32f, 512f, 2f, 2f);
            pos.yMin += pos.height - 1;
            pos.height = height + 2;
            pos.xMin -= 1;
            pos.xMax += 1;

            GUI.Box(pos, string.Empty, "Toolbar");
        }

        public static void Box(Rect pos)
        {
            GUI.Box(pos, string.Empty);
        }

        public static void Box(float height, string text)
        {
            Rect pos = GUILayoutUtility.GetRect(32f, 512f, 2f, 2f);
            pos.yMin += pos.height - 1;
            pos.height = height + 2;
            pos.xMin -= 1;
            pos.xMax += 1;

            GUI.Box(pos, string.Empty, "Toolbar");
            GUILayout.Label(text);
        }

        public static bool FoldoutBox(bool state, float height, string text)
        {
            Rect pos = GUILayoutUtility.GetRect(32f, 512f, 2f, 2f);
            pos.yMin += pos.height - 1;
            pos.height = height + 2;
            pos.xMin -= 1;
            pos.xMax += 1;

            GUI.Box(pos, string.Empty, "Toolbar");
            return EditorGUILayout.Foldout(state, text);
        }


        #region Button

        public static void ButtonStyle(GUIStyle style = null)
        {
            _ButtonStyle = style == null ? EditorStyles.toolbarButton : style;
        }

        private static GUIStyle _ButtonStyle = EditorStyles.toolbarButton;

        public static bool Button(object text)
        {
            return text is GUIContent ? GUILayout.Button((GUIContent)text, _ButtonStyle) : GUILayout.Button(text.ToString(), _ButtonStyle);
        }

        public static void Button(object text, System.Action action)
        {
            if (_ButtonStyle == null)
            {
                if (text is GUIContent ? GUILayout.Button((GUIContent)text) : GUILayout.Button(text.ToString()))
                {
                    action();
                }
            }
            else
            {
                if (text is GUIContent ? GUILayout.Button((GUIContent)text, _ButtonStyle) : GUILayout.Button(text.ToString(), _ButtonStyle))
                {
                    action();
                }
            }
        }

        public static void Button(object text, System.Action action, params GUILayoutOption[] options)
        {
            Button(text, action, true && GUI.enabled, options);
        }

        public static void Button(object text, System.Action action, bool enabled, params GUILayoutOption[] options)
        {
            bool originalEnabled = GUI.enabled;
            GUI.enabled = enabled && originalEnabled;
            if (_ButtonStyle == null)
            {
                if (text is GUIContent ? GUILayout.Button((GUIContent)text, options) : GUILayout.Button(text.ToString(), options))
                {
                    action();
                }
            }
            else
            {
                if (text is GUIContent ? GUILayout.Button((GUIContent)text, _ButtonStyle, options) : GUILayout.Button(text.ToString(), _ButtonStyle, options))
                {
                    action();
                }
            }
            GUI.enabled = originalEnabled;
        }

        public static void Button(object text, System.Action action, bool enabled, bool active, Color activeColor, params GUILayoutOption[] options)
        {
            bool originalEnabled = GUI.enabled;
            GUI.enabled = enabled && originalEnabled;
            Color c = GUI.backgroundColor;
            GUI.backgroundColor = active ? activeColor : c;
            if (_ButtonStyle == null)
            {
                if (text is GUIContent ? GUILayout.Button((GUIContent)text, options) : GUILayout.Button(text.ToString(), options))
                {
                    action();
                }
            }
            else
            {
                if (text is GUIContent ? GUILayout.Button((GUIContent)text, _ButtonStyle, options) : GUILayout.Button(text.ToString(), _ButtonStyle, options))
                {
                    action();
                }
            }
            GUI.backgroundColor = c;
            GUI.enabled = originalEnabled;
        }

        public static void MiniButton(object text, System.Action action, params GUILayoutOption[] options)
        {
            ButtonStyle(EditorStyles.miniButton);
            Button(text, action, true, false, Color.white, options);
            ButtonStyle(null);
        }

        public static void MiniButton(object text, System.Action action, bool enabled, bool active, Color activeColor, params GUILayoutOption[] options)
        {
            ButtonStyle(EditorStyles.miniButton);
            Button(text, action, enabled, active, activeColor, options);
            ButtonStyle(null);
        }

        public static void Button(object text, System.Action action, bool enabled)
        {
            bool originalEnabled = GUI.enabled;
            GUI.enabled = enabled && originalEnabled;
            Button(text, action);
            GUI.enabled = originalEnabled;
        }

        public static void ButtonAndText(string button, string text, System.Action action, bool enabled)
        {
            GUILayout.BeginHorizontal();
            Button(button, action, enabled, GUILayout.Width(128));
            GUILayout.Space(10);
            GUILayout.Label(text, EditorStyles.wordWrappedMiniLabel);
            GUILayout.Space(4);
            GUILayout.EndHorizontal();
        }

        public static void ButtonAndText(string button, string text, System.Action action)
        {
            ButtonAndText(button, text, action, true);
        }

        public static bool ButtonAndText(string button, string text)
        {
            bool result = false;
            if (GUILayout.Button(button, "ToolbarButton", GUILayout.Width(128)))
                result = true;
            GUILayout.Space(-4);
            GUILayout.Label(text, EditorStyles.wordWrappedMiniLabel);
            GUILayout.Space(4);
            return result;
        }

        public static void TextAndButton(string button, string text, System.Action action, bool enabled)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(text, EditorStyles.wordWrappedMiniLabel);
            GUILayout.Space(10);
            Button(button, action, enabled, GUILayout.Width(128));
            GUILayout.Space(4);
            GUILayout.EndHorizontal();
        }

        public static void TextAndButton(string button, string text, System.Action action)
        {
            TextAndButton(button, text, action, true);
        }


        public static bool ButtonWithHelp(string buttonStr, string helpStr)
        {
            bool result = false;
            GUILayout.BeginHorizontal(GUILayout.MaxHeight(32));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            result = GUILayout.Button(buttonStr, EditorStyles.toolbarButton, UI.fixedWidth192);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label(helpStr, EditorStyles.wordWrappedMiniLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            return result;
        }

        public static bool SmallButtonWithHelp(string buttonStr, string helpStr)
        {
            bool result = false;
            EditorGUILayout.BeginHorizontal();
            result = GUILayout.Button(buttonStr, EditorStyles.toolbarButton, UI.fixedWidth192, UI.fixedHeight16);
            GUILayout.Space(16);
            GUILayout.Label(helpStr, EditorStyles.miniLabel);
            EditorGUILayout.EndHorizontal();
            //GUILayout.Space(4);
            return result;
        }

        public static void ButtonWithHelp_inactive(string buttonStr, string helpStr)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Box(buttonStr, "ToolbarButton", UI.fixedWidth192);
            GUILayout.Space(16);
            GUILayout.Label(helpStr);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(4);
        }

        public static void MiniButton(string text, System.Action action, float width = 64)
        {
            ButtonStyle(EditorStyles.miniButton);
            Button(text, action, GUILayout.Width(width));
            ButtonStyle(null);
        }

        #endregion


        public static void LabelButton(object title, object content, System.Action action)
        {
            EditorStyles.label.wordWrap = true;
            EditorGUILayout.LabelField(title.ToString(), content == null ? "null" : content.ToString());
            Rect r = GUILayoutUtility.GetLastRect();
            r.x += r.width - 20;
            r.width = 20;
            r.height = 14;
            if (GUI.Button(r, ">", EditorStyles.miniButton))
            {
                action();
            }
        }

        public static void Label(object title, object content)
        {
            EditorStyles.label.wordWrap = true;
            EditorGUILayout.LabelField(title.ToString(), content == null ? "null" : content.ToString());
        }

        public static void NiceNumber(object title, int content)
        {
            EditorStyles.label.wordWrap = true;
            EditorGUILayout.LabelField(title.ToString(), content.ToString("N0"));
        }

        public static void BoolLabelWarning(object title, bool value)
        {
            BoolLabel(title, value, Color.green, orangeColor);
        }
        public static void BoolLabel(object title, bool value)
        {
            BoolLabel(title, value, Color.green, Color.white);
        }
        public static void BoolLabel(object title, bool value, Color positive, Color negative)
        {
            string content = value ? @"✔️ Yes" : @"✖️ No";
            EditorStyles.label.wordWrap = true;
            Color color = GUI.contentColor;
            GUI.contentColor = value ? positive : negative;
            EditorGUILayout.LabelField(title.ToString(), content);
            GUI.contentColor = color;
        }

        public static void Label(object title)
        {
            EditorStyles.label.wordWrap = true;
            EditorGUILayout.LabelField(title.ToString());
        }

        public static void LabelWithColor(object title, Color color)
        {
            Color c = EditorStyles.label.normal.textColor;
            EditorStyles.label.wordWrap = true;
            EditorStyles.label.normal.textColor = color;
            GUILayout.Label(title.ToString(), EditorStyles.label);
            EditorStyles.label.normal.textColor = c;
        }

        public static void Label(object title, params GUILayoutOption[] options)
        {
            EditorStyles.label.wordWrap = true;
            GUILayout.Label(title.ToString(), options);
        }

        public static void BoldLabel(object title, params GUILayoutOption[] options)
        {
            EditorStyles.label.wordWrap = true;
            EditorStyles.label.font = EditorStyles.boldFont;
            GUILayout.Label(title.ToString(), EditorStyles.label, options);
            EditorStyles.label.font = EditorStyles.standardFont;
        }

        public static bool BoldFoldout(bool value, string title)
        {
            EditorStyles.foldout.wordWrap = false;
            EditorStyles.foldout.font = EditorStyles.boldFont;
            value = EditorGUILayout.Foldout(value, title);
            EditorStyles.foldout.font = EditorStyles.standardFont;
            return value;
        }



        public static void TitleLabel(object title, params GUILayoutOption[] options)
        {
            RectOffset old = EditorStyles.whiteLargeLabel.padding;
            EditorStyles.whiteLargeLabel.padding = new RectOffset(5, 0, -2, 0);
            GUILayout.Label(title.ToString(), EditorStyles.whiteLargeLabel, options);
            EditorStyles.whiteLargeLabel.padding = old;
        }

        public static void TitleLabel(object title, string button, System.Action action, params GUILayoutOption[] options)
        {
            RectOffset old = EditorStyles.whiteLargeLabel.padding;
            EditorStyles.whiteLargeLabel.padding = new RectOffset(5, 0, -2, 0);
            UI.HorizontalGroup(delegate
            {
                GUILayout.Label(title.ToString(), EditorStyles.whiteLargeLabel, options);
                UI.Space();
                UI.Button(button, action);
            });
            EditorStyles.whiteLargeLabel.padding = old;
        }

        public static void BoldLabel(object title, object content, params GUILayoutOption[] options)
        {
            EditorStyles.label.wordWrap = true;
            EditorStyles.label.font = EditorStyles.boldFont;
            EditorGUILayout.LabelField(title.ToString(), content.ToString(), options);
            EditorStyles.label.font = EditorStyles.standardFont;
        }

        public static void MiniLabel(object title)
        {
            GUILayout.Label(title.ToString(), EditorStyles.wordWrappedMiniLabel);
        }

        public static void MiniLabel(object title, object content)
        {
            EditorStyles.label.font = EditorStyles.miniFont;
            EditorGUILayout.LabelField(title.ToString(), content.ToString());
            EditorStyles.label.font = EditorStyles.standardFont;
        }

        public static void BoldWarningLabel(object title, bool warn = true, params GUILayoutOption[] options)
        {
            EditorStyles.boldLabel.alignment = TextAnchor.UpperLeft;
            EditorStyles.boldLabel.padding = new RectOffset(0, 0, -2, 0);
            EditorStyles.boldLabel.wordWrap = true;
            EditorStyles.boldLabel.normal.textColor = warn ? Color.red : EditorStyles.label.hover.textColor;
            GUILayout.Label(title.ToString(), EditorStyles.boldLabel, options);
            EditorStyles.boldLabel.normal.textColor = EditorStyles.label.hover.textColor;
        }

        public static void BoldWarningLabel(string title, string text, bool warn = true, params GUILayoutOption[] options)
        {
            EditorStyles.boldLabel.alignment = TextAnchor.UpperLeft;
            EditorStyles.boldLabel.padding = new RectOffset(0, 0, -2, 0);
            EditorStyles.boldLabel.wordWrap = true;
            EditorStyles.boldLabel.normal.textColor = warn ? Color.red : EditorStyles.label.hover.textColor;
            EditorGUILayout.LabelField(title, text, EditorStyles.boldLabel, options);
            EditorStyles.boldLabel.normal.textColor = EditorStyles.label.hover.textColor;
        }

        public static void MiniWarningLabel(object title, object content, bool warn)
        {
            EditorStyles.label.font = EditorStyles.miniFont;
            EditorStyles.label.normal.textColor = warn ? Color.red : EditorStyles.label.hover.textColor;
            EditorGUILayout.LabelField(title.ToString(), content.ToString());
            EditorStyles.label.font = EditorStyles.standardFont;
            EditorStyles.label.normal.textColor = EditorStyles.label.hover.textColor;
        }

        public static void MiniLabelRight(object title)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            TextAnchor t = EditorStyles.wordWrappedMiniLabel.alignment;
            EditorStyles.wordWrappedMiniLabel.alignment = TextAnchor.MiddleRight;
            GUILayout.Label(title.ToString(), EditorStyles.wordWrappedMiniLabel);
            EditorStyles.wordWrappedMiniLabel.alignment = t;
            GUILayout.Space(5);
            GUILayout.EndHorizontal();
        }

        public static void Separator()
        {
            EditorGUILayout.Separator();
        }

        public static void Space()
        {
            GUILayout.FlexibleSpace();
        }

        public static void Space(float space)
        {
            GUILayout.Space(space);
        }

        public static void Float(string label, ref float v)
        {
            v = EditorGUILayout.FloatField(label, v);
        }

        public static Font VeryBoldFont
        {
            get
            {
                return EditorStyles.boldFont;
            }
        }

        public static GUIStyle VeryBoldLabel
        {
            get
            {
                if (_veryBoldLabel == null)
                {
                    _veryBoldLabel = new GUIStyle(EditorStyles.boldLabel);
                    _veryBoldLabel.font = VeryBoldFont;
                    _veryBoldLabel.wordWrap = true;
                }
                return _veryBoldLabel;
            }
        }

        private static GUIStyle _veryBoldLabel = null;
        public const string k_AssetOnlyMessage = "Asset-only: cannot be edited as scene object!";

        public static string DefaultHeaderTitle = "Default Header Title";

        private static GUIStyle _headerStyle = null, _iconStyle = null, _titleStyle = null, _boxStyle = null, _footerStyle = null;

        public static Color ContainerColor = new Color(0.25f, 0.25f, 0.35f, 1f);

        public static void Header(UnityEditor.Editor inspector, Texture2D icon = null, string title = null, System.Action content = null, System.Action headerButtons = null)
        {
            GUILayout.Space(3);
            if (_headerStyle == null)
            {
                _headerStyle = new GUIStyle(EditorStyles.helpBox);
                _headerStyle.normal.textColor = Color.white;//EditorGUIUtility.isProSkin ? Color.white : Color.black;
                _headerStyle.fixedHeight = 34;
                _headerStyle.overflow = new RectOffset(14, 8, 0, 0);
            }
            if (_iconStyle == null)
            {
                _iconStyle = new GUIStyle();
                _iconStyle.fixedHeight = 32;
                _iconStyle.fixedWidth = 32;
                _iconStyle.padding = new RectOffset(2, 2, 2, 4);
            }
            if (_titleStyle == null)
            {
                _titleStyle = new GUIStyle(EditorStyles.whiteBoldLabel);
                _titleStyle.normal.textColor = Color.white;//EditorStyles.label.normal.textColor;
                _titleStyle.font = VeryBoldFont;
                _titleStyle.padding = new RectOffset(2, 2, 3, 5);
            }

            GUI.backgroundColor = ContainerColor;
            GUILayout.BeginHorizontal(_headerStyle);
            GUI.backgroundColor = Color.white;
            if (icon != null)
            {
                GUILayout.Label(icon, _iconStyle);
            }

            EditorStyles.boldLabel.alignment = TextAnchor.MiddleLeft;
            GUILayout.BeginVertical(GUILayout.MaxHeight(34));
            Space(4);
            GUILayout.Label(string.IsNullOrEmpty(title) ? DefaultHeaderTitle : title, _titleStyle);
            Space(4);
            GUILayout.EndVertical();
            if (content != null)
            {
                content();
            }

            if (inspector)
            {
                Color color = EditorStyles.toolbarButton.normal.textColor;
                EditorStyles.toolbarButton.normal.textColor = Color.white;

                GUILayout.BeginVertical(GUILayout.MaxHeight(34));
                Space(4);
                GUILayout.BeginHorizontal();
                if (headerButtons != null)
                {
                    headerButtons();
                }

                Space();
                MonoScript baseScript = null;
                MonoScript inspectorScript = null;

                if (inspector.target is MonoBehaviour)
                {
                    baseScript = MonoScript.FromMonoBehaviour(inspector.target as MonoBehaviour);
                }
                else
                {
                    ScriptableObject scriptableObject = inspector.target as ScriptableObject;
                    if (scriptableObject)
                    {
                        baseScript = MonoScript.FromScriptableObject(scriptableObject);
                    }
                }
                inspectorScript = MonoScript.FromScriptableObject(inspector);

                if (baseScript && AssetDatabase.GetAssetPath(baseScript).Contains(".dll"))
                {
                    baseScript = null;
                }
                if (inspectorScript && AssetDatabase.GetAssetPath(inspectorScript).Contains(".dll"))
                {
                    inspectorScript = null;
                }

                if (baseScript != null || inspectorScript != null)
                {
                    UI.Button("Edit Script", delegate ()
                    {
                        GenericMenu menu = new GenericMenu();
                        if (baseScript != null)
                        {
                            menu.AddItem(new GUIContent("Class"), false, delegate (object userData)
                            {
                                AssetDatabase.OpenAsset(baseScript);
                            }, null);
                        }
                        if (inspectorScript != null)
                        {
                            menu.AddItem(new GUIContent("Inspector"), false, delegate (object userData)
                            {
                                AssetDatabase.OpenAsset(inspectorScript);
                            }, null);
                        }
                        menu.ShowAsContext();
                    }, GUILayout.Width(64));
                }
                Space(4);
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();

                EditorStyles.toolbarButton.normal.textColor = color;
            }

            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
            Space(12);
        }

        public static void Toolbar(System.Action content)
        {
            try
            {
                //GUILayout.Space (3);
                if (_footerStyle == null)
                {
                    _footerStyle = new GUIStyle(EditorStyles.helpBox);
                    _footerStyle.overflow = new RectOffset(14, 8, 0, 0);
                    _footerStyle.fixedHeight = 30;
                }
                GUI.backgroundColor = ContainerColor;
                GUILayout.BeginVertical(_footerStyle, GUILayout.MaxHeight(24));
                GUI.backgroundColor = Color.white;
                GUILayout.Space(2);

                GUILayout.BeginHorizontal();
                GUILayout.Space(2);
                content();
                UI.Space();
                GUILayout.EndHorizontal();

                GUILayout.Space(2);
                GUILayout.EndVertical();
            }
            catch (System.InvalidOperationException e)
            {
                Debug.Log(e.ToString());
            }

        }

        public static void Tipp(string tipp)
        {
            Tipp(tipp, Color.white);
        }

        public static void Tipp(string tipp, Color color)
        {
            GUI.color = color;
            UI.Space(5);
            GUILayout.BeginVertical("Box");
            GUI.color = Color.white;
            UI.MiniLabel(tipp);
            GUILayout.EndVertical();
            UI.Space(5);
        }

        public static void PlayerPrefsToggleBar(string title, string prefId, int[] values, string[] valueNames, int defaultValue)
        {
            UI.HorizontalGroup(delegate ()
            {
                UI.Label(title + ": ");
                UI.Space();
                int active = PlayerPrefs.GetInt(prefId, defaultValue);
                for (int i = 0; i < values.Length; i++)
                {
                    UI.Button(valueNames[i], delegate ()
                    {
                        PlayerPrefs.SetInt(prefId, values[i]);
                    }, true, active == values[i], Color.yellow);
                }
            });
        }

        public static void ShowError(object error, bool log = true)
        {
            if (log) Debug.LogError(error.ToString());
            EditorUtility.DisplayDialog("Error", error.ToString(), "Okay");
        }
    }
}
