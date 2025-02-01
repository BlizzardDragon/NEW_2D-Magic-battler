using System;
using System.Collections.Generic;
using UnityEngine;
using VampireSquid.Common.Utils.Helpers;

namespace VampireSquid.Common.UI.Window
{
    public abstract class WindowManager<T> : MonoBehaviour, IWindowManager<T> where T : Enum
    {
        private readonly IEnumerable<SerializableMap<T, WindowBase>> windowsMap;

        protected WindowManager(IEnumerable<SerializableMap<T, WindowBase>> windows) => windowsMap = windows;

        public bool TryGet<TWindow>(T type, out TWindow window) where TWindow : class
        {
            foreach (var pair in windowsMap)
            {
                if (type.Equals(pair.Key))
                {
                    window = pair.Value as TWindow;
                    return true;
                }
            }

            window = null;
            return false;
        }

        public void Show(T type, bool hideOther = true)
        {
            if (hideOther)
                HideAll();

            foreach (var pair in windowsMap)
            {
                if (type.Equals(pair.Key))
                    pair.Value.Show();
            }
        }

        public void Show<TBindableModel>(T type, TBindableModel bindableModel, bool hideOther = true)
        {
            if (hideOther)
                HideAll();

            foreach (var pair in windowsMap)
            {
                if (type.Equals(pair.Key))
                {
                    if (pair.Value is BindableWindowBase<TBindableModel> bindable)
                        bindable.Bind(bindableModel);
                }
            }
        }

        public void Hide(T type)
        {
            foreach (var pair in windowsMap)
            {
                if (type.Equals(pair.Key))
                    pair.Value.Hide();
            }
        }

        public void HideAll()
        {
            foreach (var pair in windowsMap)
                pair.Value.Hide();
        }
    }
}