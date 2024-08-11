using System.Collections;
using UnityEngine;

namespace Core.CoroutineProvider
{
    public interface ICoroutineService
    {
        Coroutine StartCoroutine(IEnumerator routine);
        
        void StopCoroutine(Coroutine routine);
        void StopCoroutine(IEnumerator routine);
    }
}