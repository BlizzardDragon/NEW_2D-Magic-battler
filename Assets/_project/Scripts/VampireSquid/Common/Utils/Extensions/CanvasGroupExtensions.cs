using DG.Tweening;
using UnityEngine;

namespace VampireSquid.Common.Utils.Extensions
{
    public static class CanvasGroupExtensions
    {
        public static void FadeIn(this CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1, duration);
        }

        public static void FadeOut(this CanvasGroup canvasGroup, float duration) 
        {
            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(0, duration);
        }

        public static void Show(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha          = 1;
            canvasGroup.interactable   = true;
            canvasGroup.blocksRaycasts = true;
        }

        public static void Hide(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha          = 0;
            canvasGroup.interactable   = false;
            canvasGroup.blocksRaycasts = false;
        }

        public static void SetOptions(this CanvasGroup canvasGroup,  float alpha,
                                      bool             interactable, bool  blocksRaycasts)
        {
            canvasGroup.alpha          = alpha;
            canvasGroup.interactable   = interactable;
            canvasGroup.blocksRaycasts = blocksRaycasts;
        }
    }
}
