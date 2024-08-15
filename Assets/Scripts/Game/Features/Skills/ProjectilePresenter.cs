using Module.GameObjectInstaller.Pool;
using UnityEngine;

namespace Game.Features.Skills
{
	public class ProjectilePresenter : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem targetParticleSystem;
		private MeshRenderer meshRenderer;

		public void OnStart()
		{
			if (meshRenderer == null) { meshRenderer = GetComponent<MeshRenderer>(); }
			meshRenderer.enabled = true;
		}
		public float GetLifeTime()
		{
			return targetParticleSystem.main.startLifetime.constant;
		}
		public void ReturnToPool()
		{
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
			meshRenderer.enabled = false;
			Invoke("ReturnToPool", GetLifeTime());
		}
	}
}