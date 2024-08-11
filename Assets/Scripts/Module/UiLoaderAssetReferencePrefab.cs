using System;
using System.Collections;
using Core.UiService;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using Object = System.Object;

namespace Module.UI
{
    public class UiLoaderAssetReferencePrefab : IUiLoader
    {
        [Inject]
        private DiContainer diContainer;
        
        public IEnumerator LoadUi(IUiDefinition uiDefinition, Action<IUiDefinition, UiPresenter> callback)
        {
            AssetReferenceUiDefinition assetReferenceUiDefinition = (AssetReferenceUiDefinition) uiDefinition;

            AsyncOperationHandle<GameObject> resourceOperation = Addressables.LoadAssetAsync<GameObject>(assetReferenceUiDefinition.AssetReference);
            yield return resourceOperation;

            if (resourceOperation.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError("Could not load: " + uiDefinition.ToString() + " Check UiPrefabCatalogue for missing references.");
            }

            GameObject gameObject = diContainer.InstantiatePrefab(resourceOperation.Result);

            UiPresenter uiPresenter = gameObject.GetComponent<UiPresenter>();

            if (uiPresenter == null)
            {
                GameObject.Destroy(gameObject);
                Debug.LogError("The loaded GameObject for definition " + uiDefinition.ToString() + " does not have a UiPresenter component");
            }

            callback(uiDefinition, uiPresenter);
        }

        public void UnloadUi(UiPresenter uiPresenter)
        {
            GameObject.Destroy(uiPresenter.gameObject);
        }
        
        public UiPresenter Instantiate(UiPresenter uiPresenter)
        {
            GameObject go = diContainer.InstantiatePrefab(uiPresenter.gameObject);
            return go.GetComponent<UiPresenter>();
        }
    }
}