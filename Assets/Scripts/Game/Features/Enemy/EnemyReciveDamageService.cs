using Core.MessageHub;
using Game.Features.Actions;
using Module.DateTimeProvider;
using System;
using UnityEngine;
using Zenject;
using static Game.Features.Enemy.EnemyMessages;

namespace Game.Features.Enemy
{
	public interface IEnemyReciveDamageService
	{		
		void EnemyReciveDamage(EnemyReciveDamageMessage message);
	}

	public class EnemyReciveDamageService : IEnemyReciveDamageService, IInitializable, IDisposable
	{
		[Inject]
		private EnemyesModel enemyesModel;

		[Inject]
		private IMessageHubService messageHubService;

		[Inject]
		private IDateTimeProvider dateTimeProvider;

		[Inject]
		private ActionLocator actionLocator;

		public void Initialize()
		{
			messageHubService.Subscribe<EnemyReciveDamageMessage>(EnemyReciveDamage);
		}
		public void Dispose()
		{
			messageHubService.Unsubscribe<EnemyReciveDamageMessage>(EnemyReciveDamage);
		}

		public void EnemyReciveDamage(EnemyReciveDamageMessage message)
		{
			if (enemyesModel.EnemyesOnScene.TryGetValue(message.EnemyID, out EnemyVO enemyVO))
			{
				int enemyHp = enemyVO.Hp;
				if (enemyHp - message.Damage > 0)
				{
					enemyVO.Hp -= message.Damage;
					enemyesModel.EnemyesOnScene[message.EnemyID] = enemyVO;
					
					//Debug.Log(enemyVO.Hp);
				}
				else
				{
					enemyesModel.EnemyesOnScene.Remove(message.EnemyID);
					messageHubService.Publish(new EnemyDeadMessage(enemyVO.Id));
				}
			}
			//Debug.Log("Enemy " + message.EnemyID.ToString() + " take " + message.Damage.ToString() + " damage");
			//messageHubService.Publish(new EnemyMessages.EnemyReciveDamageMessage(damage, enemyId));

			//return LocalizationVO.Empty;
			//enemyesModel.EnemyesOnScene.ContainsKey
		}
		//private IEnumerator SpawnEnemy()
		//{
		//	string mapName = DPL.MapCreatorDataProvider.GetSelectedMap();
		//
		//	List<EnemyWaveVO> enemyWaves = enemyesModel.GetWavesByMapName(mapName);
		//	for (int i = 0; i < enemyWaves.Count; i++)
		//	{
		//		EnemyWaveVO enemyWave = enemyWaves[i];
		//		if (enemyWave.WaveId != -1)
		//		{
		//			List<EnemyVO> enemyes = enemyWave.EnemyList;
		//			foreach (EnemyVO enemy in enemyes)
		//			{
		//				EnemyVO enemyTemp = enemy;
		//				enemyTemp.Id = enemyesModel.CurrentUniqueEnemyID;
		//				enemyesModel.CurrentUniqueEnemyID++;
		//				messageHubService.Publish(new EnemyMessages.SpawnMessage(enemyTemp));
		//				enemyesModel.AddEnemyToScene(enemyTemp);
		//				yield return new WaitForSeconds(enemyWave.SpawnTimer);
		//			}
		//		}
		//		yield return TimerToNewWave();
		//	}
		//
		//	//EnemySpawnEnablerParams args = new EnemySpawnEnablerParams(true);
		//	//if (!actionLocator.EnemySpawnEnablerAction.CanExecute(args, dateTimeProvider.UtcNow, out string errMessage))
		//	//{
		//	//	UnityEngine.Debug.LogWarning(errMessage);
		//	//	return;
		//	//}
		//	//actionLocator.EnemySpawnEnablerAction.Execute(args);
		//}
	}
}