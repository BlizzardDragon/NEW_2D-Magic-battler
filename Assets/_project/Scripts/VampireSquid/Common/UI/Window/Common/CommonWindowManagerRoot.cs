using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VampireSquid.Common.CompositeRoot;
using VampireSquid.Common.Utils.Helpers;

namespace VampireSquid.Common.UI.Window.Common
{
    public class CommonWindowManagerRoot : CompositeRootBase
    {
        [SerializeField] private List<SerializableMap<CommonWindowType, WindowBase>> windowsMap;

        private IWindowManager<CommonWindowType> windowManager;

        public override async UniTask InstallBindings()
        {
            windowManager = BindAsLocal<IWindowManager<CommonWindowType>>(new CommonWindowManager(windowsMap));
        }

        public override async UniTask Initialize() => windowManager.HideAll();
    }
}
