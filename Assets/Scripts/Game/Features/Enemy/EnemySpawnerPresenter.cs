using Core.MessageHub;
using Core.UiService;
using Game.Features.Actions;
using Game.UI.Factories;
using Game.UI.Widgets.Misc;
using Game.Utils;
using Module.DateTimeProvider;
using Module.GameObjectInstaller.Pool;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Features.Enemy
{
	public class EnemySpawnerPresenter : UiPresenter
	{
		[Inject]
		private IMessageHubService messageHubService;

		[Inject]
		private IDateTimeProvider dateTimeProvider;

		[Inject]
		private ActionLocator actionLocator;

		[Inject]
		private GameObjectFactory gameObjectFactory;

		[Inject]
		private DiContainer container;

		[Inject]
		private GoPool _pool;

		[SerializeField]
		private Button startSpawnButton;

		[SerializeField]
		private AssetLabelWidget spawnEnablbeWidget;

		[SerializeField]
		private TextMeshProUGUI timerToNewWave;

		private List<EnemyPresenter> enemies = new List<EnemyPresenter>();
		protected override void OnLoaded()
		{
			spawnEnablbeWidget.Init("Spawn disable");			
			startSpawnButton.onClick.AddListener(OnActivateClick);
			messageHubService.Subscribe<EnemyMessages.SpawnMessage>(SpawnEnemyGO);
			messageHubService.Subscribe<EnemyMessages.TimerToNewWaveMessage>(ChangeTimerToNewWaveText);
			messageHubService.Subscribe<EnemyMessages.EnemyDeadMessage>(RemoveEnemy);
		}

		protected override void OnUnloaded()
		{
			messageHubService.Unsubscribe<EnemyMessages.SpawnMessage>(SpawnEnemyGO);
			messageHubService.Unsubscribe<EnemyMessages.TimerToNewWaveMessage>(ChangeTimerToNewWaveText);
			startSpawnButton.onClick.RemoveListener(OnActivateClick);
		}

		private void OnActivateClick()
		{			
			EnemySpawnEnablerParams args = new EnemySpawnEnablerParams(true);			
			if (!actionLocator.EnemySpawnEnablerAction.CanExecute(args, dateTimeProvider.UtcNow, out string errMessage))
			{
				UnityEngine.Debug.LogWarning(errMessage);
				return;
			}			
			actionLocator.EnemySpawnEnablerAction.Execute(args);
			spawnEnablbeWidget.Init("Spawn enable");
		}

		public void SpawnEnemyGO(EnemyMessages.SpawnMessage message)
		{
			bool enableSpawn = DPL.EnemyDataProvider.GetSpawnEnable();
			if (enableSpawn == true)
			{
				GameObject enemyGO = gameObjectFactory.GetGameObjectByDefinition(message.Enemy.EnemyDefinition);
				GameObject enemy = _pool.GetPooledPrefab(enemyGO, message.Enemy.MovePoints[0].PointPosition, Quaternion.identity);
				EnemyPresenter enemyPresenter = enemy.GetComponent<EnemyPresenter>();
				enemies.Add(enemyPresenter);
				enemyPresenter.OnStart(message.Enemy);
				//var go = container.InstantiatePrefab(enemyGO, message.Enemy.MovePoints[0].PointPosition, Quaternion.identity, null);
				//go.GetComponent<EnemyPresenter>().OnStart(message.Enemy);
				
			}
		}

		public void RemoveEnemy(EnemyMessages.EnemyDeadMessage message)
		{
			Debug.Log("RemoveEnemy");
			EnemyPresenter enemyPresenter = enemies.Where(i => i.EnemyId == message.EnemyId).FirstOrDefault();
			enemyPresenter.EnemyDead();
		}
		public void ChangeTimerToNewWaveText(EnemyMessages.TimerToNewWaveMessage message)
		{
			timerToNewWave.text = $"To next wave - {message.Timer} sec";
		}
	}
}