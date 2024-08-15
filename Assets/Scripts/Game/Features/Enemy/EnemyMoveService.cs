using Core.CoroutineProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Features.Enemy
{
	public interface IEnemyMoveService
	{

	}
	public class EnemyMoveService : IEnemyMoveService, IInitializable, IDisposable
	{
		[Inject]
		private EnemyesModel enemyesModel;

		[Inject]
		private ICoroutineService coroutineService;

		private WaitForSeconds waitForMoveEnemy = new WaitForSeconds(Time.fixedDeltaTime);

		public void Initialize()
		{
			coroutineService.StartCoroutine(EnemyMove());
		}
		public void Dispose()
		{
			coroutineService.StopCoroutine(EnemyMove());
		}
		private void Move(EnemyStatsInfo statsInfo)
		{
			if (statsInfo.EnemyVO.IsAlive == true)
			{
				if (statsInfo.NavMeshAgent.hasPath == false && statsInfo.EnemyVO.InControl == false)
				{
					if (statsInfo.EnemyVO.MoveIndex <= statsInfo.EnemyVO.MovePoints.Count - 1)
					{
						statsInfo.NavMeshAgent.destination = statsInfo.EnemyVO.MovePoints[statsInfo.EnemyVO.MoveIndex].PointPosition;
						statsInfo.EnemyVO.MoveIndex++;
					}
					else
					{
						enemyesModel.RemoveEnemyOnScene(statsInfo);
					}
				}
			}
		}
		private IEnumerator EnemyMove()
		{
			while (true)
			{
				List<EnemyStatsInfo> enemyStatsInfos = enemyesModel.GetAllEnemyesOnScene();
				for (int i = 0; i < enemyStatsInfos.Count; i++)
				{
					Move(enemyStatsInfos[i]);
				}
				yield return waitForMoveEnemy;
			}
		}
	}
}