using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Panels
{
	public class MenuPanelView : MonoBehaviour
	{
		public event Action OnActivateClick;

		[SerializeField]
		private Button _activateBtn;

		[SerializeField]
		private TextMeshProUGUI _nameLabel;

		private void Start()
		{
			_activateBtn.onClick.AddListener(OnActivateBtnClick);
		}

		public void SetData(string text)
		{			
			_nameLabel.text = text;			
		}

		private void OnActivateBtnClick()
		{
			OnActivateClick?.Invoke();
		}

		private void OnDestroy()
		{
			_activateBtn.onClick.RemoveListener(OnActivateBtnClick);
		}
	}
}