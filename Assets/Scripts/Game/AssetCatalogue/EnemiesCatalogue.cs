using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.AssetCatalogue
{
	[Serializable]
	public class EnemyPrefabReference
	{
		public string EnemyDefinitionId;
		public AssetReference EnemyPrefabReferences;
	}

	[CreateAssetMenu(fileName = "NewEnemiesCatalogue", menuName = "AssetCatalogue/EnemiesCatalogue", order = 0)]
	public class EnemiesCatalogue : ScriptableObject
	{
		public List<EnemyPrefabReference> EnemiesPrefabReferences;

		public List<AssetReference> GetAllEnemies()
		{
			List<AssetReference> enemies = new List<AssetReference>();
			foreach (var enemy in EnemiesPrefabReferences)
			{
				enemies.Add(enemy.EnemyPrefabReferences);
			}
			return enemies;
		}

		public AssetReference GetEnemyPrefabReference(string definitionId)
		{
			return EnemiesPrefabReferences.FirstOrDefault(x => x.EnemyDefinitionId == definitionId)?.EnemyPrefabReferences;
		}
	}
}