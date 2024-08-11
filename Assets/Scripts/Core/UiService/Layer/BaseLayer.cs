using System;
using System.Collections;
using System.Collections.Generic;
using Core.CoroutineProvider;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UiService
{
    public enum OpenBehaviour
    {
        Modal,
        Queued,  
        Stacked,
    }
    
    public class BaseLayer : BaseView, IUiLayer
    {
        private class LoadingJob
        {
            public IUiDefinition UiDefinition;
            public bool OpenAfterLoad;
        }
        
        private class UiDataHolder
        {
            public readonly IUiData UiData;

            public UiDataHolder(IUiData uiData)
            {
                UiData = uiData;
            }
        }
        
        public event Action<IUiDefinition, UiLayerDefinition> UiLoaded;
        public event Action<IUiDefinition, UiLayerDefinition> UiOpened;
        public event Action<IUiDefinition, UiLayerDefinition> UiClosed;
        
        private readonly Dictionary<IUiDefinition, UiPresenter> _loadedUiPresenter = new Dictionary<IUiDefinition, UiPresenter>();
        private readonly Dictionary<IUiDefinition, LoadingJob> _activeLoadingJobs = new Dictionary<IUiDefinition, LoadingJob>();
        
        private readonly Dictionary<IUiDefinition, UiDataHolder> _modalUiData = new Dictionary<IUiDefinition, UiDataHolder>();
        private readonly Dictionary<IUiDefinition, Queue<UiDataHolder>> _queuedUiData = new Dictionary<IUiDefinition, Queue<UiDataHolder>>();
        
        public UiLayerDefinition UiLayerDefinition { get; private set; }
        
        private ICoroutineService _coroutineService;
        private IUiLoader _uiLoader;
        
        public virtual void Init(UiLayerDefinition uiLayerDefinition, IUiLoader uiLoader, ICoroutineService coroutineService, Camera uiCamera)
        {
            UiLayerDefinition = uiLayerDefinition;
            
            _uiLoader = uiLoader;
            _coroutineService = coroutineService;
        }
        
        public IEnumerator PreloadUi(IUiDefinition uiDefinition)
        {
            LoadingJob loadingJob = new LoadingJob { UiDefinition = uiDefinition, OpenAfterLoad = false };
            yield return LoadUi(loadingJob);
        }
        
        public IEnumerator OpenUiAsync(IUiDefinition uiDefinition, IUiData uiData = null)
        {
            if (!IsUiLoaded(uiDefinition))
            {
                SaveUiData(uiDefinition, uiData);
                if (IsUiLoading(uiDefinition))
                {
                    LoadingJob activeLoadingJob = _activeLoadingJobs[uiDefinition];
                    activeLoadingJob.OpenAfterLoad = true;
                }
                else
                {
                    LoadingJob loadingJob = new LoadingJob {UiDefinition = uiDefinition, OpenAfterLoad = true};
                    yield return LoadUi(loadingJob);
                }
            }
            else
            {
                InstantOpenLoadedUi(uiDefinition, uiData);
            }
        }
        
        public void OpenUi(IUiDefinition uiDefinition, IUiData uiData = null)
        {
            if (!IsUiLoaded(uiDefinition))
            {
                if (IsUiLoading(uiDefinition))
                {
                    SaveUiData(uiDefinition, uiData);
                    LoadingJob loadingJob = _activeLoadingJobs[uiDefinition];
                    loadingJob.OpenAfterLoad = true;
                }
                else
                {
                    _coroutineService.StartCoroutine(OpenUiAsync(uiDefinition, uiData));
                }
            }
            else
            {
                InstantOpenLoadedUi(uiDefinition, uiData);
            }
        }

        public void CloseUi(IUiDefinition uiDefinition)
        {
            if (IsUiLoaded(uiDefinition))
            {
                UiPresenter[] list = GetAllUiPresenters();
                foreach (UiPresenter uiPresenter in list)
                {
                    if (uiPresenter.UiDefinition == uiDefinition)
                    {
                        uiPresenter.ExecuteStateTrigger(UiPresenter.StateTrigger.Close);
                    }
                }
            }
            else if (IsUiLoading(uiDefinition))
            {
                _activeLoadingJobs[uiDefinition].OpenAfterLoad = false;
            }
        }

        private void InstantOpenLoadedUi(IUiDefinition uiDefinition, IUiData uiData)
        {
            SaveUiData(uiDefinition, uiData);
            switch (uiDefinition.OpenBehaviour)
            {
                case OpenBehaviour.Queued:
                    Queue<UiDataHolder> queue = GetQueue(uiDefinition);
                    if (queue.Count == 1)
                    {
                        OpenUiPresenter(_loadedUiPresenter[uiDefinition], queue.Peek().UiData);
                    }

                    break;
                default:
                    OpenPendingUiPresenter(uiDefinition);
                    break;
            }
        }
        
        private void SaveUiData(IUiDefinition uiDefinition, IUiData uiData)
        {
            switch (uiDefinition.OpenBehaviour)
            {
                case OpenBehaviour.Modal:
                    _modalUiData[uiDefinition] = new UiDataHolder(uiData);
                    break;
                case OpenBehaviour.Queued:
                    GetQueue(uiDefinition).Enqueue(new UiDataHolder(uiData));
                    break;
                case OpenBehaviour.Stacked:
                    GetQueue(uiDefinition).Enqueue(new UiDataHolder(uiData));
                    break;
            }
        }
        
        private void OnLoadUiComplete(IUiDefinition uiDefinition, UiPresenter uiPresenter)
        {
            LoadingJob loadingJob;

            if (!_activeLoadingJobs.TryGetValue(uiDefinition, out loadingJob))
            {
                Debug.LogWarning("No active loading Job found. You are probably trying to use an already unloaded Layer");
                return;
            }

            _activeLoadingJobs.Remove(uiDefinition);

            if (uiPresenter != null)
            {
                _loadedUiPresenter.Add(uiDefinition, uiPresenter);

                uiPresenter.gameObject.SetActive(false);
                uiPresenter.transform.SetParent(transform, false);

                switch (uiDefinition.OpenBehaviour)
                {
                    case OpenBehaviour.Modal:
                        InitPresenter(uiDefinition, uiPresenter);
                        break;
                    case OpenBehaviour.Queued:
                        InitPresenter(uiDefinition, uiPresenter);
                        break;
                    case OpenBehaviour.Stacked:
                        uiPresenter.transform.SetAsFirstSibling();
                        break;
                }
                
                if (loadingJob.OpenAfterLoad)
                {
                    OpenPendingUiPresenter(uiDefinition);
                }
                else
                {
                    ClearUiData(uiDefinition);
                }
            }
        }
        
        private void OpenPendingUiPresenter(IUiDefinition uiDefinition)
        {
            switch (uiDefinition.OpenBehaviour)
            {
                case OpenBehaviour.Modal:
                    if (_modalUiData.TryGetValue(uiDefinition, out var holder))
                    {
                        _modalUiData.Remove(uiDefinition);
                        OpenUiPresenter(_loadedUiPresenter[uiDefinition], holder.UiData);
                    }

                    break;
                case OpenBehaviour.Queued:
                    Queue<UiDataHolder> queue = GetQueue(uiDefinition);
                    if (queue.Count > 0)
                    {
                        OpenUiPresenter(_loadedUiPresenter[uiDefinition], queue.Peek().UiData);
                    }

                    break;
                case OpenBehaviour.Stacked:
                    Queue<UiDataHolder> stackQueue = GetQueue(uiDefinition);
                    while (stackQueue.Count > 0)
                    {
                        UiDataHolder uiDataHolder = stackQueue.Dequeue();
                        UiPresenter uiPresenterInstance = _uiLoader.Instantiate(_loadedUiPresenter[uiDefinition]);
                        uiPresenterInstance.transform.SetParent(transform, false);
                        InitPresenter(uiDefinition, uiPresenterInstance);
                        OpenUiPresenter(uiPresenterInstance, uiDataHolder.UiData);
                    }

                    break;
            }
        }
        
        private void OpenUiPresenter(UiPresenter uiPresenter, IUiData uiData)
        {
            ResetScale(uiPresenter);
            uiPresenter.transform.SetAsLastSibling();
            uiPresenter.ExecuteStateTrigger(UiPresenter.StateTrigger.Open);
            uiPresenter.SetData(uiData);
        }
        
        private void InitPresenter(IUiDefinition uiDefinition, UiPresenter uiPresenter)
        {
            AddListenerFromPresenter(uiPresenter);
            uiPresenter.Init(uiDefinition);
        }
        
        private IEnumerator LoadUi(LoadingJob loadingJob)
        {
            if (IsUiLoaded(loadingJob.UiDefinition) || IsUiLoading(loadingJob.UiDefinition))
            {
                yield break;
            }

            _activeLoadingJobs.Add(loadingJob.UiDefinition, loadingJob);

            yield return _uiLoader.LoadUi(loadingJob.UiDefinition, OnLoadUiComplete);
        }
        
        private Queue<UiDataHolder> GetQueue(IUiDefinition uiDefinition)
        {
            Queue<UiDataHolder> result;
            if (!_queuedUiData.TryGetValue(uiDefinition, out result))
            {
                result = new Queue<UiDataHolder>();
                _queuedUiData[uiDefinition] = result;
            }

            return result;
        }
        
        private void ClearUiData(IUiDefinition uiDefinition)
        {
            _modalUiData.Remove(uiDefinition);
            GetQueue(uiDefinition).Clear();
        }
        
        public void UnloadUi(IUiDefinition uiDefinition)
        {
            if (IsUiLoaded(uiDefinition))
            {
                UiPresenter uiPresenter = _loadedUiPresenter[uiDefinition];
                uiPresenter.ExecuteStateTrigger(UiPresenter.StateTrigger.Unload);
                RemoveListenerFromPresenter(uiPresenter);
                _uiLoader.UnloadUi(uiPresenter);
                ClearUiData(uiDefinition);
                _loadedUiPresenter.Remove(uiDefinition);
            }
        }
        
        private bool IsUiLoaded(IUiDefinition uiDefinition)
        {
            return _loadedUiPresenter.ContainsKey(uiDefinition);
        }
        
        private bool IsUiLoading(IUiDefinition uiDefinition)
        {
            return _activeLoadingJobs.ContainsKey(uiDefinition);
        }
        
        private void AddListenerFromPresenter(UiPresenter uiPresenter)
        {
            uiPresenter.UiLoaded += OnUiLoaded;
            uiPresenter.UiOpened += OnUiOpened;
            uiPresenter.UiClosed += OnUiClosed;
            uiPresenter.RequestCloseEvent += RequestCloseEvent;
        }
        
        private void OnUiLoaded(IUiDefinition uiDefinition)
        {
            if (UiLoaded != null)
            {
                UiLoaded(uiDefinition, UiLayerDefinition);
            }
        }

        private void OnUiOpened(IUiDefinition uiDefinition)
        {
            if (UiOpened != null)
            {
                UiOpened(uiDefinition, UiLayerDefinition);
            }
        }

        private void OnUiClosed(IUiDefinition uiDefinition)
        {
            if (UiClosed != null)
            {
                UiClosed(uiDefinition, UiLayerDefinition);
            }
        }
        
        private void RequestCloseEvent(UiPresenter uiPresenter)
        {
            IUiDefinition uiDefinition = uiPresenter.UiDefinition;

            switch (uiDefinition.OpenBehaviour)
            {
                case OpenBehaviour.Modal:
                    break;
                case OpenBehaviour.Queued:
                    GetQueue(uiDefinition).Dequeue();
                    OpenPendingUiPresenter(uiDefinition);
                    break;
                case OpenBehaviour.Stacked:
                    uiPresenter.ExecuteStateTrigger(UiPresenter.StateTrigger.Unload);
                    RemoveListenerFromPresenter(uiPresenter);
                    Destroy(uiPresenter.gameObject);
                    break;
            }
            
        }
        
        private void RemoveListenerFromPresenter(UiPresenter uiPresenter)
        {
            uiPresenter.UiLoaded -= OnUiLoaded;
            uiPresenter.UiOpened -= OnUiOpened;
            uiPresenter.UiClosed -= OnUiClosed;
            uiPresenter.RequestCloseEvent -= RequestCloseEvent;
        }
        
        private UiPresenter[] GetAllUiPresenters()
        {
            UiPresenter[] result = this ? GetComponentsInChildren<UiPresenter>(true) : new UiPresenter[0];
            return result;
        }
        
        public void UnloadAllUi()
        {
            UiPresenter[] list = GetAllUiPresenters();

            foreach (UiPresenter uiPresenter in list)
            {
                if (uiPresenter ?? false)
                {
                    uiPresenter.ExecuteStateTrigger(UiPresenter.StateTrigger.Unload);
                    _uiLoader.UnloadUi(uiPresenter);
                }
            }

            _modalUiData.Clear();
            _queuedUiData.Clear();
            _loadedUiPresenter.Clear();
        }
        
        protected virtual void ResetScale(UiPresenter uiPresenter)
        {
        }
        
        private void OnDestroy()
        {
            if (UiOpened != null)
            {
                foreach (Delegate d in UiOpened.GetInvocationList())
                {
                    UiOpened -= (Action<IUiDefinition, UiLayerDefinition>) d;
                }
            }

            if (UiClosed != null)
            {
                foreach (Delegate d in UiClosed.GetInvocationList())
                {
                    UiClosed -= (Action<IUiDefinition, UiLayerDefinition>) d;
                }
            }

            if (UiLoaded != null)
            {
                foreach (Delegate d in UiLoaded.GetInvocationList())
                {
                    UiLoaded -= (Action<IUiDefinition, UiLayerDefinition>) d;
                }
            }

            UnloadAllUi();
            _activeLoadingJobs.Clear();
        }
    }
}