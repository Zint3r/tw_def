using Game.Features.GameDesign;
using Game.Features.GameDesign.DefinitionObjects.Heroes;

namespace Game.Features.Heroes
{
	public struct HeroVO
    {
        public int Id;
		public int LocalizationId;

		public int MinDamage;
		public int MaxDamage;
		public float AttackSpeed;
		public float CritChance;
		public int Price;

		public HeroClassDefinition HeroDefinition;
		public HeroCategoryDefinition CategoryDefinition;
		public HeroRaceDefinition HeroRaceDefinition;

		public static HeroVO Empty =>
			new HeroVO
			{
				CategoryDefinition = HeroCategoryDefinition.Empty,
				HeroRaceDefinition = HeroRaceDefinition.Empty,
				HeroDefinition = HeroClassDefinition.Empty,
				Id = 0,
				LocalizationId = 0,
				MinDamage = 0,
				MaxDamage = 0,
				AttackSpeed = 0,
				CritChance = 0,
				Price = 1
			};
	}
}