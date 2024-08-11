using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.AssetCatalogue
{
	[CreateAssetMenu(fileName = "New SpriteSheetCatalogue", menuName = "AssetCatalogue/SpriteSheetCatalogue")]
	public class SpriteSheetCatalogue : ScriptableObject
	{
		public AssetReference LoadingIconAtlas;
		public AssetReference ResourceIconAtlas;
		public AssetReference ConditionSettingIconAtlas;
		public AssetReference MainControlPanelAtlas;
		public AssetReference MapSelectPanelAtlas;
	}
}