using UnityEngine;
using UnityEditor;

namespace A6Z6.Editor.UI
{
    public static class Extensions
    {
        public static void MakeDirty(this UnityEditor.Editor editor)
        {
            EditorUtility.SetDirty(editor.target);
        }

        public static void MakeDirty(this Object obj)
        {
            EditorUtility.SetDirty(obj);
        }
    }
}
