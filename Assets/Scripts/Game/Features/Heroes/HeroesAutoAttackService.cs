using Core.CoroutineProvider;
using Core.MessageHub;
using Game.Features.Enemy;
using Game.Features.HeroBuilding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Features.Heroes
{
	[Serializable]
	public struct HeroDamageInfo
	{
		public int  Damage;
		public bool IsCrit;
	}
	public interface IHeroesAutoAttackService
	{

	}
	public class HeroesAutoAttackService : IHeroesAutoAttackService, IInitializable, IDisposable
	{
		[Inject]
		private ICoroutineService coroutineService;

		[Inject]
		private IMessageHubService messageHubService;

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
		private HeroDamageInfo CalculateDamage(HeroVO heroVO)
		{
			HeroDamageInfo heroDamageInfo = new HeroDamageInfo();
			bool isCrit = CheckCritChance(heroVO.CritChance);
			int randomDamage = UnityEngine.Random.Range(heroVO.MinDamage, heroVO.MaxDamage + 1);
			if (isCrit == true)
			{
				randomDamage = (int)(randomDamage * heroVO.CritMulti);
			}
			heroDamageInfo.Damage = randomDamage;
			heroDamageInfo.IsCrit = isCrit;
			return heroDamageInfo;
		}
		private bool CheckCritChance(float chance)
		{
			float randomChance = UnityEngine.Random.Range(0f,100f);
			if (chance > randomChance)
			{
				return true;
			}
			else
			{
				return false;
			}
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
										messageHubService.Publish(new HeroCollectionMessages.HeroAttackMessage(heroPresenters[i].EnemyList[j], heroes[i].transform.position, CalculateDamage(heroPresenters[i].Hero)));
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
		}
	}
}