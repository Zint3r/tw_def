using Game.Features.Actions;
using System;
using Zenject;

namespace Game.Features.Enemy
{
	public class EnemySpawnEnablerParams : IActionParams
	{
		public readonly bool SpawnEnable;
		public EnemySpawnEnablerParams(bool spawnEnable)
		{
			SpawnEnable = spawnEnable;
		}
	}
	public class EnemySpawnEnablerAction : AbstractAction<EnemySpawnEnablerRequest, EnemySpawnEnablerParams>
	{
		[Inject]
		private IEnemySpawnerService enemySpawnerService;
		public override bool CanExecute(EnemySpawnEnablerParams actionParams, DateTime timeStamp, out string errorMessage)
		{
			return base.CanExecute(actionParams, timeStamp, out errorMessage);
		}
		protected override void UpdateModel(EnemySpawnEnablerParams actionParams, DateTime timeStamp)
		{
			enemySpawnerService.EnableSpawner(actionParams.SpawnEnable);
			messageHubService.Publish(new EnemyMessages.SpawnEnableMessage(actionParams.SpawnEnable));
		}
		protected override EnemySpawnEnablerRequest CreateNetworkRequest(EnemySpawnEnablerParams actionParams)
		{
			return new EnemySpawnEnablerRequest { };
		}
	}

	public class EnemySpawnerParams : IActionParams
	{
		public EnemyVO EnemyVO { get; }
		public EnemySpawnerParams(EnemyVO enemyVO)
		{
			EnemyVO = enemyVO;
		}
	}
	public class EnemySpawnerAction : AbstractAction<EnemySpawnerRequest, EnemySpawnerParams>
	{
		protected override EnemySpawnerRequest CreateNetworkRequest(EnemySpawnerParams actionParams)
		{
			return new EnemySpawnerRequest { };
		}
		public override bool CanExecute(EnemySpawnerParams actionParams, DateTime timeStamp, out string errorMessage)
		{
			return base.CanExecute(actionParams, timeStamp, out errorMessage);
		}
		protected override void UpdateModel(EnemySpawnerParams actionParams, DateTime timeStamp)
		{
			//messageHubService.Publish(new EnemyMessages.SpawnMessage(actionParams.EnemyVO));
		}
	}

	public class EnemyReciveDamageParams : IActionParams
	{
		public int EnemyID;
		public int Damage;
		public EnemyReciveDamageParams(int damage, int enemyID)
		{
			Damage = damage;
			EnemyID = enemyID;
		}
	}
	public class EnemyReciveDamageAction : AbstractAction<EnemyReciveDamageRequest, EnemyReciveDamageParams>
	{
		protected override EnemyReciveDamageRequest CreateNetworkRequest(EnemyReciveDamageParams actionParams)
		{
			return new EnemyReciveDamageRequest { };
		}
		public override bool CanExecute(EnemyReciveDamageParams actionParams, DateTime timeStamp, out string errorMessage)
		{			
			return base.CanExecute(actionParams, timeStamp, out errorMessage);
		}
		protected override void UpdateModel(EnemyReciveDamageParams actionParams, DateTime timeStamp)
		{
			//messageHubService.Publish(new EnemyMessages.EnemyReciveDamageMessage(actionParams.Damage, actionParams.EnemyID));
		}
	}

	public class EnemyDeadParams : IActionParams
	{
		public int EnemyId;
		public EnemyDeadParams(int enemyId)
		{
			EnemyId = enemyId;
		}
	}
	public class EnemyDeadAction : AbstractAction<EnemyDeadRequest, EnemyDeadParams>
	{
		protected override EnemyDeadRequest CreateNetworkRequest(EnemyDeadParams actionParams)
		{
			return new EnemyDeadRequest { };
		}
		public override bool CanExecute(EnemyDeadParams actionParams, DateTime timeStamp, out string errorMessage)
		{			
			return base.CanExecute(actionParams, timeStamp, out errorMessage);
		}
		protected override void UpdateModel(EnemyDeadParams actionParams, DateTime timeStamp)
		{
			messageHubService.Publish(new EnemyMessages.EnemyDeadMessage(actionParams.EnemyId));
		}
	}

	//public class TimerToNewWaveParams : IActionParams
	//{
	//	public readonly int Timer;
	//
	//	public TimerToNewWaveParams(int sizeTimer)
	//	{
	//		Timer = sizeTimer;
	//	}
	//}
	//
	//public class TimerToNewWaveAction : AbstractAction<TimerToNewWaveRequest, TimerToNewWaveParams>
	//{
	//	protected override TimerToNewWaveRequest CreateNetworkRequest(TimerToNewWaveParams actionParams)
	//	{
	//		return new TimerToNewWaveRequest { };
	//	}
	//
	//	public override bool CanExecute(TimerToNewWaveParams actionParams, DateTime timeStamp, out string errorMessage)
	//	{
	//		return base.CanExecute(actionParams, timeStamp, out errorMessage);
	//	}
	//
	//	protected override void UpdateModel(TimerToNewWaveParams actionParams, DateTime timeStamp)
	//	{
	//		messageHubService.Publish(new EnemyMessages.TimerToNewWaveMessage(actionParams.Timer));
	//	}
	//}
}