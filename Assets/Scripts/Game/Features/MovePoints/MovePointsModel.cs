using Game.Features.GameDesign;
using System.Collections.Generic;

namespace Game.Features.MovePoints
{
	public class MovePointsModel : IMovePointsDataProvider
	{
		public List<MovePointVO> DefaultPoints = new List<MovePointVO> { MovePointVO.Empty };		
		public Dictionary<int, List<MovePointVO>> PathPoints = new Dictionary<int, List<MovePointVO>>();
		public Dictionary<string, Dictionary<int, List<MovePointVO>>> AllPoints = new Dictionary<string, Dictionary<int, List<MovePointVO>>>();

		public Dictionary<int, List<MovePointVO>> GetAllPathOnMap(string mapName)
		{
			AllPoints.TryGetValue(mapName, out Dictionary<int, List<MovePointVO>> allPoints);
			return allPoints;
		}

		public MovePointVO GetPoint(int id)
		{
			throw new System.NotImplementedException();
		}

		public List<MovePointVO> GetPointsList(int patchId)
		{
			AllPoints.TryGetValue(MapConstants.Aden, out Dictionary<int, List<MovePointVO>> allPoints);
			allPoints.TryGetValue(patchId, out List<MovePointVO> points);
			if (points == null)
			{
				return DefaultPoints;
			}
			return points;
		}
	}
}