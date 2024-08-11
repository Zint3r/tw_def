using System.Collections.Generic;

namespace Game.Features.MapCreator
{
	public class MapCreatorModel : IMapCreatorDataProvider
	{
		public List<MapVO> Maps = new List<MapVO>();
		public string SelectMap = "empty";

		public MapVO GetMap(int id)
		{			
			return Maps[id];
		}

		public List<MapVO> GetMapsList()
		{
			return Maps;
		}

		public string GetSelectedMap()
		{
			return SelectMap;
		}
	}
}