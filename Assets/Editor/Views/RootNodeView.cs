using Editor.Part;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace Editor.Views
{
    public class RootNodeView : CollapsibleInOutNode
    {
        protected override void BuildPartList()
        {
            base.BuildPartList();
            
            PartList.InsertPartAfter(titleIconContainerPartName, new RootNodePart("RootNode", Model, this, ussClassName));
        }
    }
}