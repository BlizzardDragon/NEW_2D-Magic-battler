/* ---
 

using Cysharp.Threading.Tasks;
using UnityEngine;
using VampireSquid.Common.Assets;
using VampireSquid.Common.CompositeRoot;
using VampireSquid.Common.Loading;
using VampireSquid.Common.Loading.Curtain;
using VampireSquid.Common.Loading.Operations;
using VampireSquid.Common.Repositories.Repos;
using VampireSquid.Common.Scenes;
using VampireSquid.Common.Scenes.Configs;
using VampireSquid.Common.Version;
using static VampireSquid.Common.CommonConstants;

namespace VampireSquid.Common.Bootstrap
{
    public class BootstrapRoot : CompositeRootBase
    {
        private IScenesProvider         scenesContainer;
        private ILoadingBuilder         loadingBuilder;
        private LoadingCurtain          loadingCurtain;
        private ScenesProviderConfig    providerConfig;
        private AppVersionConfig        appVersionConfig;

        public override async UniTask InstallBindings()
        {
            var assetProvider = new AddressablesAssetsProvider();
            var connections   = new ConnectionsRepository();
            
            BindAsGlobal<IAssetsProvider>(assetProvider);
            BindAsGlobal<IClientsRepository>(connections);

            await LoadDependencies(assetProvider);
            
            BindAsGlobal<IAppVersion>(appVersionConfig);
            
            var operationsLoader = new OperationsLoader(loadingCurtain);

            scenesContainer = new ScenesProvider(providerConfig);
            loadingBuilder  = new LoadingBuilder(operationsLoader, providerConfig);

            BindAsGlobal<IScenesProvider>(scenesContainer);
            BindAsGlobal<ILoadingBuilder>(loadingBuilder);
        }

        // -- Create root any similar method, use any required scene to load after bootstraping
        //public override async UniTask Initialize()
        //{
        //    await loadingBuilder
        //        .Reset()
        //        .AddSceneLoading(scenesContainer.GetAllScenes.First() or any other)
        //        .LoadAsync();
        //}

        private async UniTask LoadDependencies(IAssetsProvider assetsProvider)
        {
            providerConfig = await assetsProvider.LoadAsync<ScenesProviderConfig>(AssetsPath.ScenesProviderConfig);

            var loadingCurtainAsset = await assetsProvider.LoadAsync<GameObject>(providerConfig.LoadingCurtain);
            loadingCurtain = Instantiate(loadingCurtainAsset).GetComponent<LoadingCurtain>();
            
            DontDestroyOnLoad(loadingCurtain);

            appVersionConfig = await assetsProvider.LoadAsync<AppVersionConfig>(AssetsPath.AppVersionConfig);
        }
    }
}

--- */