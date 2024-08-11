using System.Collections.Generic;

namespace Game.Features.MapCreator
{
	public interface IMapCreatorDataProvider
	{
		MapVO GetMap(int id);
		string GetSelectedMap();
		List<MapVO> GetMapsList();
	}
}