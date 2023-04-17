using System;
using Editor.Graph;
using PlasticGui;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace Editor.Nodes
{
    [Serializable]
    public abstract class BaseBuilderNode : NodeModel
    {
        public override void OnConnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {
            base.OnConnection(selfConnectedPortModel, otherConnectedPortModel);
            if(GraphModel is ABuilderGraphModel builderGraphModel)
                builderGraphModel.RootNode.Build();
        }

        public override void OnDisconnection(IPortModel selfConnectedPortModel, IPortModel otherConnectedPortModel)
        {
            base.OnDisconnection(selfConnectedPortModel, otherConnectedPortModel);
            if(GraphModel is ABuilderGraphModel builderGraphModel)
                builderGraphModel.RootNode.Build();
        }

        public abstract void Build(Transform parent);
        public abstract void Update(Transform parent);
        
        protected GameObject MakeEmpty(Transform parent, Vector3 position)
        {
            var obj = new GameObject();
            obj.transform.parent = parent;
            obj.transform.localPosition = position;
            obj.transform.localScale = Vector3.one;
            obj.transform.localRotation = Quaternion.identity;
            return obj;
        }
    }
}