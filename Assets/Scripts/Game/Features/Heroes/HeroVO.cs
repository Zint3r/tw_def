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
		public float CritMulti;
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
				MinDamage = 1,
				MaxDamage = 2,
				AttackSpeed = 1,
				CritChance = 5f,
				CritMulti = 2f,
				Price = 1
			};
	}
}