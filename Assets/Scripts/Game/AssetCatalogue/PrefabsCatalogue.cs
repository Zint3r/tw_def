using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.AssetCatalogue
{
	[CreateAssetMenu(fileName = "New PrefabsCatalogue", menuName = "AssetCatalogue/PrefabsCatalogue")]
    public class PrefabsCatalogue : ScriptableObject
    {
        [Header("Player")]
        public AssetReference Player;
	}
}