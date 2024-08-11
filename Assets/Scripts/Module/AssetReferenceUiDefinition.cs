using Core.UiService;
using UnityEngine.AddressableAssets;

namespace Module.UI
{
    public class AssetReferenceUiDefinition : IUiDefinition
    {
        public AssetReference AssetReference;

        public string Name { get; }
        public OpenBehaviour OpenBehaviour { get; }
        
        public AssetReferenceUiDefinition(string name, OpenBehaviour openBehaviour = OpenBehaviour.Modal)
        {
            Name = name;
            OpenBehaviour = openBehaviour;
        }
    }
}