# Avatar Builder
Sample project showing how to use Graphtools Foundation to build a node based editor UI in Unity.
This project was created for my GameDev Guild 2023 talk on creating a custom node based editor UI.

## Getting it running ##
You will get the following error due to the package not having been updated for Unity 2021, ignore the initial compiler error warning: 
```
error CS7036: There is no argument given that corresponds to the required formal parameter 'contextType' of 'PointerDeviceState.GetPointerPosition(int, ContextType)'
```
To fix this you will need to add ContextType.Editor to the end of the 2 GetPointerPosition() calls:
```
public static Vector2 GetMousePosition()
{
    return PointerDeviceState.GetPointerPosition(PointerId.mousePointerId, ContextType.Editor);
}
```
This will temporarily fix the errors but if the package cache gets re-built you will have to re-do the changes, the easiest way to overcome this is to copy the com.unity.graphtools.foundation-0.11.2-preview directory from the package cache to your Development directory and re-add the package "from disk".

## Accessing the tool ##
There are 3 samples Avatar Builder Assets in the root of the asset folder.
```
Blank Avatar Builder
Blocky Avatar Builder
Bubbles Avatar Builder
```
Double click on one of these to open the tool. To create your own Asset file right click in your project window and select **Create** > **Avatar Builder**
