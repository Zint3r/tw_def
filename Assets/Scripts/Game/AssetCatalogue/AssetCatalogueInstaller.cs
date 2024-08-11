using UnityEngine;
using Zenject;

namespace Game.AssetCatalogue
{
	[CreateAssetMenu(fileName = "New AssetCatalogueInstaller", menuName = "AssetCatalogue/AssetCatalogueInstaller")]
	public class AssetCatalogueInstaller : ScriptableObjectInstaller<AssetCatalogueInstaller>
	{
		[Header("GameObjects")]
		public PrefabsCatalogue prefabsCatalogue;

		[Header("Maps")]
		public MapsCatalogue mapsCatalogue;

		[Header("Enemies")]
		public EnemiesCatalogue enemiesCatalogue;

		[Header("Heroes")]
		public HeroesCatalogue heroesCatalogue;

		[Header("AttackEffects")]
		public AttackEffectCatalogue attackEffectCatalogue;

		[Header("UI & 2D")]
		public UiPrefabsCatalogue uiPrefabsCatalogue;
		public SpriteSheetCatalogue SpriteSheetCatalogue;
		public UiConfigurationsCatalogue uiConfigurationsCatalogue;

		[Header("Level")]
		public LevelViewSettingsCatalogue levelViewSettingsCatalogue;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<PrefabsCatalogue>().FromInstance(prefabsCatalogue).AsSingle();
			Container.BindInterfacesAndSelfTo<UiPrefabsCatalogue>().FromInstance(uiPrefabsCatalogue).AsSingle();
			Container.BindInterfacesAndSelfTo<SpriteSheetCatalogue>().FromInstance(SpriteSheetCatalogue).AsSingle();
			Container.BindInterfacesAndSelfTo<UiConfigurationsCatalogue>().FromInstance(uiConfigurationsCatalogue).AsSingle();
			Container.BindInterfacesAndSelfTo<MapsCatalogue>().FromInstance(mapsCatalogue).AsSingle();
			Container.BindInterfacesAndSelfTo<EnemiesCatalogue>().FromInstance(enemiesCatalogue).AsSingle();
			Container.BindInterfacesAndSelfTo<HeroesCatalogue>().FromInstance(heroesCatalogue).AsSingle();
			Container.BindInterfacesAndSelfTo<AttackEffectCatalogue>().FromInstance(attackEffectCatalogue).AsSingle();
			//
			Container.BindInterfacesAndSelfTo<LevelViewSettingsCatalogue>().FromInstance(levelViewSettingsCatalogue).AsSingle();
		}
	}
}