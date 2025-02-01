using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace VampireSquid.Common.Assets
{
    public class AddressablesAssetsProvider : IAssetsProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> assetRequests = new();

        public async UniTask InitializeAsync() =>
            await Addressables.InitializeAsync().ToUniTask();

        public async UniTask<TAsset> LoadAsync<TAsset>(string key) where TAsset : class
        {
            if (!assetRequests.TryGetValue(key, out AsyncOperationHandle handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                assetRequests.Add(key, handle);
            }

            await handle.ToUniTask();

            return handle.Result as TAsset;
        }

        public async UniTask<TAsset> LoadAsync<TAsset>(AssetReference assetReference) where TAsset : class =>
            await LoadAsync<TAsset>(assetReference.AssetGUID);

        public async UniTask<List<string>> GetAssetsListByLabelAsync<TAsset>(string label) =>
            await GetAssetsListByLabelAsync(label, typeof(TAsset));

        public async UniTask<List<string>> GetAssetsListByLabelAsync(string label, Type type = null)
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(label, type);

            var locations = await operationHandle.ToUniTask();

            var assetKeys = new List<string>(locations.Count);

            foreach (var location in locations)
                assetKeys.Add(location.PrimaryKey);

            Addressables.Release(operationHandle);
            return assetKeys;
        }

        public async UniTask<TAsset[]> LoadAllAsync<TAsset>(List<string> keys) where TAsset : class
        {
            var tasks = new List<UniTask<TAsset>>(keys.Count);

            foreach (var key in keys)
                tasks.Add(LoadAsync<TAsset>(key));

            return await UniTask.WhenAll(tasks);
        }

        public async UniTask WarmupAssetsByLabelAsync(string label)
        {
            var assetsList = await GetAssetsListByLabelAsync(label);
            await LoadAllAsync<object>(assetsList);
        }

        public async UniTask ReleaseAssetsByLabelAsync(string label)
        {
            var assetsList = await GetAssetsListByLabelAsync(label);

            foreach (var assetKey in assetsList)
            {
                if (assetRequests.TryGetValue(assetKey, out var handler))
                {
                    Addressables.Release(handler);
                    assetRequests.Remove(assetKey);
                }
            }
        }

        public void Cleanup()
        {
            foreach (var assetRequest in assetRequests)
                Addressables.Release(assetRequest.Value);

            assetRequests.Clear();
        }
    }
}