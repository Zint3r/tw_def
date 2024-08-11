using Core.Actions;
using Core.CoroutineProvider;
using Core.UiService;
using Game.UI;
using System;
using System.Collections;
using Zenject;

namespace Game.GameState
{
	public class LoadMapAction : IAction
	{
		public event Action<string> OnComplete;
		public event Action<string> OnFail;

		[Inject]
		private ICoroutineService coroutineService;

		[Inject]
		private IUiService uiService;

		public void Destroy()
		{
			
		}

		public void Execute()
		{
			coroutineService.StartCoroutine(Initialize());
		}

		private IEnumerator Initialize()
		{
			yield return LoadUI();
			OnComplete?.Invoke(string.Empty);
		}

		private IEnumerator LoadUI()
		{
			//yield return levelViewConfigurationFactory.Initialize();
			yield return uiService.OpenUiAsync(UiViews.MainControlPanel, UiLayers.Panels);
			yield return uiService.OpenUiAsync(UiViews.TopInterfacePanel, UiLayers.Panels);
			yield return uiService.OpenUiAsync(UiViews.StatsControlPanel, UiLayers.Panels);
			yield return uiService.OpenUiAsync(UiViews.QuickBuildingPanel, UiLayers.Panels);

			//yield return mapService.OpenMapAsync(MapViews.MapPresenter, MapLayers.MapPresenters);
		}
	}
}