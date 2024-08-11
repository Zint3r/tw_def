using System;
using System.Collections;
using Core.Helper;
using Game.AssetCatalogue;
using Game.Features.Level.Configuration;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Features.Level.Factories
{
    public class LevelEnvironmentFactory
    {
        [Inject]
        private LevelViewSettingsCatalogue levelViewSettingsCatalogue;
        
        public IEnumerator CreateLevelSegmentBackground(string levelId, EntitySegmentId segmentId, Action<GameObject> completionCallback)
        {
            var assetLocation = levelViewSettingsCatalogue.GetBackgroundPrefabReference(levelId, segmentId);
            yield return AddressablesHelper.LoadAssetAsync<GameObject>(assetLocation, operation =>
            {
                if (operation.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Could not find Addressable level background asset for reference: {assetLocation.RuntimeKey}");
                }

                var backgroundInstance = Object.Instantiate(operation.Result);
                completionCallback.Invoke(backgroundInstance);
            });
        }
    }
}