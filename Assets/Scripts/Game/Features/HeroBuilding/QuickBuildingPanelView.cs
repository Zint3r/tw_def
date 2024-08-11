using Core.CoroutineProvider;
using Core.MessageHub;
using Core.UiService;
using Game.Features.GameDesign;
using Game.Features.HeroBuilding;
using Game.Features.Heroes;
using Game.Features.Resource;
using Game.Utils;
using Module.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Game.UI.Panels
{
	public class QuickBuildingPanelView : UiPresenter
	{
		[SerializeField]
		private QuickBuildingPanelItemView itemViewPrefab;

		[SerializeField]
		private Transform parent;

		[SerializeField]
		private GameObject panel;

		[Inject]
		private ICoroutineService coroutineService;

		[Inject]
		private IMessageHubService messageHubService;

		[Inject]
		private IResourceService resourceService;

		[Inject]
		private UiPool pool;		
		
		private List<QuickBuildingPanelItemView> panelItemView = new List<QuickBuildingPanelItemView>();
		private RectTransform rectTransform;		
		private new Camera camera;
		private Transform worldObject;
		private Vector3 lastPosition;
		private Canvas canvas;
		private int screenWidth;
		private int screenHeight;
		private bool isVisible = false;
		private bool isBuilding = false;

		protected override void OnLoaded()
		{
			screenWidth = Screen.width;
			screenHeight = Screen.height;			
			panel.SetActive(false);
			rectTransform = panel.GetComponent<RectTransform>();
			canvas = GetComponent<Canvas>();
			camera = Camera.main;
			messageHubService.Subscribe<HeroBuildingMessages.ClickOnBuldingPlaceMessage>(OnQuickBuilding);
			messageHubService.Subscribe<HeroBuildingMessages.OnCompleteHeroBuildingMessage>(OnHeroBuilding);
		}
		protected override void OnUnloaded()
		{
			messageHubService.Unsubscribe<HeroBuildingMessages.ClickOnBuldingPlaceMessage>(OnQuickBuilding);
			messageHubService.Unsubscribe<HeroBuildingMessages.OnCompleteHeroBuildingMessage>(OnHeroBuilding);
			for (int i = 0; i < panelItemView.Count; i++)
			{
				panelItemView[i].OnUnloaded();
			}
		}
		private void Clear()
		{
			if (panelItemView.Count > 0)
			{
				for (int i = 0; i < panelItemView.Count; i++)
				{
					panelItemView[i].ReturnToPool();
					panelItemView[i].OnRaceClick -= OnQuickBuildingRaceSelected;
					panelItemView[i].OnClassClick -= OnQuickBuildingClassSelected;
				}
				panelItemView.Clear();
			}
		}
		private float3[] DrawCircle(int pointCount, float radius)
		{
			float3[] points = new float3[pointCount];
			for (int i = 0; i < pointCount; i++)
			{
				var rad = Mathf.Deg2Rad * (i * 360f / pointCount);
				points[i] = new float3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
			}
			return points;
		}
		private void OnQuickBuilding(HeroBuildingMessages.ClickOnBuldingPlaceMessage message)
		{
			if (worldObject != message.Transform && isBuilding == false)
			{
				Clear();
				worldObject = message.Transform;
				panel.SetActive(true);
				isVisible = true;
				List<HeroRaceVO> races = DPL.HeroCollectionDataProvider.GetHeroRaces();
				float3[] poses = DrawCircle(races.Count, 125f);
				for (int i = 0; i < races.Count; i++)
				{
					QuickBuildingPanelItemView itemView = pool.GetPooledPrefab(itemViewPrefab, parent);
					itemView.OnStart(races[i], poses[i]);
					panelItemView.Add(itemView);
					itemView.OnRaceClick += OnQuickBuildingRaceSelected;
				}
				coroutineService.StartCoroutine(ChangePanelPosition());
			}
		}
		private void OnHeroBuilding(HeroBuildingMessages.OnCompleteHeroBuildingMessage message)
		{
			isBuilding = !message.IsBuilding;
		}
		private void OnQuickBuildingRaceSelected(HeroRaceVO heroRaceVO)
		{
			Clear();
			List<HeroClassVO> classes = DPL.HeroCollectionDataProvider.GetClassesByRace(heroRaceVO);
			float3[] poses = DrawCircle(classes.Count, 125f);
			for (int i = 0; i < classes.Count; i++)
			{
				QuickBuildingPanelItemView itemView = pool.GetPooledPrefab(itemViewPrefab, parent);
				itemView.OnStart(classes[i], poses[i]);
				panelItemView.Add(itemView);
				itemView.OnClassClick += OnQuickBuildingClassSelected;
			}
		}
		private void OnQuickBuildingClassSelected(HeroClassVO heroClassVO)
		{
			Clear();
			HeroVO heroVO = DPL.HeroCollectionDataProvider.GetHero(heroClassVO.HeroClassDefinition);
			ResourceDefinition resourceDefinition = DPL.GameDefinitionDataProvider.Get<ResourceDefinition>(ResourceConstants.SoftCurrency);
			ResourceVO softCurrency = DPL.ResourceDataProvider.GetResource(resourceDefinition.DefinitionId);			

			if (softCurrency.Amount >= heroVO.Price)
			{
				isBuilding = true;
				messageHubService.Publish(new HeroBuildingMessages.HeroTryBuildingMessage(heroClassVO));
				isVisible = false;
				panel.SetActive(false);
				ResourceVO changeSoftCurrency = new ResourceVO { ResourceDefinition = resourceDefinition, Amount = -heroVO.Price };
				resourceService.PredictResourceChange(changeSoftCurrency);
			}
			else
			{
				isVisible = false;
				panel.SetActive(false);
				worldObject = null;
			}
		}
		private Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
		{
			Vector3 screenPos = camera.WorldToScreenPoint(worldPos);
			Vector2 movePos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
			return parentCanvas.transform.TransformPoint(movePos);
		}
		private IEnumerator ChangePanelPosition()
		{
			while (panel.activeSelf == true)
			{				
				float3 currentPosition = WorldToUISpace(canvas, worldObject.position);
				if (currentPosition.Equals(lastPosition) != true)
				{
					lastPosition = currentPosition;
					rectTransform.position = currentPosition;
					float2 obbPos = RectTransformUtility.WorldToScreenPoint(camera, worldObject.position);
					
					if (obbPos.x > 0 && obbPos.x < screenWidth && obbPos.y > 0 && obbPos.y < screenHeight)
					{
						isVisible = true;
					}
					else
					{
						isVisible = false;
					}
					if (isVisible == false)
					{
						panel.SetActive(false);
					}
				}
				yield return new WaitForSeconds(Time.smoothDeltaTime);
			}
		}
		private void OnDisable()
		{
			coroutineService.StopCoroutine(ChangePanelPosition());
		}
	}
}