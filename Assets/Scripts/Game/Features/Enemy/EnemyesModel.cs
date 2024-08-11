using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Features.Enemy
{
	public interface IEnemyDataProvider
	{
		List<EnemyVO> GetAllEnemyesOnScene();
		EnemyVO GetEnemyById(int id);
		bool GetSpawnEnable();
	}

	public interface IEnemyWavesDataProvider
	{
		List<EnemyWaveVO> GetWavesByMapName(string mapName);
		EnemyWaveVO GetNextWave(string mapName);
	}

	public class EnemyesModel : IEnemyDataProvider, IEnemyWavesDataProvider
	{
		public Dictionary<int, EnemyVO> EnemyesOnScene = new Dictionary<int, EnemyVO>();
		//public List<EnemyVO> EnemyesOnScene = new List<EnemyVO>();
		public List<EnemyVO> Enemies = new List<EnemyVO>();
		public Dictionary<string, List<EnemyWaveVO>> EnemyWaves = new Dictionary<string, List<EnemyWaveVO>>();

		public EnemyWaveVO CurrentWaveVO;

		public int CurrentWaveIndex = 0;		

		public bool IsSpawnEnable = false;

		public int TimerToNewWave = 0;

		private int CurrentWavesCount = 0;

		public int CurrentUniqueEnemyID = 0;

		public List<EnemyVO> GetAllEnemyesOnScene()
		{			
			return EnemyesOnScene.Values.ToList();
		}
		public EnemyVO GetEnemyById(int id)
		{			
			return EnemyesOnScene[id];
		}
		public EnemyVO GetRandomEnemy()
		{
			int index = Enemies.Count;
			int random = Random.Range(0, index);
			EnemyVO enemy = Enemies[random];
			return enemy;
		}
		public void AddEnemyToScene(EnemyVO enemy)
		{			
			EnemyesOnScene.Add(enemy.Id, enemy);
		}
		public void SetEnemy(List<EnemyVO> enemies)
		{
			Enemies = enemies;
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
			if(CurrentWaveIndex <= enemyWaves.Count - 1)
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