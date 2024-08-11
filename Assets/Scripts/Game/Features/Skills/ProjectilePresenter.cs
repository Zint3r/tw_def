using DG.Tweening;
using Game.Features.Enemy;
using UnityEngine;

public class ProjectilePresenter : MonoBehaviour
{
	private Transform targetPosition;
	private float timeLeft;
	private int damage = 0;
	private bool IsGetDamage = false;

	[SerializeField]
	private ParticleSystem targetParticleSystem;

	private EnemyPresenter enemyPresenter;

	public void OnStart(Transform target, int damage)
    {
		//targetParticleSystem = GetComponent<ParticleSystem>();
		this.damage = damage;
		targetPosition = target;
		timeLeft = targetParticleSystem.main.startLifetime.constant;

		enemyPresenter = targetPosition.GetComponent<EnemyPresenter>();

		//transform.DOMove(target, 0.5f);
		//transform.DOJump(target, 5f, 1, 0.5f, true);
	}    
    private void FixedUpdate()
    {
		//transform.DOMove(targetPosition.position, 0.5f);
		//Debug.Log(Vector3.Distance(transform.position, targetPosition.position));
		if (Vector3.Distance(transform.position, targetPosition.position) > 0.2f)
		{

			transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, 25f * Time.deltaTime);
		}
		else if (targetParticleSystem.isPlaying == false)
		{
			targetParticleSystem.Play();
			if (IsGetDamage == false)
			{
				IsGetDamage = true;

				if (enemyPresenter != null)
				{
					enemyPresenter.TakeDamage(damage);
				}
			}
		}
		else if (timeLeft > 0)
		{
			timeLeft -= 0.02f;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}