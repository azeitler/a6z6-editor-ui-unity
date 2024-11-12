using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace A6Z6.Editor.UI
{
    /// <summary>
    /// This is the base inspector class for custom editor inspectors.
    /// You should use <see cref="Inspector"/> as the base class for your custom inspectors – it has a few nice additions.
    ///
    /// Layout:
    /// Override <see cref="Header"/> to add buttons to the header.
    /// Override <see cref="BeforeMainContent"/> to add content before the main content.
    /// Override <see cref="AfterMainContent"/> to add content after the main content.
    /// Override <see cref="FooterContent"/> to add a toolbar.
    ///
    /// Configuration
    /// <see cref="IsEnabled"/> determines whether the inspector is enabled and editing is allowed. Return false to disable.
    /// <see cref="NotEnabledMessage"/> provides a message for why the inspector is disabled.
    /// <see cref="DontAllowInstantiation"/> determines whether the inspector is enabled for asset-objects only.
    ///
    /// Events
    /// <see cref="OnActivate"/> use this instead of OnEnable
    /// <see cref="OnDeactivate"/> use this instead of OnDisable
    /// <see cref="Update"/> will be called every time the inspector is updated
    /// <see cref="OnChange"/> will be called every time the inspector changes, e.g. when a value is changed. Override this, but make sure to call the base method, which makes the target dirty.
    /// </summary>
	public class BaseInspector : UnityEditor.Editor
    {
        #region FACTORY

        public const string kTemplateFileContent = @"using UnityEngine;
using UnityEditor;
using A6Z6.Editor.UI;

/// <summary>
/// custom editor inspector for '{{0}}'
/// will only compile in Editor
/// </summary>
[CustomEditor(typeof({{0}}))]
public class {{0}}Inspector : A6Z6.Editor.UI.Inspector {

    public {{0}} Focus {
        get {
            return target as {{0}};
        }
    }

    // additional content here
}
";

        [MenuItem("Assets/Create/Inspector for Script")]
        public static void CreateInspectorForSelectedScriptFile()
        {
            Object[] selected = Selection.GetFiltered(typeof(MonoScript), SelectionMode.Assets);
            foreach (Object file in selected)
            {
                CreateInspectorForScriptFile(AssetDatabase.GetAssetPath(file));
            }
        }

        [MenuItem("Assets/Create/Inspector for Script", true)]
        public static bool CreateInspectorForSelectedScriptFileEnable()
        {
            Object[] selected = Selection.GetFiltered(typeof(MonoScript), SelectionMode.Assets);
            return selected.Length > 0;
        }

        public static void CreateInspectorForScriptFile(string path)
        {
            if (!CanCreateInspectorForScriptFile(path))
            {
                Debug.Log("Cannot create Inspector for " + path);
                return;
            }
            string inspectorFile = InspectorTargetFileForScriptFile(path);
            string type = Path.GetFileNameWithoutExtension(path);
            Directory.CreateDirectory(Path.GetDirectoryName(inspectorFile));
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
            File.WriteAllText(inspectorFile, kTemplateFileContent.Replace("{{0}}", type));
            AssetDatabase.ImportAsset(inspectorFile);
            Debug.Log("Created Inspector: " + inspectorFile);
            var obj = AssetDatabase.LoadAssetAtPath(inspectorFile, typeof(Object));
            Selection.activeObject = obj;
        }

        public static string InspectorTargetFileForScriptFile(string path)
        {
            string folder = Path.GetDirectoryName(path);
            if (!folder.Contains("Editor"))
            {
                folder = Path.Combine(folder, "..");
                folder = Path.Combine(folder, "..");
                folder = Path.Combine(folder, "Editor");
                folder = Path.Combine(folder, "Inspectors");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            string filename = Path.GetFileNameWithoutExtension(path);

            return Path.Combine(folder, filename + "Inspector.cs");
        }

        public static bool CanCreateInspectorForScriptFile(string path)
        {
            return path.Contains(".cs") && !File.Exists(InspectorTargetFileForScriptFile(path));
        }

        #endregion

        #region MEMBERS - fields and properties

        public string AssetPath
        {
            get
            {
                return Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this)));
            }
        }

        public string FilePath
        {
            get
            {
                return Application.dataPath + Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this)).Replace("Assets", ""));
            }
        }

        #endregion


        #region OVERRIDABLE METHODS - inspector configuration
        /// <summary>
        /// Icon for the inspector's title bar.
        /// </summary>
        public virtual Texture2D Icon()
        {
            return null;
        }

        /// <summary>
        /// Title for the inspector's title bar.
        /// </summary>
        public virtual string Title()
        {
            return ObjectNames.NicifyVariableName(target.GetType().Name);
        }

        public virtual void Header()
        {

        }

        /// <summary>
        /// override to enable inspector on asset-objects only (not on scene-objects)
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is enabled for asset-objects only; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool DontAllowInstantiation()
        {
            return false;
        }

        /// <summary>
        /// Determines whether this instance is enabled and editing is allowed!
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsEnabled()
        {
            return true;
        }

        /// <summary>
        /// Not enabled message, reason for why the inspector (e.g. asset-only) is disabled.
        /// </summary>
        /// <returns>
        /// The message.
        /// </returns>
        public virtual string NotEnabledMessage()
        {
            return "Editor not enabled!";
        }
        #endregion



        #region OVERRIDABLE METHODS - addtional content

        /// <summary>
        /// drawn before the main content of the inspector.
        /// </summary>
        public virtual void BeforeMainContent()
        {

        }

        /// <summary>
        /// draw the main content of the inspector (Default).
        /// </summary>
        public virtual void MainContent()
        {
            GUI.enabled = GuiEnabled(true);
            DrawDefaultInspector();
        }

        /// <summary>
        /// drawn after the main content of the inspector.
        /// </summary>
        public virtual void AfterMainContent()
        {

        }

        /// <summary>
        /// drawn in the footer of the inspector.
        /// </summary>
        public virtual void FooterContent()
        {

        }

        #endregion


        #region METHODS - initialization

        public virtual void OnActivate()
        {

        }

        public virtual void OnDeactivate()
        {

        }

        void OnEnable()
        {
            if (DontAllowInstantiation() && !AssetDatabase.Contains(target))
            {
                Component focus = target as Component;
                DestroyImmediate(focus.gameObject);
                Debug.LogError("Not allowed to instantiate object with this component attached!");
                return;
            }
            OnActivate();

            EditorApplication.update += Update;
        }

        void OnDisable()
        {
            OnDeactivate();
            EditorApplication.update -= Update;
        }

        public virtual void Update()
        {
            Repaint();
        }

        public virtual void OnChange()
        {
            target.MakeDirty();
        }

        public virtual float LabelWidth
        {
            get
            {
                return 135f;
            }
        }

        public virtual bool LooksLikeControls
        {
            get
            {
                return true;
            }
        }

        #endregion

        public bool GuiEnabled(bool state = true)
        {
            return state && IsEnabled();
        }

        public virtual void HeaderButtons()
        {

        }

        public override void OnInspectorGUI()
        {
            try
            {
                if (!IsEnabled())
                {
                    GUI.backgroundColor = Color.red;
                    UI.VerticalBox(delegate ()
                    {
                        UI.BoldLabel(NotEnabledMessage());
                    });
                    GUI.backgroundColor = Color.white;
                    UI.Disable();
                }

                EditorGUI.BeginChangeCheck();

                UI.BeginInspectorBoxWithHeader(this, Icon(), Title(), Header, HeaderButtons);

                EditorGUIUtility.labelWidth = LabelWidth;

                BeforeMainContent();
                MainContent();
                AfterMainContent();

                UI.EndInspectorBox();

                FooterContent();
                if (EditorGUI.EndChangeCheck())
                {
                    OnChange();
                }
            }
            catch (System.InvalidOperationException e)
            {
                Debug.Log(e.ToString());
            }
        }

        public static GUIContent Content(string str)
        {
            return new GUIContent(str);
        }
    }
}
