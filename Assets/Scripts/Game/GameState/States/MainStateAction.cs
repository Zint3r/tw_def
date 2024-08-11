using Core.Actions;
using Core.CoroutineProvider;
using Core.UiService;
using Game.UI;
using System;
using System.Collections;
using Zenject;

namespace Game.GameState
{
	public class MainStateAction : IAction
    {
        public event Action<string> OnComplete;
        public event Action<string> OnFail;
        
        [Inject]
        private ICoroutineService coroutineService;
        
        [Inject]
        private IUiService uiService;
        
        public void Execute()
        {
            coroutineService.StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            yield return OpenUI();
            yield return CreateMap();

			OnComplete?.Invoke(string.Empty);
        }

        private IEnumerator OpenUI()
        {
			yield return uiService.OpenUiAsync(UiViews.MapSelectPanel, UiLayers.Panels);
			//yield return uiService.OpenUiAsync(UiViews.TopInterfacePanel, UiLayers.Panels);
			//yield return uiService.OpenUiAsync(UiViews.MainControlPanel, UiLayers.Panels);
			
			// yield return uiService.PreloadUi(UiViews.DebugPanel, UiLayers.Panels);
			yield break;
		}

		private IEnumerator CreateMap()
        {
			yield break;
		}

		public void Destroy()
        {

        }
    }
}