using Core;
using UnityEngine;

namespace Game.Features.Enemy
{
	public static class EnemyMessages
	{
		public class SpawnMessage : IMessage
		{
			public readonly EnemyVO Enemy;
			public Transform EnemyTransform;

			public SpawnMessage(EnemyVO enemy, Transform transform)
			{
				Enemy = enemy;
				EnemyTransform = transform;
			}
		}

		public class EnemyDeadMessage : IMessage
		{
			public readonly int EnemyId;

			public EnemyDeadMessage(int enemyId)
			{
				EnemyId = enemyId;
			}
		}

		public class SpawnEnableMessage : IMessage
		{
			public readonly bool SpawnEnable;

			public SpawnEnableMessage(bool spawnEnable)
			{
				SpawnEnable = spawnEnable;
			}
		}

		public class TimerToNewWaveMessage : IMessage
		{
			public readonly int Timer;

			public TimerToNewWaveMessage(int timer)
			{
				Timer = timer;
			}
		}

		public class EnemyReciveDamageMessage : IMessage
		{
			public readonly int EnemyID;
			public readonly int Damage;

			public EnemyReciveDamageMessage(int damage, int enemyID)
			{
				Damage = damage;
				EnemyID = enemyID;
			}
		}
	}
}