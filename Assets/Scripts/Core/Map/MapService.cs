using System;
using System.Collections;
using System.Collections.Generic;
using Core.CoroutineProvider;
using Core.UiService;
using UnityEngine;
using Zenject;

namespace Core.Map
{
    public interface IMapService
    {
        IEnumerator OpenMapAsync(IUiDefinition definition, UiLayerDefinition layer, IUiData uiData = null);
        void CloseUi(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition);
    }

    public class MapService : IMapService
    {
        public event Action<IUiDefinition, UiLayerDefinition> UiLoaded = delegate { };
        public event Action<IUiDefinition, UiLayerDefinition> UiOpened = delegate { };
        public event Action<IUiDefinition, UiLayerDefinition> UiClosed = delegate { };
        
        [Inject(Id = "MapCamera")]
        private Camera mapCamera;
        
        private readonly Dictionary<UiLayerDefinition, IUiLayer> layerMap = new Dictionary<UiLayerDefinition, IUiLayer>();
        private readonly MapRoot mapRoot;
        
        private readonly IUiLoader _uiLoader;
        private readonly ICoroutineService _coroutineService;
        
        public MapService(MapRoot root, IUiLoader uiLoader, ICoroutineService coroutineService)
        {
            mapRoot = root;
            _uiLoader = uiLoader;
            _coroutineService = coroutineService;
        }
        
        public IEnumerator OpenMapAsync(IUiDefinition definition, UiLayerDefinition layer, IUiData uiData = null)
        {
            if (!HasUiLayer(layer))
            {
                AddUiLayer(layer);
            }

            IUiLayer uiLayer = GetLayer(layer);
            return uiLayer.OpenUiAsync(definition, uiData);
        }
        
        public void CloseUi(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition)
        {
            if (!HasUiLayer(uiLayerDefinition))
            {
                Debug.LogWarning(string.Format("Layer {0} that was not opened.", uiLayerDefinition.Name));
                return;
            }

            IUiLayer uiLayer = GetLayer(uiLayerDefinition);
            uiLayer.CloseUi(uiDefinition);
        }
        
        private IUiLayer GetLayer(UiLayerDefinition layerDefinition)
        {
            IUiLayer uiLayer;
            layerMap.TryGetValue(layerDefinition, out uiLayer);
            return uiLayer;
        }
        
        private void AddUiLayer(UiLayerDefinition layerDefinition)
        {
            if (HasUiLayer(layerDefinition))
            {
                return;
            }

            string layerName = string.Format("{0} (Ordinal: {1})", layerDefinition.Name, layerDefinition.Ordinal);
            GameObject layerGameObject = new GameObject(layerName);
            layerGameObject.transform.SetParent(mapRoot.transform);
            layerGameObject.layer = LayerMask.NameToLayer("Map");
            
            MapLayer uiLayer = layerGameObject.AddComponent<MapLayer>();
            uiLayer.Init(layerDefinition, _uiLoader, _coroutineService, mapCamera);
            uiLayer.UiLoaded += OnUiLoaded;
            uiLayer.UiOpened += OnUiOpened;
            uiLayer.UiClosed += OnUiClosed;
            layerMap.Add(layerDefinition, uiLayer);
        }
        
        private void OnUiLoaded(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition)
        {
            UiLoaded(uiDefinition, uiLayerDefinition);
        }

        private void OnUiOpened(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition)
        {
            UiOpened(uiDefinition, uiLayerDefinition);
        }

        private void OnUiClosed(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition)
        {
            UiClosed(uiDefinition, uiLayerDefinition);
        }
        
        private bool HasUiLayer(UiLayerDefinition layerDefinition)
        {
            return layerMap.ContainsKey(layerDefinition);
        }
    }
}