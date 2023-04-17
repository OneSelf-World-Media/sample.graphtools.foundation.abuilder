using System;
using Editor.Graph;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;
using Capabilities = UnityEditor.Experimental.GraphView.Capabilities;

namespace Editor.Nodes
{
    // [Serializable]
    [SearcherItem(typeof(ABuilderStencil), SearcherContext.Graph, "Parts/Sphere")]
    public class SphereNode : BaseBuilderNode
    {
        private IPortModel _parentPort;
        private IPortModel _positionPort;
        private IPortModel _sizePort;
        private IPortModel _outputPort;
        
        public SphereNode()
        {
            Title = "Sphere";
        }

        protected override void OnDefineNode()
        {
            base.OnDefineNode();
            
            _parentPort = this.AddDataInputPort("Parent", TypeHandle.GameObject, options: PortModelOptions.NoEmbeddedConstant);
            _positionPort = this.AddDataInputPort("Local Position", TypeHandle.Vector3);            
            _sizePort = this.AddDataInputPort("Size", TypeHandle.Float);
            _outputPort = this.AddDataOutputPort("Object", TypeHandle.GameObject);
        }

        public override void Build(Transform parent)
        {
            var root = MakeEmpty(parent, _positionPort.GetValue<Vector3>());

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var size = _sizePort.GetValue<float>();
            sphere.transform.parent = root.transform;
            sphere.transform.localPosition = Vector3.zero;
            sphere.transform.localScale = new Vector3(size, size, size);
            
            _outputPort.BuildAll(root.transform);
        }

        public override void Update(Transform parent)
        {
            
        }
    }
}