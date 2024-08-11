using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.AssetCatalogue
{
	[Serializable]
	public class AttackEffectPrefabReference
	{
		public string DefinitionId;
		public AssetReference PrefabReference;
	}

	[CreateAssetMenu(fileName = "New AttackEffectCatalogue", menuName = "AssetCatalogue/AttackEffectCatalogue")]
    public class AttackEffectCatalogue : ScriptableObject
	{
        [Header("AttackEffect list")]
		public List<AttackEffectPrefabReference> AttackEffectPrefabReferences;
		public AssetReference GetAttackEffectPrefabReference(string definitionId)
		{
			return AttackEffectPrefabReferences.FirstOrDefault(x => x.DefinitionId == definitionId)?.PrefabReference;
		}
	}
}