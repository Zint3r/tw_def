using Core.CoroutineProvider;
using Core.MessageHub;
using Game.Features.Enemy;
using Game.Features.GameDesign;
using Game.Features.GameDesign.DefinitionObjects.AttackEffect;
using Game.Features.HeroBuilding;
using Game.UI.Factories;
using Module.GameObjectInstaller.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Features.Heroes
{
	public interface IHeroesAutoAttackService
	{

	}
	public class HeroesAutoAttackService : IHeroesAutoAttackService
	{/*
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
		private List<GameObject> heroes = new List<GameObject>();
		private List<HeroPresenter> heroPresenters = new List<HeroPresenter>();
		private List<float> currentTimersToAttack = new List<float>();

		public void Initialize()
		{
			messageHubService.Subscribe<HeroBuildingMessages.OnCompleteHeroBuildingMessage>(AddHero);
			coroutineService.StartCoroutine(HeroesAttack());
		}
		public void Dispose()
		{
			messageHubService.Unsubscribe<HeroBuildingMessages.OnCompleteHeroBuildingMessage>(AddHero);
			coroutineService.StopCoroutine(HeroesAttack());
		}
		private void AddHero(HeroBuildingMessages.OnCompleteHeroBuildingMessage message)
		{
			heroes.Add(message.HeroGO);
			heroPresenters.Add(message.HeroGO.GetComponent<HeroPresenter>());
			currentTimersToAttack.Add(0f);
		}
		private int CalculateDamage(HeroVO heroVO)
		{
			return Random.Range(heroVO.MinDamage, heroVO.MinDamage);
		}
		private IEnumerator HeroesAttack()
		{
			while (true)
			{
				for (int i = 0; i < heroes.Count; i++)
				{
					if (currentTimersToAttack[i] > heroPresenters[i].Hero.AttackSpeed && heroPresenters[i].EnemyList.Count > 0)
					{
						for (int j = 0; j < heroPresenters[i].EnemyList.Count; j++)
						{
							if (heroPresenters[i].EnemyList[j].TryGetComponent(out EnemyPresenter enemy) == true)
							{
								if (enemy.AliveStatus() == true)
								{
									if (Vector3.Distance(heroes[i].transform.position, heroPresenters[i].EnemyList[j].position) < heroPresenters[i].Radius)
									{
										currentTimersToAttack[i] = 0f;
										AttackEffectDefinition definition = gameDefinitionModel.GetOrCreate<AttackEffectDefinition>(AttackEffectCollectionConstants.AttackEffect_1);
										GameObject go = gameObjectFactory.GetGameObjectByDefinition(definition);
										GameObject attackEffect = pool.GetPooledPrefab(go, heroes[i].transform.position, Quaternion.identity);
										ProjectilePresenter projectilePresenter;
										attackEffect.TryGetComponent(out projectilePresenter);
										projectilePresenter.OnStart(heroPresenters[i].EnemyList[j], CalculateDamage(heroPresenters[i].Hero));
										break;
									}
									else
									{
										continue;
									}
								}
								else
								{
									heroPresenters[i].EnemyList.Remove(heroPresenters[i].EnemyList[j]);
								}
							}
						}
					}
					else
					{
						currentTimersToAttack[i] += Time.fixedDeltaTime;
					}
				}
				yield return waitForHeroAttack;
			}
		}*/
	}
}