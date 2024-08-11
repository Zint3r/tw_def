using Core.CoroutineProvider;
using Core.MessageHub;
using Game.Features.Heroes;
using Game.Features.PlayerControl;
using Game.UI.Factories;
using Module.GameObjectInstaller.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Features.HeroBuilding
{
	public class HeroBuildingPresenter : MonoBehaviour
	{
		[Inject]
		private IMessageHubService messageHubService;

		[Inject]
		private ICoroutineService coroutineService;

		[Inject]
		private GameObjectFactory gameObjectFactory;

		[Inject]
		private GoPool pool;

		[Inject]
		private PlayerControlInput playerControlInput;

		[SerializeField]
		private GameObject buldingObj;

		[SerializeField]
		private List<Transform> allPlaces = new List<Transform>();

		private List<Transform> places = new List<Transform>();
		private GameObject tempHero;
		private GameObject tempPlace;
		private new Camera camera;
		private int layerMask = 1 << 7;
		private bool isPlaceSelected = false;
		private HeroClassVO heroClassVO;

		public void OnStart()
		{
			heroClassVO = HeroClassVO.Empty;
			messageHubService.Subscribe<HeroBuildingMessages.HeroTryBuildingMessage>(OnHeroTryBuilding);
			//buldingObj.SetActive(false);
			camera = Camera.main;
		}
		private void OnDestroy()
		{
			messageHubService.Unsubscribe<HeroBuildingMessages.HeroTryBuildingMessage>(OnHeroTryBuilding);
		}
		private void OnHeroTryBuilding(HeroBuildingMessages.HeroTryBuildingMessage message)
		{
			heroClassVO = message.HeroClassVO;
			playerControlInput.OnLeftMouseButtonContext += BuldinHero;
			Debug.Log(message.HeroClassVO.HeroClassDefinition + " selected");			
			GameObject heroGO = gameObjectFactory.GetGameObjectByDefinition(message.HeroClassVO.HeroClassDefinition);
			tempHero = pool.GetPooledPrefab(heroGO, new Vector3(0, 0, 0), Quaternion.identity);
			tempHero.transform.position = new Vector3(9999, 9999, 9999);
			SetMaterial(tempHero, 0.2f);
			buldingObj.SetActive(true);
			coroutineService.StartCoroutine(SetBuldingPlace());
		}
		private void SetMaterial(GameObject gameObject, float alpha)
		{
			var material = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
			material.color = new Color(1f, 1f, 1f, alpha);
		}
		private void BuldinHero()
		{
			if (isPlaceSelected == true)
			{
				SetMaterial(tempHero, 1f);
				tempHero.GetComponent<HeroPresenter>().OnStart(heroClassVO);
				messageHubService.Publish(new HeroBuildingMessages.OnCompleteHeroBuildingMessage(true, tempHero));
				tempHero = null;
				heroClassVO = HeroClassVO.Empty;
				isPlaceSelected = false;
				playerControlInput.OnLeftMouseButtonContext -= BuldinHero;
				coroutineService.StopCoroutine(SetBuldingPlace());
				CheckPlaces();
			}
		}
		private void CheckPlaces()
		{
			if (tempPlace != null)
			{
				tempPlace.SetActive(false);
			}
			for (int i = 0; i < allPlaces.Count; i++)
			{
				if (allPlaces[i].gameObject.activeSelf == false)
				{
					places.Add(allPlaces[i]);
				}
				else
				{
					places.Remove(allPlaces[i]);
				}
			}
		}
		private IEnumerator SetBuldingPlace()
		{
			while (true)
			{
				if (tempHero != null)
				{
					Ray ray = camera.ScreenPointToRay(playerControlInput.MousePosition);
					if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
					{
						tempHero.transform.position = hit.collider.gameObject.transform.position;
						isPlaceSelected = true;
						tempPlace = hit.collider.gameObject;
					}
					else
					{
						tempHero.transform.position = new Vector3(9999, 9999, 9999);
						isPlaceSelected = false;
						tempPlace = null;
					}
				}
				yield return new WaitForSeconds(Time.fixedDeltaTime);
			}			
		}
	}
}