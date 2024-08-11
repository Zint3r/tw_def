using Game.Features.GameDesign.DefinitionObjects.Map;

namespace Game.Features.MapCreator
{
	public struct MapVO
	{
		public string Name;
		public MapDefinition MapDefinition;
		public static MapVO Empty =>
			new MapVO
			{
				Name = "empty",
				MapDefinition = null
			};

	}
}