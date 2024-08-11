using Game.Features.Enemy;
using Game.Features.GameDesign;
using Game.Features.Heroes;
using Game.Features.Level;
using Game.Features.Localization;
using Game.Features.MapCreator;
using Game.Features.MovePoints;
using Game.Features.Resource;
using Zenject;

namespace Game.Utils
{
	public class DPL : IInitializable
    {
        private static DPL instance;

        public void Initialize()
        {
            instance = this;
        }

		[InjectOptional]
		private IGameDefinitionDataProvider gameDefinitionDataProvider;
		public static IGameDefinitionDataProvider GameDefinitionDataProvider => instance.gameDefinitionDataProvider;

		[InjectOptional]
		private ILocalizationDataProvider localizationDataProvider;
		public static ILocalizationDataProvider LocalizationDataProvider => instance.localizationDataProvider;

		[InjectOptional]
		private IResourceDataProvider resourceDataProvider;
		public static IResourceDataProvider ResourceDataProvider => instance.resourceDataProvider;

		[InjectOptional]
		private ILevelDataProvider levelDataProvider;
		public static ILevelDataProvider LevelDataProvider => instance.levelDataProvider;

		[InjectOptional]
		private IHeroCollectionDataProvider heroCollectionDataProvider;
		public static IHeroCollectionDataProvider HeroCollectionDataProvider => instance.heroCollectionDataProvider;

		[InjectOptional]
		private IMapCreatorDataProvider mapCreatorDataProvider;
		public static IMapCreatorDataProvider MapCreatorDataProvider => instance.mapCreatorDataProvider;

		[InjectOptional]
		private IMovePointsDataProvider movePointsDataProvider;
		public static IMovePointsDataProvider MovePointsDataProvider => instance.movePointsDataProvider;

		[InjectOptional]
		private IEnemyDataProvider enemyDataProvider;
		public static IEnemyDataProvider EnemyDataProvider => instance.enemyDataProvider;

		//[InjectOptional]
		//private IPlayerProfileDataProvider playerProfileDataProvider;
		//public static IPlayerProfileDataProvider PlayerProfileDataProvider => instance.playerProfileDataProvider;		

		/*[InjectOptional]
        private IResourceDataProvider resourceDataProvider;
        public static IResourceDataProvider ResourceDataProvider => instance.resourceDataProvider;
        
        [InjectOptional]
        private IHeroCollectionDataProvider heroCollectionDataProvider;
        public static IHeroCollectionDataProvider HeroCollectionDataProvider => instance.heroCollectionDataProvider;
        
        [InjectOptional]
        private IGiftsDataProvider giftsDataProvider;
        public static IGiftsDataProvider GiftsDataProvider => instance.giftsDataProvider;
        
        [InjectOptional]
        private IInventoryDataProvider inventoryDataProvider;
        public static IInventoryDataProvider InventoryDataProvider => instance.inventoryDataProvider;
        
        //Levels
        [InjectOptional]
        private ILevelDataProvider levelDataProvider;
        public static ILevelDataProvider LevelDataProvider => instance.levelDataProvider;
        
        [InjectOptional]
        private IAttributeDataProvider attributeDataProvider;
        public static IAttributeDataProvider AttributeDataProvider => instance.attributeDataProvider;
        
        //Check if it is needed        
        [InjectOptional]
        private IBuffDataProvider buffDataProvider;
        public static IBuffDataProvider BuffDataProvider => instance.buffDataProvider;

        [InjectOptional]
        private ISkillDataProvider skillDataProvider;
        public static ISkillDataProvider SkillDataProvider => instance.skillDataProvider;

        //Upgrades
        [InjectOptional]
        private IHeroUpgradeDataProvider upgradeProvider;
        public static IHeroUpgradeDataProvider UpgradeProvider => instance.upgradeProvider;*/
	}
}