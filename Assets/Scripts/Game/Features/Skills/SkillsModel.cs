using Game.Features.Heroes;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Features.Skills
{
	public interface ISkillsDataProvider
	{
		List<SkillStatsInfo> GetAllProjectilesOnScene();
	}
	public class SkillStatsInfo
	{
		public int TargetId;
		public HeroDamageInfo DamageInfo;
		public Transform ProjectileTransform;
		public Transform TargetTransform;
		public float3 LastPositiontOfTarget;
		public ProjectilePresenter ProjectilesPresenter;
		public float Speed = 20f;
	}
	public class SkillsModel : ISkillsDataProvider
	{
		private List<SkillStatsInfo> ProjectilesOnScene = new List<SkillStatsInfo>();

		public void AddProjectileOnScene(SkillStatsInfo info)
		{
			ProjectilesOnScene.Add(info);
		}
		public void RemoveProjectileOnScene(SkillStatsInfo info)
		{
			ProjectilesOnScene.Remove(info);
		}
		public List<SkillStatsInfo> GetAllProjectilesOnScene()
		{
			return ProjectilesOnScene;
		}
	}
}