using Core.Actions;
using Core.CoroutineProvider;
using Core.Helper;
using Core.UiService;
using Game.Features.GameDesign.Parsers;
using Game.UI;
using Game.UI.Factories;
using Game.UI.Loading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.GameState
{
	public class BootStateAction : IAction
    {
        public const string FirstAppStartPlayerPrefsKey = "FirstAppStartPlayerPrefsKey";
        
        public event Action<string> OnComplete;
        public event Action<string> OnFail;
     
        [Inject]
        private ICoroutineService coroutineService;
        
        [Inject]
        private IUiService uiService;
        
        [Inject]
        private InitialLoadingScreenPresenter initialLoadingScreenPresenter;

		[Inject]
		private LocalizationDefinitionParser localizationDefinitionParser;

		[Inject]
		private UiConfigurationFactory uiConfigurationFactory;

		[Inject]
		private IconFactory iconFactory;

		[Inject]
		private GameObjectFactory gameObjectFactory;

		[Inject]
        private ResourceDefinitionParser resourceDefinitionParser;

		[Inject]
		private HeroCollectionDefinitionParser heroCollectionDefinitionParser;

		[Inject]
		private MapDefinitionParser mapDefinitionParser;

		[Inject]
		private MovePointsDefinitionParser movePointsDefinitionParser;

		[Inject]
		private EnemyDefinitionParser enemyDefinitionParser;

		public void Execute()
        {
            coroutineService.StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            yield return InitAddressables();
			yield return LoadDefinitions();
			yield return InitLoadingScreen();
            yield return PreloadUiViews();
            yield return PreloadGameObjects();
			//yield return LoadGameplay();

			if (!PlayerPrefs.HasKey(FirstAppStartPlayerPrefsKey))
            {
                PlayerPrefs.SetInt(FirstAppStartPlayerPrefsKey, 1);
            }
            
            uiService.CloseUi(UiViews.LoadingScreen, UiLayers.InitialLoading);
            OnComplete?.Invoke(string.Empty);
        }

        private IEnumerator InitAddressables()
        {
            while (!Caching.ready)
            {
                yield return null;
            }

            yield return Addressables.InitializeAsync();

            Debug.Log("Addressables initialized");
        }
        
        private IEnumerator InitLoadingScreen()
        {
            yield return uiService.OpenUiAsync(UiViews.LoadingScreen, UiLayers.InitialLoading);
            initialLoadingScreenPresenter.Destroy();
        }
        
        private IEnumerator PreloadUiViews()
        {
            yield return CoroutineHelper.RunInParallel(new List<IEnumerator>()
            {
                iconFactory.Initialize(),                
                uiConfigurationFactory.Initialize()
            });
			
            Debug.Log("PreloadAssets complete");
        }

		private IEnumerator PreloadGameObjects()
		{
			yield return CoroutineHelper.RunInParallel(new List<IEnumerator>()
			{
				gameObjectFactory.Initialize()
			});

			Debug.Log("PreloadGameObjects complete");
		}

		private IEnumerator LoadDefinitions()
        {
            //Order matter, from bottom to top
            yield return CoroutineHelper.RunInParallel(new List<IEnumerator>()
            {
				localizationDefinitionParser.Initialize(),
                heroCollectionDefinitionParser.Initialize(),
                resourceDefinitionParser.Initialize(),
				mapDefinitionParser.Initialize(),
				movePointsDefinitionParser.Initialize(),
				enemyDefinitionParser.Initialize(),
			});
        }

        private IEnumerator LoadGameplay()
        {
            yield return CoroutineHelper.RunInParallel(new List<IEnumerator>()
            {
                //heroFactory.Initialize()
            });
            Debug.Log("Loading Heroes Assets complete");
        }

        public void Destroy()
        {
            
        }        
    }
}