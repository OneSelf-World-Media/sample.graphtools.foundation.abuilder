using Editor.Commands;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace Editor.Graph
{
    public class ABuilderState : GraphToolState
    {
        public ABuilderState(Hash128 graphViewEditorWindowGUID, Preferences preferences) : base(graphViewEditorWindowGUID, preferences)
        {
            this.SetInitialSearcherSize(SearcherService.Usage.k_CreateNode, new Vector2(500, 400), 2.25f);
        }

        public override void RegisterCommandHandlers(Dispatcher dispatcher)
        {
            base.RegisterCommandHandlers(dispatcher);

            if (dispatcher is not CommandDispatcher commandDispatcher) return;
            commandDispatcher.RegisterCommandHandler<GeneralUpdateCommand>(GeneralUpdateCommand.DefaultHandler);
            commandDispatcher.RegisterCommandHandler<UpdatePortConstantCommand>(PortCommandHandler);
            commandDispatcher.RegisterCommandHandler<UpdateConstantValueCommand>(ConstantCommandHandler);
        }

        // Intercept constant value updates to refresh preview
        private void ConstantCommandHandler(GraphToolState state, UpdateConstantValueCommand command)
        {
            UpdateConstantValueCommand.DefaultCommandHandler(state, command);
            if(state.GraphViewState.GraphModel is ABuilderGraphModel builderModel)
                builderModel.RootNode.Build();
        }

        private void PortCommandHandler(GraphToolState state, UpdatePortConstantCommand command)
        {
            UpdatePortConstantCommand.DefaultCommandHandler(state, command);
            if(state.GraphViewState.GraphModel is ABuilderGraphModel builderModel)
                builderModel.RootNode.Build();
        }
    }
}