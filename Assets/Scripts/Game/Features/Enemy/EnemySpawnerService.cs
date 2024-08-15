using Core.CoroutineProvider;
using Core.MessageHub;
using Game.UI.Factories;
using Game.Utils;
using Module.GameObjectInstaller.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
		private GameObjectFactory gameObjectFactory;

		[Inject]
		private GoPool pool;

		public void Initialize()
		{
			messageHubService.Subscribe<EnemyMessages.SpawnEnableMessage>(StartSpawn);
			messageHubService.Subscribe<EnemyMessages.EnemyDeadMessage>(RemoveEnemy);
		}
		public void Dispose()
		{
			messageHubService.Unsubscribe<EnemyMessages.SpawnEnableMessage>(StartSpawn);
			messageHubService.Unsubscribe<EnemyMessages.EnemyDeadMessage>(RemoveEnemy);
			coroutineService.StopCoroutine(SpawnEnemy());
		}
		public void InitializeEnemyWaves(Dictionary<string, List<EnemyWaveVO>> enemyWaves)
		{
			enemyesModel.EnemyWaves = enemyWaves;
		}
		private void StartSpawn(EnemyMessages.SpawnEnableMessage message)
		{
			coroutineService.StartCoroutine(SpawnEnemy());
		}
		public void RemoveEnemy(EnemyMessages.EnemyDeadMessage message)
		{
			EnemyPresenter enemyPresenter = enemyesModel.GetEnemyById(message.EnemyId).EnemyPresenter;
			enemyPresenter.EnemyDead();
			enemyesModel.RemoveEnemyOnScene(message.EnemyId);
		}
		private IEnumerator SpawnEnemy()
		{
			string mapName = DPL.MapCreatorDataProvider.GetSelectedMap();
			bool enableSpawn = DPL.EnemyDataProvider.GetSpawnEnable();
			List<EnemyWaveVO> enemyWaves = enemyesModel.GetWavesByMapName(mapName);

			for (int i = 0; i < enemyWaves.Count; i++)
			{
				EnemyWaveVO enemyWave = enemyWaves[i];
				if (enemyWave.WaveId != -1 && enableSpawn == true)
				{
					List<EnemyVO> enemyes = enemyWave.EnemyList;
					foreach (EnemyVO enemy in enemyes)
					{
						EnemyVO enemyTemp = enemy;
						enemyTemp.Id = enemyesModel.CurrentUniqueEnemyID;
						enemyesModel.CurrentUniqueEnemyID++;
						GameObject go = gameObjectFactory.GetGameObjectByDefinition(enemyTemp.EnemyDefinition);
						GameObject enemyGO = pool.GetPooledPrefab(go, enemyTemp.MovePoints[0].PointPosition, Quaternion.identity);
						enemyGO.transform.position = enemyTemp.MovePoints[0].PointPosition;

						if (enemyGO.TryGetComponent(out EnemyPresenter enemyPresenter) == true)
						{
							EnemyStatsInfo newEnemy = new EnemyStatsInfo();
							newEnemy.EnemyTransform = enemyGO.transform;
							newEnemy.EnemyPresenter = enemyGO.transform.GetComponent<EnemyPresenter>();
							newEnemy.NavMeshAgent = enemyGO.transform.GetComponent<NavMeshAgent>();
							newEnemy.NavMeshAgent.speed = enemyTemp.Speed;
							newEnemy.EnemyVO = enemyTemp;

							enemyesModel.AddEnemyToScene(newEnemy);
							enemyPresenter.OnStart();
							messageHubService.Publish(new EnemyMessages.SpawnMessage(enemyTemp, enemyGO.transform));
						}
						yield return new WaitForSeconds(enemyWave.SpawnTimer);
					}
				}
				yield return TimerToNewWave();
			}
		}
		private IEnumerator TimerToNewWave()
		{			
			for (int timer = 10; timer >= 0; timer--)
			{
				messageHubService.Publish(new EnemyMessages.TimerToNewWaveMessage(timer));
				enemyesModel.TimerToNewWave = timer;
				yield return new WaitForSeconds(1.0f);
			}
		}
		public void EnableSpawner(bool enable)
		{
			enemyesModel.IsSpawnEnable = enable;
		}
	}
}