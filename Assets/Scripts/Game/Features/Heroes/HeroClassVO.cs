using Game.Features.GameDesign.DefinitionObjects.Heroes;

namespace Game.Features.Heroes
{
	public struct HeroClassVO
	{        
		public HeroClassDefinition HeroClassDefinition;
		public HeroRaceDefinition HeroRaceDefinition;

		public static HeroClassVO Empty =>
			new HeroClassVO
			{
				HeroRaceDefinition = HeroRaceDefinition.Empty,
				HeroClassDefinition = HeroClassDefinition.Empty
			};
	}
}