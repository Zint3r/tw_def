using System.Collections.Generic;

namespace Game.Features.MovePoints
{
	public interface IMovePointsDataProvider
	{
		MovePointVO GetPoint(int id);
		List<MovePointVO> GetPointsList(int patchId);

		Dictionary<int, List<MovePointVO>> GetAllPathOnMap(string mapName);
	}
}