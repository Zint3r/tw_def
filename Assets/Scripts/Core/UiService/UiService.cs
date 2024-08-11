using Core.CoroutineProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Core.UiService
{
	public class UiService : IUiService, IDisposable
    {
        public event Action<IUiDefinition, UiLayerDefinition> UiLoaded = delegate { };
        public event Action<IUiDefinition, UiLayerDefinition> UiOpened = delegate { };
        public event Action<IUiDefinition, UiLayerDefinition> UiClosed = delegate { };
        
        [Inject(Id = "GUICamera")]
        private Camera _uiCamera;
        
        private readonly UiRoot _uiRoot;
        private readonly IUiLoader _uiLoader;
        private readonly ICoroutineService _coroutineService;
        private readonly UiServiceSettings _uiServiceSettings;
        
        private readonly Dictionary<UiLayerDefinition, IUiLayer> _layerMap = new Dictionary<UiLayerDefinition, IUiLayer>();
        
        public UiService(UiRoot uiRoot, IUiLoader uiLoader, UiServiceSettings uiServiceSettings, ICoroutineService coroutineService)
        {
            _uiRoot = uiRoot;
            _uiLoader = uiLoader;
            _uiServiceSettings = uiServiceSettings;
            _coroutineService = coroutineService;
        }
        
        public IEnumerator PreloadUi(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition)
        {
            if (!HasUiLayer(uiLayerDefinition))
            {
                AddUiLayer(uiLayerDefinition);
            }

            IUiLayer uiLayer = GetLayer(uiLayerDefinition);
            return uiLayer.PreloadUi(uiDefinition);
        }
        
        public IEnumerator OpenUiAsync(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition, IUiData uiData = null)
        {
            if (!HasUiLayer(uiLayerDefinition))
            {
                AddUiLayer(uiLayerDefinition);
            }

            IUiLayer uiLayer = GetLayer(uiLayerDefinition);
            return uiLayer.OpenUiAsync(uiDefinition, uiData);
        }
        
        public void OpenUi(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition, IUiData uiData = null)
        {
            if (!HasUiLayer(uiLayerDefinition))
            {
                AddUiLayer(uiLayerDefinition);
            }

            IUiLayer uiLayer = GetLayer(uiLayerDefinition);
            uiLayer.OpenUi(uiDefinition, uiData);
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
        
        private bool HasUiLayer(UiLayerDefinition layerDefinition)
        {
            return _layerMap.ContainsKey(layerDefinition);
        }
        
        private IUiLayer GetLayer(UiLayerDefinition layerDefinition)
        {
            IUiLayer uiLayer;
            _layerMap.TryGetValue(layerDefinition, out uiLayer);
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
            layerGameObject.transform.SetParent(_uiRoot.transform);
            layerGameObject.layer = LayerMask.NameToLayer("UI");
            
            UiLayer uiLayer = layerGameObject.AddComponent<UiLayer>();
            uiLayer.Init(layerDefinition, _uiLoader, _coroutineService, _uiCamera);
            uiLayer.UiLoaded += OnUiLoaded;
            uiLayer.UiOpened += OnUiOpened;
            uiLayer.UiClosed += OnUiClosed;
            _layerMap.Add(layerDefinition, uiLayer);

            SortLayer();
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
        
        public void UnloadUi(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition)
        {
            if (!HasUiLayer(uiLayerDefinition))
            {
                return;
            }

            IUiLayer uiLayer = GetLayer(uiLayerDefinition);
            uiLayer.UnloadUi(uiDefinition);
        }
        
        private void SortLayer()
        {
            UiLayer[] layers = _uiRoot.GetComponentsInChildren<UiLayer>();
            layers = layers.OrderBy(x => x.UiLayerDefinition.Ordinal).ToArray();

            for (int i = 0; i < layers.Length; i++)
            {
                UiLayer uiLayer = layers[i];
                uiLayer.gameObject.transform.SetSiblingIndex(i);
            }
        }
        
        public void Dispose()
        {
            foreach (Delegate d in UiOpened.GetInvocationList())
            {
                UiOpened -= (Action<IUiDefinition, UiLayerDefinition>) d;
            }

            foreach (Delegate d in UiClosed.GetInvocationList())
            {
                UiClosed -= (Action<IUiDefinition, UiLayerDefinition>) d;
            }

            foreach (Delegate d in UiLoaded.GetInvocationList())
            {
                UiLoaded -= (Action<IUiDefinition, UiLayerDefinition>) d;
            }
        }
    }
}