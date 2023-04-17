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
    [SearcherItem(typeof(ABuilderStencil), SearcherContext.Graph, "Parts/Cube")]
    public class CubeNode : BaseBuilderNode
    {
        private IPortModel _parentPort;
        private IPortModel _positionPort;
        private IPortModel _sizePort;
        private IPortModel _outputPort;
        
        public CubeNode()
        {
            Title = "Cube";
        }

        protected override void OnDefineNode()
        {
            base.OnDefineNode();
            
            _parentPort = this.AddDataInputPort("Parent", TypeHandle.GameObject, options: PortModelOptions.NoEmbeddedConstant);
            _positionPort = this.AddDataInputPort("Local Position", TypeHandle.Vector3);            
            _sizePort = this.AddDataInputPort("Scale", TypeHandle.Vector3);
            _outputPort = this.AddDataOutputPort("Object", TypeHandle.GameObject);
        }

        public override void Build(Transform parent)
        {
            var root = MakeEmpty(parent, _positionPort.GetValue<Vector3>());

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = root.transform;
            cube.transform.localPosition = Vector3.zero;
            cube.transform.localRotation = Quaternion.identity;
            cube.transform.localScale = _sizePort.GetValue<Vector3>();
            
            _outputPort.BuildAll(root.transform);
        }

        public override void Update(Transform parent)
        {
            
        }
    }
}