using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace Editor.Graph
{
    public class ABuilderGraphWindow : GraphViewEditorWindow
    {
        [InitializeOnLoadMethod]
        static void RegisterTool()
        {
            ShortcutHelper.RegisterDefaultShortcuts<ABuilderGraphWindow>(ABuilderStencil.GraphName);
        }
        
        public static void ShowWindow()
        {
            FindOrCreateGraphWindow<ABuilderGraphWindow>();
        }

        protected override void OnEnable()
        {
            EditorToolName = "Avatar Builder";
            base.OnEnable();
        }

        protected override bool CanHandleAssetType(IGraphAssetModel asset)
        {
            return asset is ABuilderAssetModel;
        }

        protected override GraphView CreateGraphView()
        {
            return new ABuilderGraphView(this, CommandDispatcher, EditorToolName);
        }

        protected override GraphToolState CreateInitialState()
        {
            var prefs = Preferences.CreatePreferences(EditorToolName);
            return new ABuilderState(GUID, prefs);
        }
    }
}