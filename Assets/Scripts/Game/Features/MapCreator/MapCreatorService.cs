using System.Collections.Generic;
using Zenject;

namespace Game.Features.MapCreator
{
	public class MapCreatorService : IMapCreatorService
	{
		[Inject]
		private MapCreatorModel mapCreatorModel;

		public void InitializeMaps(List<MapVO> maps)
		{
			mapCreatorModel.Maps = maps;
		}

		public void SelectedMap(MapVO map)
		{
			mapCreatorModel.SelectMap = map.Name;
		}
	}
}