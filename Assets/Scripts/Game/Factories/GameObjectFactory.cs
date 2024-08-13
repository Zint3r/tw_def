using Core.Helper;
using Game.AssetCatalogue;
using Game.Features.GameDesign;
using Game.Features.GameDesign.DefinitionObjects.AttackEffect;
using Game.Features.GameDesign.DefinitionObjects.Enemyes;
using Game.Features.GameDesign.DefinitionObjects.Heroes;
using Game.Features.GameDesign.DefinitionObjects.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.UI.Factories
{
	public class GameObjectFactory
	{
		[Inject]
		private PrefabsCatalogue prefabsCatalogue;

		[Inject]
		private MapsCatalogue mapsCatalogue;

		[Inject]
		private EnemiesCatalogue enemiesCatalogue;

		[Inject]
		private HeroesCatalogue heroesCatalogue;

		[Inject]
		private AttackEffectCatalogue attackEffectCatalogue;

		private Dictionary<string, GameObject> mapsGo = new Dictionary<string, GameObject>();
		private Dictionary<string, GameObject> enemiesGo = new Dictionary<string, GameObject>();
		private Dictionary<string, GameObject> heroesGo = new Dictionary<string, GameObject>();
		private Dictionary<string, GameObject> attackEffectsGo = new Dictionary<string, GameObject>();
		private GameObject PlayerGO;

		public IEnumerator Initialize()
		{
			yield return CoroutineHelper.RunInParallel(new List<IEnumerator>
			{
				AddressablesHelper.LoadAssetAsync<GameObject>(prefabsCatalogue.Player, op => PlayerGO = op.Result),

				AddressablesHelper.LoadAssetAsync<GameObject>(mapsCatalogue.GetMapPrefabReference(MapConstants.Aden), op => { mapsGo.Add(MapConstants.Aden, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(mapsCatalogue.GetMapPrefabReference(MapConstants.Dion), op => { mapsGo.Add(MapConstants.Dion, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(mapsCatalogue.GetMapPrefabReference(MapConstants.Giran), op => { mapsGo.Add(MapConstants.Giran, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(mapsCatalogue.GetMapPrefabReference(MapConstants.Goddard), op => { mapsGo.Add(MapConstants.Goddard, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(mapsCatalogue.GetMapPrefabReference(MapConstants.Heine), op => { mapsGo.Add(MapConstants.Heine, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(mapsCatalogue.GetMapPrefabReference(MapConstants.Oren), op => { mapsGo.Add(MapConstants.Oren, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(mapsCatalogue.GetMapPrefabReference(MapConstants.Rune), op => { mapsGo.Add(MapConstants.Rune, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(mapsCatalogue.GetMapPrefabReference(MapConstants.Schuttgart), op => { mapsGo.Add(MapConstants.Schuttgart, op.Result); }),

				AddressablesHelper.LoadAssetAsync<GameObject>(enemiesCatalogue.GetEnemyPrefabReference("empty"), op => { enemiesGo.Add("empty", op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(enemiesCatalogue.GetEnemyPrefabReference("enemy_aden_type_1"), op => { enemiesGo.Add("enemy_aden_type_1", op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(enemiesCatalogue.GetEnemyPrefabReference("enemy_aden_type_2"), op => { enemiesGo.Add("enemy_aden_type_2", op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(enemiesCatalogue.GetEnemyPrefabReference("enemy_aden_type_3"), op => { enemiesGo.Add("enemy_aden_type_3", op.Result); }),

				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Human_Warrior), op => { heroesGo.Add(HeroCollectionConstants.Class_Human_Warrior, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Human_Knight), op => { heroesGo.Add(HeroCollectionConstants.Class_Human_Knight, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Human_Rogue), op => { heroesGo.Add(HeroCollectionConstants.Class_Human_Rogue, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Human_Wizard), op => { heroesGo.Add(HeroCollectionConstants.Class_Human_Wizard, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Human_Cleric), op => { heroesGo.Add(HeroCollectionConstants.Class_Human_Cleric, op.Result); }),
				
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Elven_Knight), op => { heroesGo.Add(HeroCollectionConstants.Class_Elven_Knight, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Elven_Scout), op => { heroesGo.Add(HeroCollectionConstants.Class_Elven_Scout, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Elven_Wizard), op => { heroesGo.Add(HeroCollectionConstants.Class_Elven_Wizard, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Elven_Oracle), op => { heroesGo.Add(HeroCollectionConstants.Class_Elven_Oracle, op.Result); }),
				
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Dark_Knight), op => { heroesGo.Add(HeroCollectionConstants.Class_Dark_Knight, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Dark_Assassin), op => { heroesGo.Add(HeroCollectionConstants.Class_Dark_Assassin, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Dark_Wizard), op => { heroesGo.Add(HeroCollectionConstants.Class_Dark_Wizard, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Dark_Oracle), op => { heroesGo.Add(HeroCollectionConstants.Class_Dark_Oracle, op.Result); }),
				
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Orc_Raider), op => { heroesGo.Add(HeroCollectionConstants.Class_Orc_Raider, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Orc_Monk), op => { heroesGo.Add(HeroCollectionConstants.Class_Orc_Monk, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Orc_Shaman), op => { heroesGo.Add(HeroCollectionConstants.Class_Orc_Shaman, op.Result); }),
				
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Dwarf_Scavenger), op => { heroesGo.Add(HeroCollectionConstants.Class_Dwarf_Scavenger, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Dwarf_Artisan), op => { heroesGo.Add(HeroCollectionConstants.Class_Dwarf_Artisan, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Dwarf_Wizard), op => { heroesGo.Add(HeroCollectionConstants.Class_Dwarf_Wizard, op.Result); }),
				
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Kamael_Trooper), op => { heroesGo.Add(HeroCollectionConstants.Class_Kamael_Trooper, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(heroesCatalogue.GetHeroPrefabReference(HeroCollectionConstants.Class_Kamael_Warder), op => { heroesGo.Add(HeroCollectionConstants.Class_Kamael_Warder, op.Result); }),

				AddressablesHelper.LoadAssetAsync<GameObject>(attackEffectCatalogue.GetAttackEffectPrefabReference(AttackEffectCollectionConstants.AttackEffect_1), op => { attackEffectsGo.Add(AttackEffectCollectionConstants.AttackEffect_1, op.Result); }),
				AddressablesHelper.LoadAssetAsync<GameObject>(attackEffectCatalogue.GetAttackEffectPrefabReference(AttackEffectCollectionConstants.AttackEffect_2), op => { attackEffectsGo.Add(AttackEffectCollectionConstants.AttackEffect_2, op.Result); }),
			});
		}
		public GameObject GetPlayerGameObject()
		{
			return PlayerGO;
		}
		public GameObject GetGameObjectByDefinition(GameDefinition definition)
		{
			GameObject gameObject = null;
			switch (definition)
			{
				case HeroClassDefinition heroClassDefinition:
					gameObject = GetHeroGameObject(heroClassDefinition.DefinitionId);
					break;
				case EnemyDefinition enemyDefinition:
					gameObject = GetEnemyGameObject(enemyDefinition.DefinitionId);
					break;
				case MapDefinition mapDefinition:
					gameObject = GetMapGameObject(mapDefinition.DefinitionId);
					break;
				case AttackEffectDefinition attackEffectDefinition:
					gameObject = GetAttackEffectGameObject(attackEffectDefinition.DefinitionId);
					break;
				default:					
					break;
			}
			return gameObject;
		}
		private GameObject GetMapGameObject(string definitionId)
		{			
			if (mapsGo.TryGetValue(definitionId, out GameObject mapGo))
			{
				return mapGo;
			}
			return null;
		}
		private GameObject GetEnemyGameObject(string definitionId)
		{
			if (enemiesGo.TryGetValue(definitionId, out GameObject enemyGo))
			{
				return enemyGo;
			}
			return null;
		}
		private GameObject GetHeroGameObject(string definitionId)
		{
			if (heroesGo.TryGetValue(definitionId, out GameObject heroGo))
			{
				return heroGo;
			}
			return null;
		}
		private GameObject GetAttackEffectGameObject(string definitionId)
		{
			if (attackEffectsGo.TryGetValue(definitionId, out GameObject attackEffectGo))
			{
				return attackEffectGo;
			}
			return null;
		}
	}
}