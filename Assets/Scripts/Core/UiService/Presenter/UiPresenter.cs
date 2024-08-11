using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UiService
{
    public class UiPresenter<T> : UiPresenter where T : IUiData
    {
        protected T UiData;
        
        public sealed override void SetData(IUiData uiData)
        {
            if (uiData != null)
            {
                UiData = (T) uiData;
            }

            OnDataUpdated(UiData);
        }
        
        protected virtual void OnDataUpdated(T data)
        {
        }
    }

    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UiPresenter : MonoBehaviour
    {
        public event Action<IUiDefinition> UiLoaded;
        public event Action<IUiDefinition> UiOpened;
        public event Action<IUiDefinition> UiClosed;
        public event Action<UiPresenter> RequestCloseEvent;
        
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private IUiPresenterAnimation _uiPresenterAnimation;
        
        private enum State
        {
            Loaded,
            Opening,
            Opened,
            Closing,
            OpenWhileClosing,
            Closed,
            Unloaded
        }
        
        public enum StateTrigger
        {
            Open,
            OpeningComplete,
            Close,
            ClosingComplete,
            Unload
        }
        
        public IUiDefinition UiDefinition { get; private set; }
        
        private readonly FiniteStateMachine<State, StateTrigger> _fsm = new FiniteStateMachine<State, StateTrigger>();
        private List<SubPresenter> _subPresenterList = new List<SubPresenter>();
        
        public void Init(IUiDefinition uiDefinition)
        {
            UiDefinition = uiDefinition;

            _fsm.AddState(State.Loaded)
                .OnEntryAction(Loaded)
                .AddTransition(State.Opening, StateTrigger.Open);

            _fsm.AddState(State.Opening)
                .OnEntryAction(Opening)
                .AddTransition(State.Opened, StateTrigger.OpeningComplete)
                .AddTransition(State.Closing, StateTrigger.Close);

            _fsm.AddState(State.Opened)
                .OnEntryAction(Opened)
                .AddTransition(State.Closing, StateTrigger.Close);

            _fsm.AddState(State.Closing)
                .OnEntryAction(Closing)
                .AddTransition(State.Closed, StateTrigger.ClosingComplete)
                .AddTransition(State.OpenWhileClosing, StateTrigger.Open);

            _fsm.AddState(State.OpenWhileClosing)
                .OnEntryAction(OpenWhileClosing)
                .AddTransition(State.Closed, StateTrigger.ClosingComplete);

            _fsm.AddState(State.Closed)
                .OnEntryAction(Closed)
                .AddTransition(State.Opening, StateTrigger.Open);

            _fsm.AddState(State.Unloaded)
                .OnEntryAction(Unloaded);

            _fsm.AnyState
                .AddTransition(State.Unloaded, StateTrigger.Unload, Closed);

            _fsm.Finalize(State.Loaded);
        }
        
        public virtual void SetData(IUiData uiData)
        {
        }
        
        public void ExecuteStateTrigger(StateTrigger trigger)
        {
            if (_fsm.IsFinalized)
            {
                _fsm.ExecuteTrigger(trigger);
            }
        }
        
        private void Loaded()
        {
            _canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _uiPresenterAnimation = GetComponent<IUiPresenterAnimation>();
            RefreshSubPresenterList();
            OnLoaded();
            _subPresenterList.ForEach(s => s.OnLoaded());

            if (UiLoaded != null)
            {
                UiLoaded(UiDefinition);
            }
        }

        private void Opening()
        {
            if (UiOpened != null)
            {
                UiOpened(UiDefinition);
            }

            Interactable = false;

            gameObject.SetActive(true);
            Visible = true;
            _subPresenterList.ForEach(s => s.OnOpening());

            OnOpening();

            if (_uiPresenterAnimation != null)
            {
                _uiPresenterAnimation.PlayOpenAnimation(() => ExecuteStateTrigger(StateTrigger.OpeningComplete));
            }
            else
            {
                ExecuteStateTrigger(StateTrigger.OpeningComplete);
            }
        }

        private void Opened()
        {
            Interactable = true;

            OnOpened();
            _subPresenterList.ForEach(s => s.OnOpened());
        }

        private void Closing()
        {
            Interactable = false;

            _subPresenterList.ForEach(s => s.OnClosing());

            OnClosing();

            if (_uiPresenterAnimation != null)
            {
                _uiPresenterAnimation.PlayCloseAnimation(() => ExecuteStateTrigger(StateTrigger.ClosingComplete));
            }
            else
            {
                ExecuteStateTrigger(StateTrigger.ClosingComplete);
            }
        }
        
        private void OpenWhileClosing()
        {
            ExecuteStateTrigger(StateTrigger.ClosingComplete);
            ExecuteStateTrigger(StateTrigger.Open);
        }

        private void Closed()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                if (RequestCloseEvent != null)
                {
                    RequestCloseEvent(this);
                }

                OnClosed();
                _subPresenterList.ForEach(s => s.OnClosed());

                if (UiClosed != null)
                {
                    UiClosed(UiDefinition);
                }
            }
        }

        private void Unloaded()
        {
            if (RequestCloseEvent != null)
            {
                foreach (Delegate d in RequestCloseEvent.GetInvocationList())
                {
                    RequestCloseEvent -= (Action<UiPresenter>) d;
                }
            }

            if (UiLoaded != null)
            {
                foreach (Delegate d in UiLoaded.GetInvocationList())
                {
                    UiLoaded -= (Action<IUiDefinition>) d;
                }
            }

            if (UiOpened != null)
            {
                foreach (Delegate d in UiOpened.GetInvocationList())
                {
                    UiOpened -= (Action<IUiDefinition>) d;
                }
            }

            if (UiClosed != null)
            {
                foreach (Delegate d in UiClosed.GetInvocationList())
                {
                    UiClosed -= (Action<IUiDefinition>) d;
                }
            }

            _subPresenterList.ForEach(s => s.OnUnloaded());
            OnUnloaded();
        }
        
        protected void RefreshSubPresenterList()
        {
            _subPresenterList = this ? GetComponentsInChildren<SubPresenter>(true).ToList() : new List<SubPresenter>();
        }
        
        protected virtual void OnLoaded()
        {
        }

        protected virtual void OnOpening()
        {
        }

        protected virtual void OnOpened()
        {
        }

        protected virtual void OnClosing()
        {
        }

        protected virtual void OnClosed()
        {
        }

        protected virtual void OnUnloaded()
        {
        }
        
        protected virtual bool Interactable
        {
            get { return _canvasGroup.interactable; }
            set { _canvasGroup.interactable = value; }
        }

        protected virtual bool Visible
        {
            get { return _canvas.enabled; }
            set { _canvas.enabled = value; }
        }

        protected virtual float Alpha
        {
            get { return _canvasGroup.alpha; }
            set { _canvasGroup.alpha = value; }
        }
    }
}