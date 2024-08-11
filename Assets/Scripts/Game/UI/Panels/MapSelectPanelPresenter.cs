using Core.MessageHub;
using Core.UiService;
using Game.Features.HeroBuilding;
using Game.Features.MapCreator;
using Game.GameState;
using Game.GameState.MapState;
using Game.UI.Factories;
using Game.Utils;
using Module.GameObjectInstaller.Pool;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Panels
{
	public class MapSelectPanelPresenter : UiPresenter
	{
		[SerializeField]
		private Button _startButton;

		[SerializeField]
		private MapSelectPanelView _mapSelectPanelView;

		private MapVO _mapVO = MapVO.Empty;

		[Inject]
		private IGameStateChart gameStateChart;

		[Inject]
		private IMessageHubService messageHubService;

		[Inject]
		private GameObjectFactory gameObjectFactory;

		[Inject]
		private GoPool _pool;

		protected override void OnLoaded()
		{
			_startButton.interactable = false;
			_mapSelectPanelView.SetMapsData(DPL.MapCreatorDataProvider.GetMapsList());
			_startButton.onClick.AddListener(StartMap);
			messageHubService.Subscribe<MapCreatorMessages.MapCreatorMessage>(OnSelectMap);
		}

		protected override void OnUnloaded()
		{
			_startButton.onClick.RemoveListener(StartMap);
			messageHubService.Unsubscribe<MapCreatorMessages.MapCreatorMessage>(OnSelectMap);
		}

		public void StartMap()
		{
			Debug.Log($"Loading {_mapVO.MapDefinition.DefinitionId} map");			
			GameObject mapGO = gameObjectFactory.GetGameObjectByDefinition(_mapVO.MapDefinition);
			GameObject map = _pool.GetPooledPrefab(mapGO, new Vector3(0, 0, 0), Quaternion.identity);
			map.GetComponent<HeroBuildingPresenter>().OnStart();
			map.GetComponent<QuickBuildingPresenter>().OnStart();
			GameObject playerGO = gameObjectFactory.GetPlayerGameObject();
			GameObject player = _pool.GetPooledPrefab(playerGO, new Vector3(0, 0, 0), Quaternion.identity);
			gameStateChart.ExecuteStateTrigger(MapStateTrigger.Open);
		}

		private void OnSelectMap(MapCreatorMessages.MapCreatorMessage message)
		{
			_mapVO = message.SelectedMapVO;
			_startButton.interactable = true;
		}
	}
}