using Core;

namespace Game.Features.MapCreator
{
	public static class MapCreatorMessages
	{
		public struct MapCreatorMessage : IMessage
		{
			public readonly MapVO SelectedMapVO;

			public MapCreatorMessage(MapVO seletMapVO)
			{
				SelectedMapVO = seletMapVO;
			}
		}
	}
}