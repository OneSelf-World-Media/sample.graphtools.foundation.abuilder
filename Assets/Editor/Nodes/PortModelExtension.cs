using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;

namespace Editor.Nodes
{
    public static class PortModelExtension
    {
        public static T GetValue<T>(this IPortModel self)
        {
            if (self == null)
                return default(T);
            var node = self.GetConnectedEdges().FirstOrDefault()?.FromPort.NodeModel;

            switch (node)
            {
                case IVariableNodeModel varNode:
                    return (T)varNode.VariableDeclarationModel.InitializationModel.ObjectValue;
                case IConstantNodeModel constNode:
                    return (T)constNode.ObjectValue;
                case IEdgePortalExitModel portalModel:
                    var oppositePortal = portalModel.GraphModel.FindReferencesInGraph<IEdgePortalEntryModel>(portalModel.DeclarationModel).FirstOrDefault();
                    if (oppositePortal != null)
                    {
                        return oppositePortal.InputPort.GetValue<T>();
                    }
                    return default(T);
                default:
                    return (T)self.EmbeddedValue.ObjectValue;
            }
        }

        public static void BuildAll(this IPortModel self, Transform parent)
        {
            if (self == null) return;
            var nodes = self.GetConnectedEdges().Select(e => e.ToPort.NodeModel);
            foreach (var node in nodes)
            {
                if (node is BaseBuilderNode builderNode)
                {
                    builderNode.Build(parent);
                }
            }
        }
    }
}
