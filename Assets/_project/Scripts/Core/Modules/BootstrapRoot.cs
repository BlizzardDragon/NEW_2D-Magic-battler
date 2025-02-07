using Cysharp.Threading.Tasks;
using VampireSquid.Common.Assets;
using VampireSquid.Common.Commands;
using VampireSquid.Common.Commands.Handlers;
using VampireSquid.Common.CompositeRoot;
using VampireSquid.Common.DependencyContainer;

namespace _project.Scripts.Core.Modules
{
    public class BootstrapRoot : CompositeRootBase
    {
        private GlobalCommandHandler _globalCommandHandler;
        private SceneLoader _sceneLoader;

        public override async UniTask InstallBindings()
        {
            var assetProvider = new AddressablesAssetsProvider();
            _globalCommandHandler = new GlobalCommandHandler();
            _sceneLoader = new SceneLoader();

            BindAsGlobal<IAssetsProvider>(assetProvider);
            BindAsGlobal<IGlobalCommandHandler>(_globalCommandHandler);
            BindAsGlobal<ISceneLoader>(_sceneLoader);
        }

        public override async UniTask Initialize()
        {
            _sceneLoader.LoadGameScene();
        }

        public override void OnBeforeDestroyed()
        {
            _globalCommandHandler.CleanUp();
            GlobalContext.CreateNewInstance();
        }
    }
}