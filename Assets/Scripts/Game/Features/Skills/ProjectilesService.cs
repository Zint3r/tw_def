using Core.CoroutineProvider;
using Core.MessageHub;
using Game.Features.Enemy;
using Game.Features.GameDesign;
using Game.Features.GameDesign.DefinitionObjects.AttackEffect;
using Game.Features.Heroes;
using Game.UI.Factories;
using Module.GameObjectInstaller.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Features.Skills
{
	public interface IProjectilesService
	{

	}
	public class ProjectilesService : IProjectilesService, IInitializable, IDisposable
	{
		[Inject]
		private ICoroutineService coroutineService;

		[Inject]
		private IMessageHubService messageHubService;

		[Inject]
		private GameObjectFactory gameObjectFactory;

		[Inject]
		private GameDefinitionModel gameDefinitionModel;

		[Inject]
		private GoPool pool;

		[Inject]
		private SkillsModel skillsModel;

		private WaitForSeconds waitForHeroAttack = new WaitForSeconds(Time.fixedDeltaTime);

		public void Initialize()
		{
			messageHubService.Subscribe<HeroCollectionMessages.HeroAttackMessage>(AddProjectile);
			coroutineService.StartCoroutine(AddProjectilesMove());
		}
		public void Dispose()
		{
			messageHubService.Unsubscribe<HeroCollectionMessages.HeroAttackMessage>(AddProjectile);
			coroutineService.StopCoroutine(AddProjectilesMove());
		}
		private void AddProjectile(HeroCollectionMessages.HeroAttackMessage message)
		{
			AttackEffectDefinition definition;
			if (message.HeroDamageInfo.IsCrit == true)
			{
				definition = gameDefinitionModel.GetOrCreate<AttackEffectDefinition>(AttackEffectCollectionConstants.AttackEffect_2);
			}
			else
			{
				definition = gameDefinitionModel.GetOrCreate<AttackEffectDefinition>(AttackEffectCollectionConstants.AttackEffect_1);
			}
			GameObject go = gameObjectFactory.GetGameObjectByDefinition(definition);
			GameObject attackEffect = pool.GetPooledPrefab(go, Vector3.zero, Quaternion.identity);
			attackEffect.transform.position = message.HeroPosition;
			attackEffect.TryGetComponent(out ProjectilePresenter projectilePresenter);
			
			if (projectilePresenter == true)
			{
				SkillStatsInfo statsInfo = new SkillStatsInfo();
				statsInfo.TargetId = message.EnemyId;
				statsInfo.DamageInfo = message.HeroDamageInfo;
				statsInfo.ProjectileTransform = attackEffect.transform;
				statsInfo.TargetTransform = message.TargetTransform;
				statsInfo.LastPositiontOfTarget = message.TargetTransform.position;
				statsInfo.ProjectilesPresenter = projectilePresenter;
				skillsModel.AddProjectileOnScene(statsInfo);
				projectilePresenter.OnStart();
			}
		}
		private IEnumerator AddProjectilesMove()
		{
			while (true)
			{
				List<SkillStatsInfo> allProjectiles = skillsModel.GetAllProjectilesOnScene();
				for (int i = 0; i < allProjectiles.Count; i++)
				{
                    if (allProjectiles[i].TargetTransform == null)
                    {
						if (Vector3.Distance(allProjectiles[i].TargetTransform.position, allProjectiles[i].LastPositiontOfTarget) > 0.5f)
						{
							allProjectiles[i].ProjectileTransform.position = Vector3.MoveTowards(allProjectiles[i].ProjectileTransform.position, allProjectiles[i].LastPositiontOfTarget, allProjectiles[i].Speed * Time.fixedDeltaTime);
						}
						else if (allProjectiles[i].ProjectilesPresenter != null)
						{
							if (allProjectiles[i].ProjectilesPresenter.GetParticleSystemInfo() == false)
							{
								allProjectiles[i].ProjectilesPresenter.PlayParticleSystem();
								skillsModel.RemoveProjectileOnScene(allProjectiles[i]);
							}
						}
					}
					else
					{
						if (Vector3.Distance(allProjectiles[i].ProjectileTransform.position, allProjectiles[i].TargetTransform.position) > 0.5f)
						{
							allProjectiles[i].ProjectileTransform.position = Vector3.MoveTowards(allProjectiles[i].ProjectileTransform.position, allProjectiles[i].TargetTransform.position, allProjectiles[i].Speed * Time.fixedDeltaTime);
							allProjectiles[i].LastPositiontOfTarget = allProjectiles[i].ProjectileTransform.position;
						}
						else if (allProjectiles[i].ProjectilesPresenter != null)
						{
							if (allProjectiles[i].ProjectilesPresenter.GetParticleSystemInfo() == false)
							{
								allProjectiles[i].ProjectilesPresenter.PlayParticleSystem();
								int damage = allProjectiles[i].DamageInfo.Damage;
								int enemyId = allProjectiles[i].TargetId;
								messageHubService.Publish(new EnemyMessages.EnemyReciveDamageMessage(damage, enemyId));
								skillsModel.RemoveProjectileOnScene(allProjectiles[i]);
							}
						}
					}
				}
				yield return waitForHeroAttack;
			}
		}
	}
}