using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Helper
{
    public static class AddressablesHelper
    {
        public static IEnumerator LoadAssetAsync<T>(AssetReference assetReference, Action<AsyncOperationHandle<T>> completeAction) where T : class
        {
            AsyncOperationHandle<T> resourceOperation = Addressables.LoadAssetAsync<T>(assetReference);
            resourceOperation.Completed += completeAction;
            yield return resourceOperation;
        }
    }
}