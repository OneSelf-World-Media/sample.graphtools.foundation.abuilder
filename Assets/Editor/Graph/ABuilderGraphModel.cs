using System;
using Editor.Nodes;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace Editor.Graph
{
    public class ABuilderGraphModel : GraphModel
    {
        public ABuilderGraphModel()
        {
            StencilType = null;
        }

        public override Type DefaultStencilType => typeof(ABuilderStencil);

        public RootNode RootNode;
        
        protected override bool IsCompatiblePort(IPortModel startPortModel, IPortModel compatiblePortModel)
        {
            return startPortModel.DataTypeHandle == compatiblePortModel.DataTypeHandle;
        }

        public void Initialize()
        {
            RootNode = (RootNode)CreateNode(typeof(RootNode), "Root", Vector2.one * 250);
        }
    }
}