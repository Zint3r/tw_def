using Game.UI.Factories;
using UnityEngine;
using Zenject;

namespace Game.UI.Panels
{
	public class ConditionPanelView : MonoBehaviour
	{
		[SerializeField]
		private ConditionPanelItemView LanguageItemView;

		[Inject]
		private IconFactory iconFactory;

		public void SetLanguageItemData(string spriteName)
		{
			Sprite sprite = iconFactory.GetConditionSettingSprite(spriteName);
			LanguageItemView.SetData(sprite);
		}
	}
}