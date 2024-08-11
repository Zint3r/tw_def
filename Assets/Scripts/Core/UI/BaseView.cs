using System;
using UnityEngine;

namespace Core.UI
{
    public class BaseView : MonoBehaviour
    {
        private void Awake()
        {
            OnAwake();
        }
        
        private void Start()
        {
            OnStart();
        }

        private void OnDestroy()
        {
            OnReleaseResources();
        }
        
        protected virtual void OnAwake()
        {
        }
        
        protected virtual void OnStart()
        {
        }
        
        protected virtual void OnReleaseResources()
        {
        }
    }

    public class BaseView<T> : BaseView
    {
        public T Data;
        
        public void SetData(T t)
        {
            Data = t;
            OnDataUpdated(t);
        }

        protected virtual void OnDataUpdated(T data)
        {
        }
    }
}