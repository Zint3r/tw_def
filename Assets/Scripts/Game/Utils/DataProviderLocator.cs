using Game.Features.Enemy;
using Game.Features.GameDesign;
using Game.Features.Heroes;
using Game.Features.Level;
using Game.Features.Localization;
using Game.Features.MapCreator;
using Game.Features.MovePoints;
using Game.Features.Resource;
using Game.Features.Skills;
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

		[InjectOptional]
		private ISkillsDataProvider skillsDataProvider;
		public static ISkillsDataProvider SkillsDataProvider => instance.skillsDataProvider;
	}
}