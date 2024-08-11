using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Panels
{
	public class MenuPanelItemView : MonoBehaviour
	{
		public event Action OnActivateClick;

		[SerializeField]
		private Button _enBtn;

		[SerializeField]
		private Button _ruBtn;

		[SerializeField]
		private Button _backBtn;

		[SerializeField]
		private TextMeshProUGUI _enLabel;

		[SerializeField]
		private TextMeshProUGUI _ruLabel;

		[SerializeField]
		private TextMeshProUGUI _backLabel;

		private void Start()
		{
			_enBtn.onClick.AddListener(OnActivateBtnClick);
			_ruBtn.onClick.AddListener(OnActivateBtnClick);
		}

		public void SetData(string text)
		{
			_enLabel.text = text;			
		}

		private void SetLabel(TextMeshProUGUI label, string text)
		{
			label.text = text;
		}

		private void OnActivateBtnClick()
		{
			OnActivateClick?.Invoke();
		}

		private void OnDestroy()
		{
			_enBtn.onClick.RemoveListener(OnActivateBtnClick);
			_ruBtn.onClick.RemoveListener(OnActivateBtnClick);
		}
	}
}