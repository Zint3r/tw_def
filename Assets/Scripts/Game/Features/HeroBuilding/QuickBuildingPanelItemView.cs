using DG.Tweening;
using Game.Features.Heroes;
using Game.UI.Widgets.Misc;
using Module.UI;
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.Panels
{
	public class QuickBuildingPanelItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		private AssetIconWidget iconWidget;
		private RectTransform rectTransform;
		private Button button;

		private float2 minSize = new float2(100f, 100f);
		private float2 maxSize = new float2(125f, 125f);
		private float sizeTimer = 0.1f;
		private float moveTimer = 0.3f;

		public event Action<HeroRaceVO> OnRaceClick;
		public event Action<HeroClassVO> OnClassClick;
		public HeroRaceVO HeroRaceVO { get; set; }
		public HeroClassVO HeroClassVO { get; set; }

		public void OnStart(HeroRaceVO heroRaceVO, float3 movePos)
		{
			OnStart(movePos);
			HeroRaceVO = heroRaceVO;
			iconWidget.Init(HeroRaceVO.HeroRaceDefinition);
			button.onClick.AddListener(RaceSelect);
		}
		public void OnStart(HeroClassVO heroClassVO, float3 movePos)
		{
			OnStart(movePos);
			HeroClassVO = heroClassVO;
			iconWidget.Init(HeroClassVO.HeroClassDefinition);
			button.onClick.AddListener(ClassSelect);
		}
		private void OnStart(float3 movePos)
		{
			if (iconWidget == null)	{	iconWidget = GetComponent<AssetIconWidget>();	}
			if (rectTransform == null)	{	rectTransform = GetComponent<RectTransform>();	}
			if (button == null)	{	button = GetComponent<Button>();	}
			ChangePosition(movePos, moveTimer);
		}
		public void OnUnloaded()
		{
			button.onClick.RemoveAllListeners();
		}
		public void ReturnToPool()
		{
			button.onClick.RemoveAllListeners();			
			HeroRaceVO = HeroRaceVO.Empty;
			HeroClassVO = HeroClassVO.Empty;
			rectTransform.localPosition = Vector3.zero;
			rectTransform.sizeDelta = minSize;
			TryGetComponent(out UiPoolMember poolMember);
			poolMember.ReturnToPool();
		}
		public void OnPointerExit(PointerEventData eventData)
		{
			ChangeSize(minSize, sizeTimer);
		}
		public void OnPointerEnter(PointerEventData eventData)
		{
			ChangeSize(maxSize, sizeTimer);
		}
		private void RaceSelect()
		{
			OnRaceClick?.Invoke(HeroRaceVO);
		}
		private void ClassSelect()
		{
			OnClassClick?.Invoke(HeroClassVO);
		}

		//Animations
		private void ChangeSize(float2 size, float timer)
		{
			rectTransform.DOSizeDelta(size, timer);
		}
		private void ChangePosition(float3 position, float timer)
		{
			rectTransform.DOLocalMove(position, timer);
		}
	}
}