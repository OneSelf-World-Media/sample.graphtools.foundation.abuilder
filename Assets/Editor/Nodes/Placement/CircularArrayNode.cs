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
    [SearcherItem(typeof(ABuilderStencil), SearcherContext.Graph, "Placement/CircularArray")]
    public class CircularArrayNode : BaseBuilderNode
    {
        private IPortModel _parentPort;
        private IPortModel _positionPort;
        private IPortModel _distancePort;
        private IPortModel _outputPort;
        private IPortModel _countPort;

        public CircularArrayNode()
        {
            Title = "Symmetry";
        }

        protected override void OnDefineNode()
        {
            base.OnDefineNode();
            
            _parentPort = this.AddDataInputPort("Parent", TypeHandle.GameObject, options: PortModelOptions.NoEmbeddedConstant);
            _positionPort = this.AddDataInputPort("Local Position", TypeHandle.Vector3);            
            _distancePort = this.AddDataInputPort("Distance", TypeHandle.Float);
            _countPort = this.AddDataInputPort("Count", TypeHandle.Int);
            _outputPort = this.AddDataOutputPort("Object", TypeHandle.GameObject);
        }

        public override void Build(Transform parent)
        {
            var root = MakeEmpty(parent, _positionPort.GetValue<Vector3>());
            var distance = _distancePort.GetValue<float>();
            var count = _countPort.GetValue<int>();
            
            for (int i = 0; i < count; i++)
            {
                float radius = distance;
                float angle = i * Mathf.PI * 2f / (float)count;
                Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
                var ringPoint = MakeEmpty(root.transform, newPos);
                ringPoint.transform.LookAt(root.transform.position);
                _outputPort.BuildAll(ringPoint.transform);
            }
        }

        public override void Update(Transform parent)
        {
            
        }
    }
}