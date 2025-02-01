using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VampireSquid.Common.BeautifulLogs;
using VampireSquid.Common.CompositeRoot;

namespace VampireSquid.Common.Assets
{
    public class SceneAssetWarmupRoot : CompositeRootBase
    {
        [SerializeField] private List<AssetLabelReference> assetLabels;

        private IAssetsProvider assetsProvider;

        public override async UniTask InstallBindings()
        {
            assetsProvider = BindAsGlobal<IAssetsProvider>(new AddressablesAssetsProvider());
        }

        public override async UniTask PreInitialize()
        {
            foreach (var asset in assetLabels)
                await assetsProvider.WarmupAssetsByLabelAsync(asset.labelString);
            
            BLog.LogImportant("Assets WarmUp Complete");
        }

        public override void OnBeforeDestroyed()
        {
            foreach (var asset in assetLabels)
                 assetsProvider.ReleaseAssetsByLabelAsync(asset.labelString);
        }
    }
}