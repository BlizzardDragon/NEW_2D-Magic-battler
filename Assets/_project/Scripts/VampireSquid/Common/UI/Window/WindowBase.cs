using UnityEngine;

namespace VampireSquid.Common.UI.Window
{
    public interface IWindowBase
    {
        void Show();
        void Hide();
    }

    public class WindowBase : MonoBehaviour, IWindowBase
    {
        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        protected virtual void OnShow() { }

        protected virtual void OnHide() { }
    }
}