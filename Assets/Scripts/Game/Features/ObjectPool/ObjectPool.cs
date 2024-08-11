using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectPool : MonoBehaviour
{
	public AssetReference prefabReference;
	public int poolSize = 10;

	public List<GameObject> pool;
	private AsyncOperationHandle<GameObject> prefabHandle;
	public event Action OnLoad;

	private void Awake()
	{
		pool = new List<GameObject>(poolSize);

		Addressables.LoadAssetAsync<GameObject>(prefabReference).Completed += handle =>
		{
			prefabHandle = handle;

			for (int i = 0; i < poolSize; i++)
			{
				GameObject obj = Instantiate(prefabHandle.Result);
				obj.SetActive(false);
				pool.Add(obj);
			}
		};

		OnLoad?.Invoke();
	}

	private void OnDestroy()
	{
		Addressables.Release(prefabHandle);
	}

	public GameObject GetObject(Vector3 position, Quaternion rotation)
	{
		foreach (GameObject obj in pool)
		{
			if (!obj.activeInHierarchy)
			{
				obj.transform.position = position;
				obj.transform.rotation = rotation;
				obj.SetActive(true);
				return obj;
			}
		}

		/*Addressables.InstantiateAsync(prefabReference, position, rotation).Completed += handle =>
		{
			handle.Result.transform.SetParent(transform);
			pool.Add(handle.Result);
			
		};*/

		Addressables.LoadAssetAsync<GameObject>(prefabReference).Completed += handle =>
		{
			prefabHandle = handle;

			GameObject obj = Instantiate(prefabHandle.Result);			
			pool.Add(obj);
		};

		return prefabHandle.Result;
	}

	public void ReturnObject(GameObject obj)
	{
		obj.SetActive(false);
	}
}