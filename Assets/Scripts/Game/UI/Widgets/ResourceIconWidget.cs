using Game.Features.Resource;
using TMPro;

namespace Game.UI.Widgets.Misc
{
	public class ResourceIconWidget : AssetIconWidget
    {
        public TextMeshProUGUI Label;
        
        public void Init(ResourceVO data)
        {
            Init(data.ResourceDefinition);
            UpdateLabel(data.Amount);
        }

        public virtual void UpdateLabel(float amount, string format = "{0}")
        {
            Label.text = string.Format(format, amount);
        }
    }
}