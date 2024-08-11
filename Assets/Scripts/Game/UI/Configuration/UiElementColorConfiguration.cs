using UnityEngine;

namespace Game.UI.Configuration
{
    [CreateAssetMenu(fileName = nameof(UiElementColorConfiguration), menuName = "Configurations/" + nameof(UiElementColorConfiguration))]
    public class UiElementColorConfiguration : ScriptableObject
    {
        [Tooltip("Default text color.")]
        public ColorConfiguration DefaultText;
    }
}