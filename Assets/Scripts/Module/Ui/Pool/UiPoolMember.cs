using UnityEngine;

namespace Module.UI
{
    public class UiPoolMember : MonoBehaviour
    {
        private UiPool uiPool;
        private string assetName;

        public void Init(UiPool uiPool, string assetName)
        {
            this.uiPool = uiPool;
            this.assetName = assetName;
        }

        public void ReturnToPool()
        {
            uiPool.ReturnPooledObject(assetName, gameObject);
        }
    }
}