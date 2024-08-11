using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Features.Level.Configuration
{
    public abstract class AbstractEntity : ScriptableObject
    {
        //Reference for editor tool
        public AssetReference Asset;
    }
}