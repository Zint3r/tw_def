using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.AssetCatalogue
{
	[Serializable]
	public class HeroPrefabReference
	{
		public string HeroDefinitionId;
		public AssetReference HeroPrefabReferences;
	}

	[CreateAssetMenu(fileName = "NewHeroesCatalogue", menuName = "AssetCatalogue/HeroesCatalogue", order = 0)]
	public class HeroesCatalogue : ScriptableObject
	{
		public List<HeroPrefabReference> HeroesPrefabReferences;

		public List<AssetReference> GetAllMaps()
		{
			List<AssetReference> heroes = new List<AssetReference>();
			foreach (var hero in HeroesPrefabReferences)
			{
				heroes.Add(hero.HeroPrefabReferences);
			}
			return heroes;
		}

		public AssetReference GetEnemyPrefabReference(string definitionId)
		{
			return HeroesPrefabReferences.FirstOrDefault(x => x.HeroDefinitionId == definitionId)?.HeroPrefabReferences;
		}
	}
}