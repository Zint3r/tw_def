using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Module.GameObjectInstaller.Pool
{
	public class GoPool : MonoBehaviour
	{
		[Inject]
		private DiContainer diContainer;

		private readonly Dictionary<string, Stack<GameObject>> pools = new Dictionary<string, Stack<GameObject>>();

		public GameObject GetPooledPrefab(GameObject prefab)
		{
			return GetPooledObjectInternal(prefab.name, prefab);
		}

		public GameObject GetPooledPrefab(GameObject prefab, Vector3 position, Quaternion quaternion)
		{
			return GetPooledObjectInternal(prefab.name, prefab, position, quaternion);
		}

		public T GetPooledPrefab<T>(T prefab, Transform parent) where T : Component
		{
			var go = GetPooledObjectInternal(prefab.name, prefab.gameObject);
			go.transform.SetParent(parent, false);
			return go.GetComponent<T>();
		}

		public void ReturnPooledObject(GameObject objectToReturn)
		{
			if (objectToReturn == null)
			{
				return;
			}

			GoPoolMember goPoolMember = objectToReturn.GetComponentInChildren<GoPoolMember>(true);
			if (goPoolMember != null)
			{
				goPoolMember.ReturnToPool();
			}
		}

		public void ReturnPooledObject(string assetName, GameObject objectToReturn)
		{
			objectToReturn.SetActive(false);
			objectToReturn.transform.SetParent(transform, false);
			Stack<GameObject> stack = GetPool(assetName);
			stack.Push(objectToReturn);
		}

		public void ReturnAllPooledChildren(GameObject container)
		{
			if (container == null)
			{
				return;
			}

			GoPoolMember[] pooledObjects = container.GetComponentsInChildren<GoPoolMember>(true);
			foreach (GoPoolMember poolMember in pooledObjects)
			{
				poolMember.ReturnToPool();
			}
		}

		public void ClearPool()
		{
			foreach (var stack in pools.Values)
			{
				foreach (var pooledInstance in stack)
				{
					Destroy(pooledInstance);
				}
				stack.Clear();
			}
		}

		private Stack<GameObject> GetPool(string assetName)
		{
			Stack<GameObject> stack;
			pools.TryGetValue(assetName, out stack);

			if (stack == null)
			{
				stack = new Stack<GameObject>();
				pools.Add(assetName, stack);
			}

			return stack;
		}

		private GameObject GetPooledObjectInternal(string assetName, GameObject prefab)
		{
			Stack<GameObject> stack = GetPool(assetName);
			GameObject pooledObject;

			if (stack.Count == 0)
			{
				pooledObject = diContainer.InstantiatePrefab(prefab);
				GoPoolMember poolMember = pooledObject.AddComponent<GoPoolMember>();
				poolMember.Init(this, assetName);
			}
			else
			{
				pooledObject = stack.Pop();
			}

			pooledObject.SetActive(true);

			return pooledObject;
		}

		private GameObject GetPooledObjectInternal(string assetName, GameObject prefab, Vector3 position, Quaternion quaternion)
		{
			Stack<GameObject> stack = GetPool(assetName);
			GameObject pooledObject;

			if (stack.Count == 0)
			{
				pooledObject = diContainer.InstantiatePrefab(prefab, position, quaternion, null);
				GoPoolMember poolMember = pooledObject.AddComponent<GoPoolMember>();
				poolMember.Init(this, assetName);
			}
			else
			{
				pooledObject = stack.Pop();
			}

			pooledObject.SetActive(true);

			return pooledObject;
		}
	}
}