using System.Collections;
using System.Collections.Generic;

namespace Core.Helper
{
    public class CoroutineHelper
    {
        public static IEnumerator RunInParallel(IEnumerable<IEnumerator> coroutines)
        {
            Stack<IEnumerator> stackedCoroutines = new Stack<IEnumerator>(coroutines);

            while (stackedCoroutines.Count > 0)
            {
                IEnumerator coroutine = stackedCoroutines.Pop();
                yield return coroutine;
            }
        }
    }
}