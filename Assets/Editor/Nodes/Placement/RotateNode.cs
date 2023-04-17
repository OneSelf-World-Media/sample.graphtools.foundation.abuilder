using System;
using Editor.Graph;
using UnityEditor.Build.Reporting;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;
using Capabilities = UnityEditor.Experimental.GraphView.Capabilities;

namespace Editor.Nodes
{
    [SearcherItem(typeof(ABuilderStencil), SearcherContext.Graph, "Placement/Rotate")]
    public class RotateNode : BaseBuilderNode
    {
        private IPortModel _parentPort;
        private IPortModel _anglesPort;
        private IPortModel _positionPort;
        private IPortModel _outputPort;

        public RotateNode()
        {
            Title = "Rotate";
        }

        protected override void OnDefineNode()
        {
            base.OnDefineNode();
            
            _parentPort = this.AddDataInputPort("Parent", TypeHandle.GameObject, options: PortModelOptions.NoEmbeddedConstant);
            _positionPort = this.AddDataInputPort("Local Position", TypeHandle.Vector3);
            _anglesPort = this.AddDataInputPort("Euler Angles", TypeHandle.Vector3);            
            _outputPort = this.AddDataOutputPort("Object", TypeHandle.GameObject);
        }

        public override void Build(Transform parent)
        {
            var root = MakeEmpty(parent, _positionPort.GetValue<Vector3>());
            root.transform.localEulerAngles = _anglesPort.GetValue<Vector3>();
            _outputPort.BuildAll(root.transform);
        }

        public override void Update(Transform parent)
        {
            
        }
    }
}