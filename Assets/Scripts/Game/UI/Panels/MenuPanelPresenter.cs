using Core.MessageHub;
using Core.UiService;
using Game.Features.GameDesign;
using Game.Features.Localization;
using Game.UI.Widgets.Misc;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Panels
{
	public class MenuPanelPresenter : UiPresenter
	{
		[SerializeField]
		private ConditionPanelView conditionPanelView;

		[SerializeField]
		private Button EnBtn;

		[SerializeField]
		private Button RuBtn;

		//[SerializeField]
		//private Button BackBtn;

		[SerializeField]
		private AssetLabelWidget settingsText;

		[SerializeField]
		private AssetLabelWidget localizationText;

		[SerializeField]
		private AssetLabelWidget enBtnText;

		[SerializeField]
		private AssetLabelWidget ruBtnText;

		[SerializeField]
		private AssetLabelWidget backBtnText;

		[Inject]
		private ILocalizationService localizationService;

		[Inject]
		private LocalizationModel localizationModel;

		[Inject]
		private IMessageHubService messageHubService;

		protected override void OnLoaded()
		{
			SetLanguageViewData();
			FillText();			
			EnBtn.onClick.AddListener(SwitchToEnglishLanguage);
			RuBtn.onClick.AddListener(SwitchToRussianLanguage);
			messageHubService.Subscribe<LocalizationMessages.LanguageSwitchMessage>(RefreshText);
		}

		protected override void OnUnloaded()
		{
			EnBtn.onClick.RemoveListener(SwitchToEnglishLanguage);
			RuBtn.onClick.RemoveListener(SwitchToRussianLanguage);
			messageHubService.Unsubscribe<LocalizationMessages.LanguageSwitchMessage>(RefreshText);
		}

		private void SwitchToEnglishLanguage()
		{
			localizationService.SwitchToEnglishLanguage();
			conditionPanelView.SetLanguageItemData("en_lng");
		}

		private void SwitchToRussianLanguage()
		{
			localizationService.SwitchToRussianLanguage();
			conditionPanelView.SetLanguageItemData("ru_lng");
		}
		private void SetLanguageViewData()
		{
			LanguageEnum language = localizationModel.GetCurrentLanguage();
			switch (language)
			{
				case LanguageEnum.En:					
					conditionPanelView.SetLanguageItemData("en_lng");					
					break;
				case LanguageEnum.Ru:					
					conditionPanelView.SetLanguageItemData("ru_lng");
					break;
				default:					
					conditionPanelView.SetLanguageItemData("en_lng");
					break;
			}
			FillText();
		}

		public void FillText()
		{
			settingsText.Init(localizationService.GetLocalizationText(4));
			localizationText.Init(localizationService.GetLocalizationText(5));
			enBtnText.Init(localizationService.GetLocalizationText(6));
			ruBtnText.Init(localizationService.GetLocalizationText(7));
			backBtnText.Init(localizationService.GetLocalizationText(8));
		}

		public void RefreshText(LocalizationMessages.LanguageSwitchMessage message)
		{
			FillText();
		}
	}
}