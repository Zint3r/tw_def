//using Game.Features.Buff.Actions;
//using Game.Features.HeroUpgrade.Action;
//using Game.Features.PlayerProfile;
//using Game.Features.Skills.Actions;
using Game.Features.Enemy;
using Game.Features.Localization;
using Game.Features.MapCreator;
using Module.Network;
using Zenject;

namespace Game.Features.Actions
{
	public class ActionLocator : ActionService, IInitializable
    {
        public LocalizationAction LocalizationAction { get; private set; }

		public MapCreatorAction MapCreatorAction { get; private set; }
		public EnemySpawnEnablerAction EnemySpawnEnablerAction { get; private set; }
		public EnemySpawnerAction EnemySpawnerAction { get; private set; }
		public EnemyReciveDamageAction EnemyReciveDamageAction { get; private set; }
		public EnemyDeadAction EnemyDeadAction { get; private set; }
		//public PlayerLevelUpAction PlayerLevelUpAction { get; private set; }
		//public HeroUpgradeAction HeroUpgradeAction { get; private set; }   
		//public ApplyBuffAction ApplyBuffAction { get; private set; }
		//public UseSkillAction UseSkillAction { get; private set; }

		public void Initialize()
        {
            LocalizationAction = RegisterAction<LocalizationAction, LocalizationSwitchRequest, LocalizationParams>(RequestMapping.LanguageSwitch);
			MapCreatorAction = RegisterAction<MapCreatorAction, MapSelectRequest, MapSelectParams>(RequestMapping.SelectMap);
			EnemySpawnEnablerAction = RegisterAction<EnemySpawnEnablerAction, EnemySpawnEnablerRequest, EnemySpawnEnablerParams>(RequestMapping.SpawnEnemyEnable);
			EnemySpawnerAction = RegisterAction<EnemySpawnerAction, EnemySpawnerRequest, EnemySpawnerParams>(RequestMapping.SpawnEnemy);
			EnemyReciveDamageAction = RegisterAction<EnemyReciveDamageAction, EnemyReciveDamageRequest, EnemyReciveDamageParams>(RequestMapping.EnemyReciveDamage);
			EnemyDeadAction = RegisterAction<EnemyDeadAction, EnemyDeadRequest, EnemyDeadParams>(RequestMapping.EnemyDead);
			//HeroUpgradeAction = RegisterAction<HeroUpgradeAction, HeroUpgradeRequest, HeroUpgradeActionParams>(RequestMapping.HeroUpgrade);

			//PlayerLevelUpAction = RegisterAction<PlayerLevelUpAction, PlayerLevelUpRequest, PlayerLevelUpActionsParams>(RequestMapping.PlayerLevelLevelUp);

			//ApplyBuffAction = RegisterAction<ApplyBuffAction, ApplyBuffRequest, ApplyBuffActionParams>(RequestMapping.ApplyBuff);

			//UseSkillAction = RegisterAction<UseSkillAction, UseSkillRequest, UseSkillActionParams>(RequestMapping.UseSkill);
		}
    }
}