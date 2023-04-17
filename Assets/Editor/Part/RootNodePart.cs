using Editor.Commands;
using Editor.Nodes;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Editor.Part
{
    public class RootNodePart : BaseModelUIPart
    {
        private const int ImageSize = 256;
        private const float CameraDistance = 20f;

        private static readonly Rect ImageDimension = new(0, 0, ImageSize, ImageSize);

        private static readonly Vector3 DefaultCameraPosition = new(0, 0, -CameraDistance);

        private static Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            return Quaternion.Euler(angles.y, -angles.x, 0) * (point - pivot) + pivot;
        }
        
        public RootNodePart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName) : base(name, model, ownerElement, parentClassName)
        {
            // setup basic Preview Render Utility
            _previewUtility = new PreviewRenderUtility();
            _previewUtility.camera.farClipPlane = 200;
            Transform transform = _previewUtility.camera.transform;
            transform.position = DefaultCameraPosition;
            transform.LookAt(Vector3.zero);

            // Create and add the empty root GameObject
            GameObject obj = new GameObject();
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            _previewUtility.AddSingleGO(obj);
            _origin = obj.transform;
        }

        private VisualElement _rootContainer;
        public override VisualElement Root => _rootContainer;

        private PreviewRenderUtility _previewUtility;
        private Transform _origin;
        private Image _image;
        private bool _mouseIsDown;
        private Vector3 _previousMousePosition;
        private Vector3 _totalRotation;
        
        protected override void BuildPartUI(VisualElement parent)
        {
            if (m_Model is not RootNode rootNode) return;
            rootNode.OnRebuilt -= OnRebuilt;
            rootNode.OnRebuilt += OnRebuilt;
            rootNode.Origin = _origin;
            rootNode.Build();
            _rootContainer = new VisualElement();
            _rootContainer.AddManipulator(new ContextualMenuManipulator((ContextualMenuPopulateEvent evt) =>
            {
                evt.StopPropagation();
            }));
            
            // Create Image
            _image = new Image();
            _rootContainer.Add(_image);
            
            // Setup Mouse Handlers for image
            // note image should be focusable for image to receive all event types
            _image.RegisterCallback<PointerDownEvent>(OnPointerDownHandler);
            _image.RegisterCallback<PointerMoveEvent>(OnPointerMoveHandler);
            _image.RegisterCallback<PointerUpEvent>(OnPointerUpHandler);
            
            // create and assign Render Texture to Image
            _previewUtility.BeginPreview(ImageDimension, GUIStyle.none);
            _previewUtility.camera.Render();
            _image.image = _previewUtility.EndPreview();
            parent.Add(_rootContainer);
        }

        private void OnRebuilt()
        {
            if (_image != null && _previewUtility != null)
            {
                _previewUtility.BeginPreview(ImageDimension, GUIStyle.none);
                _previewUtility.camera.Render();
                _previewUtility.EndAndDrawPreview(ImageDimension);
                _image.MarkDirtyRepaint();
            }
        }

        protected override void UpdatePartFromModel()
        {
        }
        
        private void OnPointerDownHandler(PointerDownEvent evt)
        {
            // start dragging
            if (evt.button == 1)
            {
                _mouseIsDown = true;
                _previousMousePosition = evt.position;
                evt.StopImmediatePropagation();
            }
        }

        private void OnPointerMoveHandler(PointerMoveEvent evt)
        {
            // If the mouse is not down do nothing
            if (!_mouseIsDown)
            {
                return;
            }

            // Calculate delta and add it to the total rotation
            Vector3 delta = evt.position - _previousMousePosition;
            _previousMousePosition = evt.position;
            _totalRotation += delta;

            // pivot camera around (0, 0, 0)
            _previewUtility.camera.transform.position = RotateAroundPivot(DefaultCameraPosition, Vector3.zero, _totalRotation);
            _previewUtility.camera.transform.LookAt(Vector3.zero);

            // Render new camera location - seems to lag
            _previewUtility.BeginPreview(ImageDimension, GUIStyle.none);
            _previewUtility.camera.Render();
            _previewUtility.EndAndDrawPreview(ImageDimension);
            
            _image.MarkDirtyRepaint();
        }

        private void OnPointerUpHandler(PointerUpEvent evt)
        {
            if(evt.button == 1)
                _mouseIsDown = false;
        }

        protected override void PartOwnerRemovedFromView()
        {
            // Cleanup preview 
            if (_previewUtility is not null)
            {
                _previewUtility.Cleanup();
                _previewUtility = null;
            }
        }
    }
}