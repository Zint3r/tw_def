using Game.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.AssetCatalogue
{
	[CreateAssetMenu(fileName = "New UiPrefabsCatalogue", menuName = "AssetCatalogue/UiPrefabsCatalogue")]
    public class UiPrefabsCatalogue : ScriptableObject, IInitializable
    {
        [Header("Loading")]
        public AssetReference LoadingScreen;

		[Header("Panels")]
		public AssetReference MapSelectPanel;
		public AssetReference TopInterfacePanel;
		public AssetReference MenuPanel;
		public AssetReference MainControlPanel;
		public AssetReference StatsControlPanel;
		public AssetReference QuickBuildingPanel;

		public void Initialize()
        {
            UiViews.Init(this);
        }
    }
}