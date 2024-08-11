using UnityEngine;

namespace Core.Helper
{
    public static class TransformExtensions
    {
        public static void RemoveAllChildren(this Transform transform, bool isDestroyingImmediate = false)
        {
            int childCount = transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject gameObject = transform.GetChild(i).gameObject;
                if (isDestroyingImmediate)
                {
                    Object.DestroyImmediate(gameObject, false);
                }
                else
                {
                    Object.Destroy(gameObject);
                }
            }
        }
    }
}