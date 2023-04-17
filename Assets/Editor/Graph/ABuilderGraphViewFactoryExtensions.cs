using Editor.Nodes;
using Editor.Views;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace Editor.Graph
{
    [GraphElementsExtensionMethodsCache(typeof(ABuilderGraphView))]
    public static class ABuilderGraphViewFactoryExtensions
    {
        // Hook up custom view for RootNode
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, RootNode model)
        {
            IModelUI ui = new RootNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }

        
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, BaseBuilderNode model)
        {
            IModelUI ui = new BaseBuilderNodeView();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }
}