using System.Collections.Generic;

namespace Game.Features.MovePoints
{
	public interface IMovePointsService
	{
		void InitializeMovePoints(Dictionary<string, Dictionary<int, List<MovePointVO>>> movePoints);
	}
}