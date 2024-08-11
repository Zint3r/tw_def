using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.AssetCatalogue
{
    [CreateAssetMenu(fileName = nameof(UiConfigurationsCatalogue), menuName = "AssetCatalogue/" + nameof(UiConfigurationsCatalogue))]
    public class UiConfigurationsCatalogue : ScriptableObject
    {
        public AssetReference ManagerRarityColorConfiguration;
        public AssetReference UiElementColorConfiguration;
    }
}