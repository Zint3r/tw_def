using System.Collections.Generic;

namespace Game.Features.Enemy
{
	public struct EnemyWaveVO
	{
		public int WaveId;
		public float SpawnTimer;
		public List<EnemyVO> EnemyList;

		public static EnemyWaveVO Empty =>
			new EnemyWaveVO
			{
				WaveId = -1,
				SpawnTimer = 1f,
				EnemyList = null
			};
	}
}