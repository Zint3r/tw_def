using Core.MessageHub;
using Game.Features.Actions;
using Module.DateTimeProvider;
using System;
using Zenject;

namespace Game.Features.Enemy
{
	public interface IEnemyMoveService
	{
	}

	public class EnemyMoveService : IEnemyMoveService, IInitializable, IDisposable
	{
		[Inject]
		private EnemyesModel enemyesModel;

		[Inject]
		private IMessageHubService messageHubService;

		[Inject]
		private IDateTimeProvider dateTimeProvider;

		[Inject]
		private ActionLocator actionLocator;

		public void Initialize()
		{
			//messageHubService.Subscribe<EnemyMessages.EnemyReciveDamageMessage>(EnemyReciveDamage);
		}
		public void Dispose()
		{
			//messageHubService.Unsubscribe<EnemyMessages.EnemyReciveDamageMessage>(EnemyReciveDamage);
		}
	}
}