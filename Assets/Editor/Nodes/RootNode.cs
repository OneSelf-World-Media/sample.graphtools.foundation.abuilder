using System;
using System.Transactions;
using Codice.Client.GameUI.Explorer;
using Editor.Graph;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace Editor.Nodes
{
    public class RootNode : NodeModel
    {
        private IPortModel _outputPort;

        public Transform Origin;
        public Action OnRebuilt;
        public RootNode()
        {
            Title = "Root";
            
            this.SetCapability(UnityEditor.GraphToolsFoundation.Overdrive.Capabilities.Copiable, false);
            this.SetCapability(UnityEditor.GraphToolsFoundation.Overdrive.Capabilities.Deletable, false);
            this.SetCapability(UnityEditor.GraphToolsFoundation.Overdrive.Capabilities.Droppable, false);
        }

        protected override void OnDefineNode()
        {
            base.OnDefineNode();
            _outputPort = this.AddDataOutputPort("Origin", TypeHandle.GameObject);
            
            if (this.GraphModel is ABuilderGraphModel builderModel)
            {
                builderModel.RootNode = this;
            }
        }

        public void Build()
        {
            if (Origin == null) return;
            
            foreach (Transform child in Origin)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
            _outputPort.BuildAll(Origin);
            OnRebuilt?.Invoke();
        }
    }
}