using UnityEngine;
using UnityEditor;

using Components;
using Pixel;
using UnityEngine.XR;

namespace Editor
{
    [CustomEditor(typeof(PixelAnimator), true)]
    
    
    // This should be an editorWindow
    public class PixelAnimatorEditor : UnityEditor.Editor
    {
        private PixelAnimator _p;

        [ExecuteAlways]
        public void OnSceneGUI () 
        {
            _p = this.target as PixelAnimator;

            if (_p is null) return;
            _p.GetBoxInfo(out var hit, out var hurt);

            var position = _p.gameObject.transform.position;
            DrawColoredRect(position, hit.shape, Color.red);
            DrawColoredRect(position, hurt.shape, Color.green);
        }
        private void DrawColoredRect(Vector3 origin, Rect rect, Color color)
        {
            Handles.color = color;
            Vector3 botLeft = new Vector3(rect.min.x, rect.min.y, 0.0f);
            Vector3 topRight = new Vector3(rect.max.x, rect.max.y, 0.0f);
            Vector3 botRight = new Vector3(topRight.x, botLeft.y, 0.0f);
            Vector3 topLeft = new Vector3(botLeft.x, topRight.y, 0.0f);

            Handles.DrawLine(origin + botLeft, origin + botRight, 2.0f);
            Handles.DrawLine(origin + botRight, origin + topRight, 2.0f);
            Handles.DrawLine(origin + topRight, origin + topLeft, 2.0f);
            Handles.DrawLine(origin + topLeft, origin + botLeft, 2.0f);
        }
    }
}
