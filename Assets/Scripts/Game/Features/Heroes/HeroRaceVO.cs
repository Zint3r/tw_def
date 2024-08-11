using Game.Features.GameDesign.DefinitionObjects.Heroes;

namespace Game.Features.Heroes
{
	public struct HeroRaceVO
	{        
		public HeroRaceDefinition HeroRaceDefinition;

		public static HeroRaceVO Empty =>
			new HeroRaceVO
			{
				HeroRaceDefinition = HeroRaceDefinition.Empty
			};
	}
}