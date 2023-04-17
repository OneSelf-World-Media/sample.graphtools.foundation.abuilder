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
    [SearcherItem(typeof(ABuilderStencil), SearcherContext.Graph, "Parts/Capsule")]
    public class CapsuleNode : BaseBuilderNode
    {
        private IPortModel _parentPort;
        private IPortModel _positionPort;
        private IPortModel _sizePort;
        private IPortModel _outputPort;
        
        public CapsuleNode()
        {
            Title = "Capsule";
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

            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var size = _sizePort.GetValue<float>();
            capsule.transform.parent = root.transform;
            capsule.transform.localPosition = Vector3.zero;
            capsule.transform.localRotation = Quaternion.identity;
            capsule.transform.localScale = new Vector3(size, size, size);
            
            _outputPort.BuildAll(root.transform);
        }

        public override void Update(Transform parent)
        {
            
        }
    }
}