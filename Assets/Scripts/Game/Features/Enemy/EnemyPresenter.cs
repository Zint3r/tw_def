using Core.CoroutineProvider;
using Game.Features.Actions;
using Game.Features.MovePoints;
using Module.DateTimeProvider;
using Module.GameObjectInstaller.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Features.Enemy
{
	public class EnemyPresenter : MonoBehaviour
	{
		[Inject]
		private ActionLocator actionLocator;

		[Inject]
		private IDateTimeProvider dateTimeProvider;

		[Inject]
		private ICoroutineService coroutineService;

		[HideInInspector]
		public int EnemyId = 0;

		private NavMeshAgent agent;
		private Animator anim;
		private AnimationClip clip;
		private int TriggerId = 0;
		private EnemyVO enemyStats = EnemyVO.Empty;
		private int index = 0;
		private List<MovePointVO> points = new List<MovePointVO>();
		private bool inControl = false;
		private bool isAlive = true;
		public void OnStart(EnemyVO enemyVO)
		{
			inControl = false;
			isAlive = true;

			if (anim == null)
			{
				anim = GetComponent<Animator>();
			}
			if (agent == null)
			{
				agent = GetComponent<NavMeshAgent>();
			}
			if (TriggerId == 0)
			{
				TriggerId = Animator.StringToHash("Dead");
			}
			clip = anim.runtimeAnimatorController.animationClips[1];
			clip.events[0].functionName = "Dead";
			EnemyId = enemyVO.Id;
			index = 0;
			enemyStats = enemyVO;
			agent.speed = enemyVO.Speed;
			points = enemyVO.MovePoints;
			coroutineService.StartCoroutine(EnemyMove());
		}
		public void TakeDamage(int damage)
		{
			if (isAlive == false)
			{
				return;
			}
			EnemyReciveDamageParams damageParams = new EnemyReciveDamageParams(damage, enemyStats.Id);
			if (!actionLocator.EnemyReciveDamageAction.CanExecute(damageParams, dateTimeProvider.UtcNow, out string message))
			{
				Debug.LogWarning(message);
				return;
			}
			actionLocator.EnemyReciveDamageAction.Execute(damageParams);
		}
		private void Dead()
		{
			ReturnToPool();
		}
		public void ReturnToPool()
		{
			points = null;
			if (TryGetComponent(out GoPoolMember poolMember) == true)
			{
				poolMember.ReturnToPool();
			}
		}
		public void EnemyDead()
		{
			isAlive = false;
			inControl = false;
			coroutineService.StopCoroutine(EnemyMove());
			enemyStats = EnemyVO.Empty;
			index = 0;
			// play dead animation
			anim.SetTrigger(TriggerId);
		}
		public bool AliveStatus()
		{
			return isAlive;
		}
		public void InControlStatus()
		{
			inControl = true;
		}
		private void OnDisable()
		{
			coroutineService.StopCoroutine(EnemyMove());
		}
		private IEnumerator EnemyMove()
		{
			while (isAlive == true)
			{
				if (agent.hasPath == false && inControl == false)
				{
					if (index <= points.Count - 1)
					{
						agent.destination = points[index].PointPosition;

						index++;
					}
					else
					{
						transform.gameObject.SetActive(false);
					}
				}
				yield return new WaitForSeconds(Time.deltaTime);
			}
		}
	}
}