using Game.Features.Enemy;
using Game.Features.Heroes;
using Game.Utils;
using System.Collections.Generic;
using UnityEngine;

public class HeroPresenter : MonoBehaviour
{
	[SerializeField]
	public float Radius;

	public HeroVO Hero;
	public List<Transform> EnemyList = new List<Transform>();
	private Collider[] enemyColliders;

	public void OnStart(HeroClassVO heroClassVO)
	{
		Radius = Radius * 3f;
		Hero = DPL.HeroCollectionDataProvider.GetHero(heroClassVO.HeroClassDefinition);
		enemyColliders = new Collider[10];
		GetComponent<SphereCollider>().enabled = true;
	}
	private void OnTriggerExit(Collider other)
    {
		if (EnemyList.Contains(other.transform) == true)
        {
			EnemyList.Remove(other.transform);
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		EnemyList.Add(other.transform);
		if (other.TryGetComponent(out EnemyPresenter enemy) == true)
        {
			EnemyList.Add(other.transform);
		}
	}
	//private IEnumerator DisablingControlStatus()
	//{
	//	int hitCount = Physics.OverlapSphereNonAlloc(heroTransform.position, Radius, enemyColliders, layerMask);
	//	for (int i = 0; i < hitCount; i++)
	//	{
	//		if (enemyColliders[i].TryGetComponent(out EnemyPresenter enemy) == true)
	//		{
	//			if (enemy.AliveStatus() == true && target == null)
	//			{
	//				//target = enemy;
	//				break;
	//			}
	//		}
	//	}
	//	yield return new WaitForSeconds(0.25f);
	//}
}