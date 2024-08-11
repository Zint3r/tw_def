using System;
using UnityEngine;

namespace Core.UiService
{
    public class UiRoot : MonoBehaviour
    {
        private Camera _uiCamera;

        public Camera UiCamera
        {
            get
            {
                if (_uiCamera == null)
                {
                    _uiCamera = GetComponent<Camera>();
                }

                return _uiCamera;
            }
        }
    }
}