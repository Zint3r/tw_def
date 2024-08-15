using Module.GameObjectInstaller.Pool;
using UnityEngine;

namespace Game.Features.Enemy
{
	public class EnemyPresenter : MonoBehaviour
	{
		private Animator anim;
		private AnimationClip clip;
		private int TriggerId = 0;
		public void OnStart()
		{
			if (anim == null)	{	anim = GetComponent<Animator>();	}
			if (TriggerId == 0)	{	TriggerId = Animator.StringToHash("Dead");	}
			clip = anim.runtimeAnimatorController.animationClips[1];
			Debug.Log(clip.name);
			clip.events[0].functionName = "Dead";
		}
		public void Dead()
		{
			ReturnToPool();
		}
		public void ReturnToPool()
		{
			if (TryGetComponent(out GoPoolMember poolMember) == true)
			{
				poolMember.ReturnToPool();
			}
		}
		public void EnemyDead()
		{
			anim.SetTrigger(TriggerId);
		}
	}
}