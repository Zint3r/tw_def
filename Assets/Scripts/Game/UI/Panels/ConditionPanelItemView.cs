using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Panels
{
	public class ConditionPanelItemView : MonoBehaviour
	{
		[SerializeField]
		private Image sprite;

		public void SetData(Sprite sprite)
		{
			this.sprite.sprite = sprite;
		}
	}
}