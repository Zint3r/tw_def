using System.Collections.Generic;

namespace Game.Features.MapCreator
{
	public interface IMapCreatorService
	{
		void InitializeMaps(List<MapVO> maps);
		void SelectedMap(MapVO map);
	}
}