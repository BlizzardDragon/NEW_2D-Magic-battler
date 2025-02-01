using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace VampireSquid.Common.Assets
{
    public interface IAssetsProvider
    {
        UniTask InitializeAsync();
        UniTask<List<string>> GetAssetsListByLabelAsync<TAsset>(string label);
        UniTask<List<string>> GetAssetsListByLabelAsync(string label, Type type = null);
        UniTask<TAsset[]> LoadAllAsync<TAsset>(List<string> keys) where TAsset : class;
        UniTask<TAsset> LoadAsync<TAsset>(string key) where TAsset : class;
        UniTask<TAsset> LoadAsync<TAsset>(AssetReference assetReference) where TAsset : class;
        UniTask ReleaseAssetsByLabelAsync(string label);
        UniTask WarmupAssetsByLabelAsync(string label);
        void Cleanup();
    }
}