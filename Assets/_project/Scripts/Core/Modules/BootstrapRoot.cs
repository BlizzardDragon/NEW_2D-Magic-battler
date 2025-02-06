using Cysharp.Threading.Tasks;
using VampireSquid.Common.Assets;
using VampireSquid.Common.Commands;
using VampireSquid.Common.Commands.Handlers;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Core.Modules
{
    public class BootstrapRoot : CompositeRootBase
    {
        private SceneLoader _sceneLoader;

        public override async UniTask InstallBindings()
        {
            var assetProvider = new AddressablesAssetsProvider();
            _sceneLoader = new SceneLoader();

            BindAsGlobal<IAssetsProvider>(assetProvider);
            BindAsGlobal<IGlobalCommandHandler>(new GlobalCommandHandler());
        }

        public override async UniTask Initialize()
        {
            _sceneLoader.LoadGameScene();
        }
    }
}