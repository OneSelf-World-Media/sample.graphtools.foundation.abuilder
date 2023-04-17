using System;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace Editor.Graph
{
    public class ABuilderStencil : Stencil
    {
        public static string GraphName => "Avatar Builder";
        public override string ToolName => GraphName;
    
    
        public override IBlackboardGraphModel CreateBlackboardGraphModel(IGraphAssetModel graphAssetModel)
        {
            // Default implementation
            return new BlackboardGraphModel(graphAssetModel);
        }

        public override void PopulateBlackboardCreateMenu(string sectionName, GenericMenu menu, CommandDispatcher commandDispatcher)
        {
            menu.AddItem(new GUIContent("Create Vector 3"), false, () =>
            {
                var name = $"My Vector3 {commandDispatcher.State.WindowState.GraphModel.VariableDeclarations.Count}";
                commandDispatcher.Dispatch(new CreateGraphVariableDeclarationCommand(name, true, TypeHandle.Vector3, ModifierFlags.ReadWrite));
            });
            menu.AddItem(new GUIContent("Create Int"), false, () =>
            {
                var name = $"My Int {commandDispatcher.State.WindowState.GraphModel.VariableDeclarations.Count}";
                commandDispatcher.Dispatch(new CreateGraphVariableDeclarationCommand(name, true, TypeHandle.Int));
            });

        }

        // Required to see variable constants in node view 
        public override Type GetConstantNodeValueType(TypeHandle typeHandle)
        {
            return TypeToConstantMapper.GetConstantNodeType(typeHandle);
        }

        public override void PreProcessGraph(IGraphModel graphModel)
        {
            base.PreProcessGraph(graphModel);
            if (graphModel.NodeModels.Count == 0)
            {
                // Initialize the empty graph   
                if (graphModel is ABuilderGraphModel builderGraphModel)
                    builderGraphModel.Initialize();
            }
        }
    }
}
