using Core.CoroutineProvider;
using Core.MessageHub;
using Game.Features.Enemy;
using Game.Utils;
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

		[Inject]
		private HeroCollectionModel heroCollectionModel;

		private WaitForSeconds waitForHeroAttack = new WaitForSeconds(Time.fixedDeltaTime);

		public void Initialize()
		{
			coroutineService.StartCoroutine(HeroesAttack());
		}
		public void Dispose()
		{
			coroutineService.StopCoroutine(HeroesAttack());
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
		private void HeroAttack(HeroStatsInfo heroStats)
		{
			if (heroStats.CurrentTimersToAttack > heroStats.HeroVO.AttackSpeed && heroStats.HeroPresenter.EnemyList.Count > 0)
			{
				for (int j = 0; j < heroStats.HeroPresenter.EnemyList.Count; j++)
				{
					EnemyStatsInfo enemyStats = DPL.EnemyDataProvider.GetEnemyByTransform(heroStats.HeroPresenter.EnemyList[j]);

					if (enemyStats != null && enemyStats.EnemyVO.IsAlive == true)
					{
						if (Vector3.Distance(heroStats.HeroTransform.position, heroStats.HeroPresenter.EnemyList[j].position) < heroStats.HeroPresenter.Radius)
						{
							heroStats.CurrentTimersToAttack = 0f;
							messageHubService.Publish(new HeroCollectionMessages.HeroAttackMessage(enemyStats.EnemyVO.Id, heroStats.HeroPresenter.EnemyList[j], heroStats.HeroTransform.position, CalculateDamage(heroStats.HeroVO)));
							break;
						}
						else
						{
							continue;
						}
					}
					else
					{
						heroStats.HeroPresenter.EnemyList.Remove(heroStats.HeroPresenter.EnemyList[j]);
					}
				}
			}
			else
			{
				heroStats.CurrentTimersToAttack += Time.fixedDeltaTime;
			}
		}
		private IEnumerator HeroesAttack()
		{
			while (true)
			{
				List<HeroStatsInfo> allHeroOnScene = heroCollectionModel.GetAllHeroOnScene();
				for (int i = 0; i < allHeroOnScene.Count; i++)
				{
					HeroAttack(allHeroOnScene[i]);
				}
				yield return waitForHeroAttack;
			}
		}
	}
}