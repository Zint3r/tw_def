using Game.AssetCatalogue;
using Module.UI;

namespace Game.UI
{
	public static class UiViews
	{
		public static readonly AssetReferenceUiDefinition LoadingScreen = new AssetReferenceUiDefinition("LoadingScreen");

		//public static readonly AssetReferenceUiDefinition DebugPanel = new AssetReferenceUiDefinition("DebugPanel");
		//
		//Panels MapSelectPanel
		public static readonly AssetReferenceUiDefinition MapSelectPanel = new AssetReferenceUiDefinition("MapSelectPanel");
		public static readonly AssetReferenceUiDefinition TopInterfacePanel = new AssetReferenceUiDefinition("TopInterfacePanel");
		public static readonly AssetReferenceUiDefinition MenuPanel = new AssetReferenceUiDefinition("MenuPanel");
		public static readonly AssetReferenceUiDefinition MainControlPanel = new AssetReferenceUiDefinition("MainControlPanel");
		public static readonly AssetReferenceUiDefinition StatsControlPanel = new AssetReferenceUiDefinition("StatsControlPanel");
		public static readonly AssetReferenceUiDefinition QuickBuildingPanel = new AssetReferenceUiDefinition("QuickBuildingPanel");

		public static void Init(UiPrefabsCatalogue uiPrefabCatalogue)
		{
			//Loading
			LoadingScreen.AssetReference = uiPrefabCatalogue.LoadingScreen;

			//Panels
			MapSelectPanel.AssetReference = uiPrefabCatalogue.MapSelectPanel;
			TopInterfacePanel.AssetReference = uiPrefabCatalogue.TopInterfacePanel;
			MenuPanel.AssetReference = uiPrefabCatalogue.MenuPanel;
			MainControlPanel.AssetReference = uiPrefabCatalogue.MainControlPanel;
			StatsControlPanel.AssetReference = uiPrefabCatalogue.StatsControlPanel;
			QuickBuildingPanel.AssetReference = uiPrefabCatalogue.QuickBuildingPanel;

			////Debug
			//DebugPanel.AssetReference = uiPrefabCatalogue.DebugPanel;
		}
	}
}
