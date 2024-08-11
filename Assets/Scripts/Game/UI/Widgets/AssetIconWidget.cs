using Game.Features.GameDesign;
using Game.UI.Factories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Widgets.Misc
{
    public class AssetIconWidget : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private Sprite defaultSprite;
        
        [Inject]
        private IconFactory iconFactory;
        
        public void Init(GameDefinition definition)
        {
            Sprite sprite = iconFactory.GetDefinitionSprite(definition);
            SetIconSprite(definition, sprite);
        }
        
        private void SetIconSprite(GameDefinition definition, Sprite sprite)
        {
            if (sprite == null)
            {
                icon.sprite = defaultSprite;
                return;
            }
            
            icon.sprite = sprite;
        }
    }
}