using Core.CoroutineProvider;
using Core.MessageHub;
using Game.Features.GameDesign;
using Game.Features.GameDesign.DefinitionObjects.AttackEffect;
using Game.Features.Skills;
using Game.UI.Factories;
using Module.GameObjectInstaller.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Game.Features.Heroes
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

		private WaitForSeconds waitForHeroAttack = new WaitForSeconds(Time.fixedDeltaTime);

		private List<Transform> projectilesObj = new List<Transform>();
		private List<Transform> targetObj = new List<Transform>();
		private List<float3> targelastPositiontObj = new List<float3>();
		private List<ProjectilePresenter> projectilesPresenters = new List<ProjectilePresenter>();

		private float speed = 20f;

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
				projectilePresenter.OnStart(message.TargetTransform, message.HeroDamageInfo.Damage);
				projectilesObj.Add(attackEffect.transform);
				targetObj.Add(message.TargetTransform);
				targelastPositiontObj.Add(message.TargetTransform.position);
				projectilesPresenters.Add(projectilePresenter);
			}
		}
		private void RemoveProjectile(int index)
		{
			projectilesObj.RemoveAt(index);
			targetObj.RemoveAt(index);
			targelastPositiontObj.RemoveAt(index);
			projectilesPresenters.RemoveAt(index);
		}
		private IEnumerator AddProjectilesMove()
		{
			while (true)
			{
				for (int i = 0; i < projectilesObj.Count; i++)
				{
                    if (targetObj[i] == null)
                    {
						if (Vector3.Distance(projectilesObj[i].position, targelastPositiontObj[i]) > 0.5f)
						{
							projectilesObj[i].position = Vector3.MoveTowards(projectilesObj[i].position, targelastPositiontObj[i], speed * Time.fixedDeltaTime);
						}
						else if (projectilesPresenters[i] != null)
						{
							if (projectilesPresenters[i].GetParticleSystemInfo() == false)
							{
								projectilesPresenters[i].PlayParticleSystem();
								RemoveProjectile(i);
							}
						}
					}
					else
					{
						if (Vector3.Distance(projectilesObj[i].position, targetObj[i].position) > 0.5f)
						{
							projectilesObj[i].position = Vector3.MoveTowards(projectilesObj[i].position, targetObj[i].position, speed * Time.fixedDeltaTime);
							targelastPositiontObj[i] = projectilesObj[i].position;
						}
						else if (projectilesPresenters[i] != null)
						{
							if (projectilesPresenters[i].GetParticleSystemInfo() == false)
							{
								projectilesPresenters[i].DealDamageToEnemy();
								RemoveProjectile(i);
							}
						}
					}
				}
				yield return waitForHeroAttack;
			}
		}
	}
}