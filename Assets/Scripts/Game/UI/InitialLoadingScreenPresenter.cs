using System;
using UnityEngine;

namespace Game.UI.Loading
{
    public class InitialLoadingScreenPresenter : MonoBehaviour
    {
        public GameObject Content;

        private void Awake()
        {
            Content.SetActive(true);
        }

        public void Destroy()
        {
            try
            {
                Destroy(gameObject);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}