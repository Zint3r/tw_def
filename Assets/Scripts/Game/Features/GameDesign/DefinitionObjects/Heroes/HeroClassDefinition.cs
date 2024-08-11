namespace Game.Features.GameDesign.DefinitionObjects.Heroes
{
	public class HeroClassDefinition : GameDefinition
	{
		public string Class;

		public static HeroClassDefinition Empty =>
			new HeroClassDefinition
			{				
				Class = "Empty"
			};
	}
}