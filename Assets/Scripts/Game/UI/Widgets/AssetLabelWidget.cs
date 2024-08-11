using TMPro;
using UnityEngine;

namespace Game.UI.Widgets.Misc
{
	public class AssetLabelWidget : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI label;
		public void Init(string text)
		{
			SetLabelText(text);
		}
		private void SetLabelText(string text)
		{
			label.text = text;
		}
	}
}