using Core.MessageHub;
using System;
using Zenject;

namespace Game.Features.Enemy
{
	public interface IEnemyReciveDamageService
	{		
		void EnemyReciveDamage(EnemyMessages.EnemyReciveDamageMessage message);
	}
	public class EnemyReciveDamageService : IEnemyReciveDamageService, IInitializable, IDisposable
	{
		[Inject]
		private EnemyesModel enemyesModel;

		[Inject]
		private IMessageHubService messageHubService;

		public void Initialize()
		{
			messageHubService.Subscribe<EnemyMessages.EnemyReciveDamageMessage>(EnemyReciveDamage);
		}
		public void Dispose()
		{
			messageHubService.Unsubscribe<EnemyMessages.EnemyReciveDamageMessage>(EnemyReciveDamage);
		}
		public void EnemyReciveDamage(EnemyMessages.EnemyReciveDamageMessage message)
		{
			if (enemyesModel.EnemyesOnScene.TryGetValue(message.EnemyID, out EnemyStatsInfo enemy))
			{
				EnemyVO enemyVO = enemy.EnemyVO;
				int enemyHp = enemyVO.Hp;
				if (enemyHp - message.Damage > 0)
				{
					enemyVO.Hp -= message.Damage;
					enemy.EnemyVO = enemyVO;
					enemyesModel.EnemyesOnScene[message.EnemyID] = enemy;
				}
				else
				{
					messageHubService.Publish(new EnemyMessages.EnemyDeadMessage(enemyVO.Id));
				}
			}
		}
	}
}