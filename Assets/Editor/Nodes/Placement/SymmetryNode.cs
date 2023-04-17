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
    [SearcherItem(typeof(ABuilderStencil), SearcherContext.Graph, "Placement/Symmetry")]
    public class SymmetryNode : BaseBuilderNode
    {
        private IPortModel _parentPort;
        private IPortModel _positionPort;
        private IPortModel _distancePort;
        private IPortModel _outputPort;
        private IPortModel _xAxisPort;
        private IPortModel _yAxisPort;
        private IPortModel _zAxisPort;

        public SymmetryNode()
        {
            Title = "Symmetry";
        }

        protected override void OnDefineNode()
        {
            base.OnDefineNode();
            
            _parentPort = this.AddDataInputPort("Parent", TypeHandle.GameObject, options: PortModelOptions.NoEmbeddedConstant);
            _positionPort = this.AddDataInputPort("Local Position", TypeHandle.Vector3);            
            _distancePort = this.AddDataInputPort("Distance", TypeHandle.Float);
            _xAxisPort = this.AddDataInputPort("X Axis", TypeHandle.Bool);
            _yAxisPort = this.AddDataInputPort("Y Axis", TypeHandle.Bool);
            _zAxisPort = this.AddDataInputPort("Z Axis", TypeHandle.Bool);
            _outputPort = this.AddDataOutputPort("Object", TypeHandle.GameObject);
        }

        public override void Build(Transform parent)
        {
            var root = MakeEmpty(parent, _positionPort.GetValue<Vector3>());

            var distance = _distancePort.GetValue<float>();
            if (_xAxisPort.GetValue<bool>())
            {
                BuildAxis(root.transform, new Vector3(distance, 0, 0));
            }
            if (_yAxisPort.GetValue<bool>())
            {
                BuildAxis(root.transform, new Vector3(0, distance, 0));
            }
            if (_zAxisPort.GetValue<bool>())
            {
                BuildAxis(root.transform, new Vector3(0, 0, distance));
            }
        }

        private void BuildAxis(Transform root, Vector3 posVector)
        {
            var pos = MakeEmpty(root, posVector);
            _outputPort.BuildAll(pos.transform);
            
            var neg = MakeEmpty(root, -posVector);
            _outputPort.BuildAll(neg.transform);
        }

        public override void Update(Transform parent)
        {
            
        }
    }
}