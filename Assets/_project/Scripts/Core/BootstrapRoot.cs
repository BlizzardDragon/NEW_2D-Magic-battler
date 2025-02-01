using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VampireSquid.Common.Assets;
using VampireSquid.Common.Commands;
using VampireSquid.Common.Commands.Handlers;
using VampireSquid.Common.CompositeRoot;

public class BootstrapRoot : CompositeRootBase
{
    public override async UniTask InstallBindings()
    {
        var assetProvider = new AddressablesAssetsProvider();

        BindAsGlobal<IAssetsProvider>(assetProvider);
        BindAsGlobal<IGlobalCommandHandler>(new GlobalCommandHandler());
    }

    public override async UniTask Initialize()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        SceneManager.LoadScene(1);
    }
}
