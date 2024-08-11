using Core.UI;
using UnityEngine;

namespace Core
{
    public class MonoSingleton<T> : BaseView where T : BaseView
    {
        public bool IsOverrideOnAwake = true;

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var temp = FindFirstObjectByType<T>();

                    if (temp == null)
                    {
                        Debug.LogError($"There is no object of type '{nameof(T)}'. Instance cannot be accessed.");
                    }
                    else
                    {
                        _instance = temp;
                    }
                }

                return _instance;
            }
        }

        protected override void OnAwake()
        {
            if (_instance == null || IsOverrideOnAwake)
            {
                _instance = this as T;
            }
        }
    }
}