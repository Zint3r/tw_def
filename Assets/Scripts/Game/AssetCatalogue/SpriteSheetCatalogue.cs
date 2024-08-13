using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.AssetCatalogue
{
	[CreateAssetMenu(fileName = "New SpriteSheetCatalogue", menuName = "AssetCatalogue/SpriteSheetCatalogue")]
	public class SpriteSheetCatalogue : ScriptableObject
	{
		public AssetReferenceAtlasedSprite LoadingIconAtlas;
		public AssetReferenceAtlasedSprite ResourceIconAtlas;
		public AssetReferenceAtlasedSprite ConditionSettingIconAtlas;
		public AssetReferenceAtlasedSprite MainControlPanelAtlas;
		public AssetReferenceAtlasedSprite MapSelectPanelAtlas;
	}
}