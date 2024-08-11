using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Module.UI
{
    public class UiPool : MonoBehaviour
    {
        [Inject]
        private DiContainer diContainer;
        
        private readonly Dictionary<string, Stack<GameObject>> pools = new Dictionary<string, Stack<GameObject>>();
        
        public GameObject GetPooledPrefab(GameObject prefab)
        {
            return GetPooledObjectInternal(prefab.name, prefab);
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

            //UiPoolMember uiPoolMember = objectToReturn.GetComponentInChildren<UiPoolMember>(true);
            UiPoolMember uiPoolMember;
            objectToReturn.TryGetComponent<UiPoolMember>(out uiPoolMember);
			if (uiPoolMember != null)
            {
                uiPoolMember.ReturnToPool();
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

            UiPoolMember[] pooledObjects = container.GetComponents<UiPoolMember>();

            foreach (UiPoolMember poolMember in pooledObjects)
            {
                if (poolMember != null)
                {
					poolMember.ReturnToPool();
				}
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
                UiPoolMember poolMember = pooledObject.AddComponent<UiPoolMember>();
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