using System;
using System.Collections;
using System.Collections.Generic;
using Core.CoroutineProvider;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UiService
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UiLayer : BaseLayer
    {
        private static readonly Vector2 ReferenceResolution = new Vector2(1080, 1920);
        private static readonly float ReferenceAspect = ReferenceResolution.y / ReferenceResolution.x;
        private static readonly float TabletReferenceAspect = 4f / 3f;
        
        private Canvas _canvas;
        private CanvasScaler _canvasScaler;
        private CanvasGroup _canvasGroup;

        private static float MatchWidthOrHeight
        {
            get
            {
                float currentAspect = (float) Screen.height / (float) Screen.width;
                if (currentAspect >= ReferenceAspect)
                {
                    return 0;
                }

                if (currentAspect > TabletReferenceAspect)
                {
                    return 1 - (currentAspect - TabletReferenceAspect) / (ReferenceAspect - TabletReferenceAspect);
                }

                return 1f;
            }
        }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _canvas = GetComponent<Canvas>();
            _canvasScaler = GetComponent<CanvasScaler>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void Init(UiLayerDefinition uiLayerDefinition, IUiLoader uiLoader, ICoroutineService coroutineService, Camera uiCamera)
        {
            base.Init(uiLayerDefinition, uiLoader, coroutineService, uiCamera);
            
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.sortingOrder = -uiLayerDefinition.Ordinal;
            _canvas.worldCamera = uiCamera;
        
            _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            _canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            _canvasScaler.matchWidthOrHeight = MatchWidthOrHeight;
            _canvasScaler.referenceResolution = ReferenceResolution;
        }
        
        protected override void ResetScale(UiPresenter uiPresenter)
        {
            RectTransform rectTransform = uiPresenter.GetComponent<RectTransform>();
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;
        }
    }
}