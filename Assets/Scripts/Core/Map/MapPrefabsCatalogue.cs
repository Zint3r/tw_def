using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Core.Map
{
    [CreateAssetMenu(fileName = "New MapPrefabsCatalogue", menuName = "AssetCatalogue/MapPrefabsCatalogue")]
    public class MapPrefabsCatalogue : ScriptableObject, IInitializable
    {
        [Header("Map")]
        public AssetReference MapPresenter;
        
        public void Initialize()
        {
            MapViews.Init(this);
        }
    }
}