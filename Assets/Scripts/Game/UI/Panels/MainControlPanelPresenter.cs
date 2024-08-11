using Core.UiService;
using Game.Features.Resource;
using Game.UI.Widgets.Misc;
using Game.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Panels
{
	public class MainControlPanelPresenter : UiPresenter
	{
		[SerializeField]
		private AssetLabelWidget _nameText;

		[SerializeField]
		private AssetLabelWidget _descText;

		[SerializeField]
		private AssetIconWidget _iconImg;

		[SerializeField]
		private MainControlPanelView _buttonsPanelView;

		[SerializeField]
		private Button _closeButton;

		protected override void OnLoaded()
		{			
			_closeButton.onClick.AddListener(Refresh);
			Refresh();
			_buttonsPanelView.OnSelect += ActivateCloseButton;
		}

		protected override void OnUnloaded()
		{
			_closeButton.onClick.RemoveListener(Refresh);
			_buttonsPanelView.OnSelect -= ActivateCloseButton;
		}

		public void ActivateCloseButton()
		{
			_closeButton.gameObject.SetActive(true);
		}

		public void Refresh()
		{
			_buttonsPanelView.SetRacesData(DPL.HeroCollectionDataProvider.GetHeroRaces());
			_closeButton.gameObject.SetActive(false);
		}

		private void OnResourceUpdated(ResourceMessages.ResourceUpdatedMessage message)
		{
			foreach (ResourceVO resourceVo in message.ChangedResources)
			{
				UpdateWidget(resourceVo);
			}
		}

		private void UpdateWidget(ResourceVO resourceVo)
		{
			//shownValues[resourceVo.ResourceDefinition.DefinitionId] = resourceVo.Amount;
			//widgets[resourceVo.ResourceDefinition.DefinitionId].UpdateLabel(shownValues[resourceVo.ResourceDefinition.DefinitionId]);
		}
	}
}