using Core.Helper;
using Game.AssetCatalogue;
using Game.Features.GameDesign;
using Game.Features.GameDesign.DefinitionObjects.Heroes;
using Game.Features.GameDesign.DefinitionObjects.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Zenject;

namespace Game.UI.Factories
{
	public enum IconSourceSize
    {
        Small,
        Big
    } 
    
    public class IconFactory
    {
        [Inject]
        private SpriteSheetCatalogue spriteSheetCatalogue;
        
        private SpriteAtlas loadingIconAtlas;
		private SpriteAtlas resourceIconAtlas;
		private SpriteAtlas mainControlPanelAtlas;
		private SpriteAtlas conditionSettingIconAtlas;
		private SpriteAtlas mapSelectPanelAtlas;

		public IEnumerator Initialize()
		{
			yield return CoroutineHelper.RunInParallel(new List<IEnumerator>
			{
				AddressablesHelper.LoadAssetAsync<SpriteAtlas>(spriteSheetCatalogue.LoadingIconAtlas, op => loadingIconAtlas = op.Result),
				AddressablesHelper.LoadAssetAsync<SpriteAtlas>(spriteSheetCatalogue.ResourceIconAtlas, op => resourceIconAtlas = op.Result),
				AddressablesHelper.LoadAssetAsync<SpriteAtlas>(spriteSheetCatalogue.MainControlPanelAtlas, op => mainControlPanelAtlas = op.Result),
				AddressablesHelper.LoadAssetAsync<SpriteAtlas>(spriteSheetCatalogue.ConditionSettingIconAtlas, op => conditionSettingIconAtlas = op.Result),
				AddressablesHelper.LoadAssetAsync<SpriteAtlas>(spriteSheetCatalogue.MapSelectPanelAtlas, op => mapSelectPanelAtlas = op.Result)
			});
			
		}
        
        public Sprite GetDefinitionSprite(GameDefinition definition)
        {
            Sprite sprite = null;

            switch (definition)
            {
				case ResourceDefinition resourceDefinition:
					sprite = GetResourceIcon(resourceDefinition);
					break;
				case HeroRaceDefinition heroRaceDefinition:
					sprite = GetHeroRaceIcon(heroRaceDefinition);
					break;
				case HeroClassDefinition heroClassDefinition:
					sprite = GetHeroClassIcon(heroClassDefinition);
					break;
				case MapDefinition mapDefinition:
					sprite = GetMapIcon(mapDefinition);
					break;
				//case SkillDefinition skillDefinition:
				//	sprite = GetSkillIcon(skillDefinition);
				//	break;
				//case BuffDefinition buffDefinition:
				//    sprite = GetBuffIcon(buffDefinition);
				//    break;
				//case HeroCategoryDefinition heroCategoryDefinition:
				//    sprite = GetHeroCategoryIcon(heroCategoryDefinition);
				//    break;
				//case HeroDefinition heroDefinition:
				//    sprite = GetHeroIcon(heroDefinition);
				//    break;
				//case InventoryItemDefinition heroDefinition:
				//    sprite = GetInventoryIcon(heroDefinition);
				//    break;
				default:
                    //sprite = GetGenericIcon(definition.AssetId);
                    break;
            }

            return sprite;
        }

		private Sprite GetMapIcon(MapDefinition mapDefinition)
		{
			string assetName = string.Concat("map_preview_", mapDefinition.AssetId);
			return mapSelectPanelAtlas.GetSprite(assetName);
		}

		public Sprite GetSpriteByName(SpriteAtlas atlas, string name)
		{
			Sprite sprite = atlas.GetSprite(name);
			return sprite;
		}

		public Sprite GetLoadingIcon(string definition)
		{
			string assetName = string.Concat("icon_", definition);
			return loadingIconAtlas.GetSprite(assetName);
		}

		public Sprite GetResourceIcon(ResourceDefinition definition)
        {
            string assetName = string.Concat("icon_", definition.AssetId);
            return resourceIconAtlas.GetSprite(assetName);
        }

		public Sprite GetHeroRaceIcon(HeroRaceDefinition definition)
		{
			string assetName = string.Concat("icon_", definition.AssetId);
			return mainControlPanelAtlas.GetSprite(assetName);
		}

		public Sprite GetHeroClassIcon(HeroClassDefinition definition)
		{
			string assetName = string.Concat("icon_", definition.AssetId);
			return mainControlPanelAtlas.GetSprite(assetName);
		}

		public Sprite GetConditionSettingSprite(string name)
		{			
			return GetSpriteByName(conditionSettingIconAtlas, name);
		}
	}
}