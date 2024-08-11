using Game.Features.GameDesign.DefinitionObjects.Map;
using Unity.Mathematics;

namespace Game.Features.MovePoints
{
	public struct MovePointVO
	{
		public float3 PointPosition;
		public MapDefinition MapDefinition;
		public static MovePointVO Empty =>
			new MovePointVO
			{
				PointPosition = new float3(),
				MapDefinition = null
			};
	}
}