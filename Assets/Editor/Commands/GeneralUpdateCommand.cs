using System.Collections.Generic;
using Editor.Nodes;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace Editor.Commands
{
    // Command to refresh the root node
    public class GeneralUpdateCommand : ModelCommand<RootNode>
    {
        private const string UNDO_SINGULAR = "Update root";
        private const string UNDO_PLURAL = "Update roots";

        public GeneralUpdateCommand(IReadOnlyList<RootNode> models) 
            : base(UNDO_SINGULAR, UNDO_PLURAL, models)
        {
        }
        
        public static void DefaultHandler(GraphToolState state, GeneralUpdateCommand command)
        {
            //state.PushUndo(command);

            using (var graphUpdater = state.GraphViewState.UpdateScope)
            {
                foreach (var nodeModel in command.Models)
                {
                    graphUpdater.MarkChanged(nodeModel);
                }
            }
        }
    }
}