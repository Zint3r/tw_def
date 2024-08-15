using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Features.Enemy
{
	public interface IEnemyDataProvider
	{
		List<EnemyStatsInfo> GetAllEnemyesOnScene();
		EnemyStatsInfo GetEnemyById(int id);
		EnemyStatsInfo GetEnemyByTransform(Transform transform);
		bool GetSpawnEnable();
	}
	public interface IEnemyWavesDataProvider
	{
		List<EnemyWaveVO> GetWavesByMapName(string mapName);
		EnemyWaveVO GetNextWave(string mapName);
	}
	public class EnemyStatsInfo
	{
		public Transform EnemyTransform;
		public EnemyPresenter EnemyPresenter;
		public NavMeshAgent NavMeshAgent;
		public EnemyVO EnemyVO = EnemyVO.Empty;
	}
	public class EnemyesModel : IEnemyDataProvider, IEnemyWavesDataProvider
	{
		public Dictionary<int, EnemyStatsInfo> EnemyesOnScene = new Dictionary<int, EnemyStatsInfo>();
		public List<EnemyVO> Enemies = new List<EnemyVO>();
		public Dictionary<string, List<EnemyWaveVO>> EnemyWaves = new Dictionary<string, List<EnemyWaveVO>>();

		public EnemyWaveVO CurrentWaveVO;
		public int CurrentWaveIndex = 0;
		public bool IsSpawnEnable = false;
		public int TimerToNewWave = 0;
		private int CurrentWavesCount = 0;
		public int CurrentUniqueEnemyID = 1;

		public void AddEnemyToScene(EnemyStatsInfo enemy)
		{
			EnemyesOnScene.Add(enemy.EnemyVO.Id, enemy);
		}
		public List<EnemyStatsInfo> GetAllEnemyesOnScene()
		{			
			return EnemyesOnScene.Values.ToList();
		}
		public EnemyStatsInfo GetEnemyById(int id)
		{			
			return EnemyesOnScene[id];
		}
		public EnemyStatsInfo GetEnemyByTransform(Transform transform)
		{
			return EnemyesOnScene.Values.FirstOrDefault(x => x.EnemyTransform.Equals(transform));
		}
		public void RemoveEnemyOnScene(int id)
		{
			EnemyesOnScene.Remove(id);
		}
		public void RemoveEnemyOnScene(EnemyStatsInfo enemyStatsInfo)
		{
			EnemyesOnScene.Remove(enemyStatsInfo.EnemyVO.Id);
		}
		public EnemyVO GetRandomEnemy()
		{
			int index = Enemies.Count;
			int random = Random.Range(0, index);
			EnemyVO enemy = Enemies[random];
			return enemy;
		}
		public List<EnemyWaveVO> GetWavesByMapName(string mapName)
		{
			EnemyWaves.TryGetValue(mapName, out List<EnemyWaveVO> waves);
			return waves;
		}
		public EnemyWaveVO GetNextWave(string mapName)
		{			
			List<EnemyWaveVO> enemyWaves = GetWavesByMapName(mapName);
			EnemyWaveVO enemyWave = enemyWaves[CurrentWaveIndex];
			if (CurrentWavesCount == 0)
			{
				CurrentWavesCount = enemyWaves.Count;
			}
			if (CurrentWaveIndex <= enemyWaves.Count - 1)
			{
				CurrentWaveIndex++;
				return enemyWave;
			}
			else
			{
				return EnemyWaveVO.Empty;
			}
		}
		public int GetWavesCount()
		{
			return CurrentWavesCount;
		}
		public bool GetSpawnEnable()
		{
			return IsSpawnEnable;
		}
	}
}