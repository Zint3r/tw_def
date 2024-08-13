using Game.Features.Enemy;
using Module.GameObjectInstaller.Pool;
using UnityEngine;

namespace Game.Features.Skills
{
	public class ProjectilePresenter : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem targetParticleSystem;

		private int damage = 0;
		private bool IsGetDamage = false;
		private EnemyPresenter enemyPresenter;

		public void OnStart(Transform target, int damage)
		{
			this.damage = damage;
			IsGetDamage = false;
			GetComponent<MeshRenderer>().enabled = true;
			enemyPresenter = target.GetComponent<EnemyPresenter>();
		}
		public float GetLifeTime()
		{
			return targetParticleSystem.main.startLifetime.constant;
		}
		public void ReturnToPool()
		{
			enemyPresenter = null;
			if (TryGetComponent(out GoPoolMember poolMember) == true)
			{
				poolMember.ReturnToPool();
			}
		}
		public bool GetParticleSystemInfo()
		{
			return targetParticleSystem.isPlaying;
		}
		public void PlayParticleSystem()
		{
			targetParticleSystem.Play();
			GetComponent<MeshRenderer>().enabled = false;
			Invoke("ReturnToPool", GetLifeTime());
		}
		public void DealDamageToEnemy()
		{
			if (IsGetDamage == false)
			{
				IsGetDamage = true;
				PlayParticleSystem();
				if (enemyPresenter != null)
				{
					enemyPresenter.TakeDamage(damage);
				}
			}
		}
	}
}