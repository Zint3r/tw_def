namespace Game.Features.GameDesign.DefinitionObjects.Heroes
{
	public class HeroRaceDefinition : GameDefinition
	{
		public string Race;

		public static HeroRaceDefinition Empty =>
			new HeroRaceDefinition
			{
				Race = "Empty"
			};
	}
}