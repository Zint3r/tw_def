using Game.Features.GameDesign.DefinitionObjects.Enemyes;
using Game.Features.MovePoints;
using System.Collections.Generic;

namespace Game.Features.Enemy
{
	public struct EnemyVO
	{
		public int Id;
		public string Name;
		public float Speed;
		public int Hp;
		public bool InControl;
		public bool IsAlive;

		public List<MovePointVO> MovePoints;
		public int MoveIndex;
		public EnemyDefinition EnemyDefinition;


		public static EnemyVO Empty =>
			new EnemyVO
			{
				Id = -1,
				Name = "empty",
				Speed = 1f,
				Hp = 1,
				InControl = false,
				IsAlive = true,
				EnemyDefinition = null,
				MovePoints = new List<MovePointVO>(),
				MoveIndex = 0
			};
	}
}