using Core;
using Game.Features.MapCreator;
//using Game.Features.Buff.Actions;
//using Game.Features.PlayerProfile;
//using Game.Features.Skills.Actions;

namespace Module.Network
{
    public class NetworkAction : NetworkCall
    {
        public NetworkAction(RequestMapping.Call identifier, string destination, RequestMapping.Method method, IMessage body)
        {
            Identifier = identifier;
            Destination = destination;
            Method = method;
            Body = body;
        }
    }
        
    public class NetworkCall
    {
        public RequestMapping.Call Identifier;
        public string Destination;
        public RequestMapping.Method Method;
        public IMessage Body;
    }

    public static class RequestMapping
    {
        public enum Method
        {
            GET,
            POST,
            DELETE,
            PUT
        }
        
        public enum Call
        {
            Error,
            PlayerLevelUp,
            
            InventorySellItem,
            UseSkill,
            ApplyBuff,
            StatUp,
            HeroUpgrade,
			LanguageSwitch,
            MapSelect,
			EnemySpawnEnabler,
			EnemySpawn,
			EnemyReciveDamage,
            EnemyDead
		}

		public static NetworkAction LanguageSwitch(LocalizationSwitchRequest request)
		{
			return new NetworkAction(Call.LanguageSwitch, "/game/localization/switch", Method.POST, request);
		}

		public static NetworkAction SelectMap(MapSelectRequest request)
		{
			return new NetworkAction(Call.MapSelect, "/game/map_select/select", Method.POST, request);
		}

		public static NetworkAction SpawnEnemyEnable(EnemySpawnEnablerRequest request)
		{
			return new NetworkAction(Call.EnemySpawnEnabler, "/game/enemy_spawn_enabler/enable", Method.POST, request);
		}

		public static NetworkAction SpawnEnemy(EnemySpawnerRequest request)
		{
			return new NetworkAction(Call.EnemySpawn, "/game/enemy_spawn/spawn", Method.POST, request);
		}

		public static NetworkAction EnemyReciveDamage(EnemyReciveDamageRequest request)
		{
			return new NetworkAction(Call.EnemyReciveDamage, "/game/enemy/recive_damage", Method.POST, request);
		}

		public static NetworkAction EnemyDead(EnemyDeadRequest request)
		{
			return new NetworkAction(Call.EnemyDead, "/game/enemy/dead", Method.POST, request);
		}

		/*public static NetworkAction PlayerLevelLevelUp(PlayerLevelUpRequest request)
        {
            return new NetworkAction(Call.PlayerLevelUp, "/game/player-level/level-up", Method.POST, request);
        }
        
        public static NetworkAction HeroUpgrade(HeroUpgradeRequest request)
        {
            return new NetworkAction(Call.HeroUpgrade, "/game/Hero-upgrade/upgrade", Method.POST, request);
        }
        
        // public static NetworkAction InventorySellItem(InventorySellItemRequest request)
        // {
        //     return new NetworkAction(Call.InventorySellItem, "/game/player-level/level-up", Method.POST, request);
        // }
        
        public static NetworkAction ApplyBuff(ApplyBuffRequest request)
        {
            return new NetworkAction(Call.ApplyBuff, "/game/buff/apply", Method.POST, request);
        }

        public static NetworkAction UseSkill(UseSkillRequest request)
        {
            return new NetworkAction(Call.UseSkill, "/game/skill/use", Method.POST, request);
        }*/
	}
}