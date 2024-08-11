using Game.Features.Actions;
using Game.Features.MapCreator;

public class MapSelectParams : IActionParams
{
	public MapVO SelectMap;
	public MapSelectParams(MapVO selectMap)
	{
		SelectMap = selectMap;
	}
}