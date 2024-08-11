using System;
using System.Collections;
using UnityEngine;

namespace Core.CoroutineProvider
{
    public class CoroutineService : MonoBehaviour, ICoroutineService
    {
        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            var coroutine = base.StartCoroutine(routine);
            return coroutine;
        }

        public new Coroutine StartCoroutine(string methodName)
        {
            throw new NotSupportedException(
                "This method overload is not supported by the system. Please call StartCoroutine(IEnumerator routine);");
        }
        
        public new void StopCoroutine(IEnumerator routine)
        {
            if (WasDisposed())
            {
                return;
            }

            base.StopCoroutine(routine);
        }
        
        public new void StopCoroutine(Coroutine routine)
        {
            if (WasDisposed())
            {
                return;
            }

            base.StopCoroutine(routine);
        }
        
        public void Dispose()
        {
            if (WasDisposed())
            {
                return;
            }

            StopAllCoroutines();

            if (Application.isPlaying)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private bool WasDisposed()
        {
            return this == null;
        }
    }
}