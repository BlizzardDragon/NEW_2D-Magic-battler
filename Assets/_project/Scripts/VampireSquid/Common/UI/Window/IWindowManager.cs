using System;

namespace VampireSquid.Common.UI.Window
{
    public interface IWindowManager<T> where T : Enum
    {
        bool TryGet<TWindow>(T      type, out TWindow    window) where TWindow : class;
        void Show(T                 type, bool           hideOther                     = true);
        void Show<TBindableModel>(T type, TBindableModel bindableModel, bool hideOther = true);
        void Hide(T                 type);
        void HideAll();
    }
}