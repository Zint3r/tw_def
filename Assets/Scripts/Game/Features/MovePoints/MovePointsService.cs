using System.Collections.Generic;
using Zenject;

namespace Game.Features.MovePoints
{
	public class MovePointsService : IMovePointsService
	{
		[Inject]
		private MovePointsModel movePointsModel;		

		public void InitializeMovePoints(Dictionary<string, Dictionary<int, List<MovePointVO>>> movePoints)
		{
			movePointsModel.AllPoints =	movePoints;
		}
	}
}