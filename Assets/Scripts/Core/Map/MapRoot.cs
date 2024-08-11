using UnityEngine;

namespace Core.Map
{
    public class MapRoot : MonoBehaviour
    {
        private Camera mapCamera;

        public Camera MapCamera
        {
            get
            {
                if (mapCamera == null)
                {
                    mapCamera = GetComponent<Camera>();
                }

                return mapCamera;
            }
        }
    }
}