using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.AssetCatalogue
{
	[Serializable]
	public class MapPrefabReference
	{
		public string MapDefinitionId;
		public AssetReference MapPrefabReferences;
	}

	[CreateAssetMenu(fileName = "NewMapsCatalogue", menuName = "AssetCatalogue/MapsCatalogue", order = 0)]
	public class MapsCatalogue : ScriptableObject
	{
		public List<MapPrefabReference> MapsPrefabReferences;

		public List<AssetReference> GetAllMaps()
		{
			List<AssetReference> maps = new List<AssetReference>();
			foreach (var map in MapsPrefabReferences)
			{
				maps.Add(map.MapPrefabReferences);
			}
			return maps;
		}

		public AssetReference GetMapPrefabReference(string definitionId)
		{
			return MapsPrefabReferences.FirstOrDefault(x => x.MapDefinitionId == definitionId)?.MapPrefabReferences;
		}


	}
}