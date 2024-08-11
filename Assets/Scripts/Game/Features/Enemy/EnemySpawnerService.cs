using Core.CoroutineProvider;
using Core.MessageHub;
using Game.Features.Actions;
using Game.Utils;
using Module.DateTimeProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Features.Enemy
{
	public interface IEnemySpawnerService
	{
		void InitializeEnemyWaves(Dictionary<string, List<EnemyWaveVO>> enemyWaves);
		void EnableSpawner(bool enable);
	}

	public class EnemySpawnerService : IEnemySpawnerService, IInitializable, IDisposable
	{
		[Inject]
		private EnemyesModel enemyesModel;

		[Inject]
		private ICoroutineService coroutineService;

		[Inject]
		private IMessageHubService messageHubService;

		[Inject]
		private IDateTimeProvider dateTimeProvider;

		[Inject]
		private ActionLocator actionLocator;

		public void Initialize()
		{
			messageHubService.Subscribe<EnemyMessages.SpawnEnableMessage>(StartSpawn);
		}

		public void InitializeEnemyWaves(Dictionary<string, List<EnemyWaveVO>> enemyWaves)
		{
			enemyesModel.EnemyWaves = enemyWaves;
		}
		private void StartSpawn(EnemyMessages.SpawnEnableMessage message)
		{
			coroutineService.StartCoroutine(SpawnEnemy());
		}
		private IEnumerator SpawnEnemy()
		{
			string mapName = DPL.MapCreatorDataProvider.GetSelectedMap();

			List<EnemyWaveVO> enemyWaves = enemyesModel.GetWavesByMapName(mapName);
			for (int i = 0; i < enemyWaves.Count; i++)
			{
				EnemyWaveVO enemyWave = enemyWaves[i];
				if (enemyWave.WaveId != -1)
				{
					List<EnemyVO> enemyes = enemyWave.EnemyList;
					foreach (EnemyVO enemy in enemyes)
					{
						EnemyVO enemyTemp = enemy;
						enemyTemp.Id = enemyesModel.CurrentUniqueEnemyID;
						enemyesModel.CurrentUniqueEnemyID++;
						messageHubService.Publish(new EnemyMessages.SpawnMessage(enemyTemp));
						enemyesModel.AddEnemyToScene(enemyTemp);
						yield return new WaitForSeconds(enemyWave.SpawnTimer);
					}
				}
				yield return TimerToNewWave();
			}

			//EnemySpawnEnablerParams args = new EnemySpawnEnablerParams(true);
			//if (!actionLocator.EnemySpawnEnablerAction.CanExecute(args, dateTimeProvider.UtcNow, out string errMessage))
			//{
			//	UnityEngine.Debug.LogWarning(errMessage);
			//	return;
			//}
			//actionLocator.EnemySpawnEnablerAction.Execute(args);
		}

		private IEnumerator TimerToNewWave()
		{			
			for (int timer = 10; timer >= 0; timer--)
			{
				//Debug.Log(sizeTimer);
				messageHubService.Publish(new EnemyMessages.TimerToNewWaveMessage(timer));
				enemyesModel.TimerToNewWave = timer;
				yield return new WaitForSeconds(1.0f);
			}
		}

		public void EnableSpawner(bool enable)
		{
			enemyesModel.IsSpawnEnable = enable;
		}

		public void Dispose()
		{
			coroutineService.StopCoroutine(SpawnEnemy());
		}
	}
}