using UnityEngine;

namespace VampireSquid.Common.Utils.Extensions
{
    public static class RectTransformExtensions
    {
        public static bool Overlaps(this RectTransform a, RectTransform b) {
            return a.WorldRect().Overlaps(b.WorldRect());
        }
        public static bool Overlaps(this RectTransform a, RectTransform b, bool allowInverse) {
            return a.WorldRect().Overlaps(b.WorldRect(), allowInverse);
        }
        
        public static Rect WorldRect(this RectTransform rectTransform, bool flipY = false) {
            Vector2 sizeDelta           = rectTransform.sizeDelta;
            float   rectTransformWidth  = sizeDelta.x * rectTransform.lossyScale.x;
            float   rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;

            Vector3 position = rectTransform.position;
            if (flipY) position.y = Mathf.Abs(Screen.height - position.y) + rectTransformHeight / 2f;
            return new Rect(position.x - rectTransformWidth / 2f, position.y - rectTransformHeight / 2f, rectTransformWidth, rectTransformHeight);
        }
        
        public static Matrix4x4 GetCanvasMatrix(this Canvas _Canvas)
        {
            RectTransform rectTr       = _Canvas.transform as RectTransform;
            Matrix4x4     canvasMatrix = rectTr.localToWorldMatrix;
            canvasMatrix *= Matrix4x4.Translate(-rectTr.sizeDelta / 2);
            return canvasMatrix;
        }
    }
}