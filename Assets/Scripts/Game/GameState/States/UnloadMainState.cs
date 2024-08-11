using System;
using System.Collections;
using Core.Actions;
using Core.CoroutineProvider;
using Core.UiService;
using Game.UI;
//using Game.UI;
using Zenject;

namespace Game.GameState
{
    public class UnloadMainState : IAction
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
            yield return CloseUI();
            
            OnComplete?.Invoke(string.Empty);
        }

        private IEnumerator CloseUI()
        {
			//uiService.CloseUi(UiViews.LoadingScreen, UiLayers.Panels);
			//uiService.CloseUi(UiViews.TopInterfacePanel, UiLayers.Panels);
			//uiService.CloseUi(UiViews.MenuPanel, UiLayers.Panels);
			uiService.CloseUi(UiViews.MapSelectPanel, UiLayers.Panels);
			//uiService.CloseUi(UiViews.FooterInterfacePanel, UiLayers.Panels);
			//uiService.CloseUi(UiViews.MainMenuPanel, UiLayers.Panels);
			//
			//uiService.CloseUi(UiViews.SelectAbbysHeroPopup, UiLayers.Popups);

			yield break;
        }

        public void Destroy()
        {
        }
    }
}