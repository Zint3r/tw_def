using UnityEngine;

namespace Module.GameObjectInstaller.Pool
{
	public class GoPoolMember : MonoBehaviour
	{
		private GoPool goPool;
		private string assetName;

		public void Init(GoPool goPool, string assetName)
		{
			this.goPool = goPool;
			this.assetName = assetName;
		}

		public void ReturnToPool()
		{
			goPool.ReturnPooledObject(assetName, gameObject);
		}
	}
}