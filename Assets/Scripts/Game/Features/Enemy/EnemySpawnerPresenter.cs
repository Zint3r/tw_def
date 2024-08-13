using Core.MessageHub;
using Core.UiService;
using Game.Features.Actions;
using Game.UI.Widgets.Misc;
using Module.DateTimeProvider;
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

		[SerializeField]
		private Button startSpawnButton;

		[SerializeField]
		private AssetLabelWidget spawnEnablbeWidget;

		[SerializeField]
		private TextMeshProUGUI timerToNewWave;
		
		protected override void OnLoaded()
		{
			spawnEnablbeWidget.Init("Spawn disable");
			startSpawnButton.onClick.AddListener(OnActivateClick);
			messageHubService.Subscribe<EnemyMessages.SpawnMessage>(SpawnEnemyGO);
			messageHubService.Subscribe<EnemyMessages.TimerToNewWaveMessage>(ChangeTimerToNewWaveText);
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
				Debug.LogWarning(errMessage);
				return;
			}			
			actionLocator.EnemySpawnEnablerAction.Execute(args);
			spawnEnablbeWidget.Init("Spawn enable");
		}
		public void SpawnEnemyGO(EnemyMessages.SpawnMessage message)
		{
			
		}
		public void ChangeTimerToNewWaveText(EnemyMessages.TimerToNewWaveMessage message)
		{
			timerToNewWave.text = $"To next wave - {message.Timer} sec";
		}
	}
}