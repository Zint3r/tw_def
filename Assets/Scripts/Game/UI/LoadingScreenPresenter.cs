using Core.MessageHub;
using Core.UiService;
using Game.Application;
using Game.Features.Localization;
using Game.UI.Widgets.Misc;
using UnityEngine;
using Zenject;

namespace Game.UI.Loading
{
	public class LoadingScreenPresenter : UiPresenter
	{
		[SerializeField]
		private AssetLabelWidget NameText;

		[SerializeField]
		private AssetLabelWidget LoadingText;

		[SerializeField]
		private AssetLabelWidget VersionText;

		[SerializeField]
		private GameObject LoadingIndicator;

		[Inject]
		private IApplicationService applicationService;

		[Inject]
		private ILocalizationService localizationService;

		[Inject]
		private IMessageHubService messageHubService;

		protected override void OnLoaded()
		{
			FillText();
			messageHubService.Subscribe<LocalizationMessages.LanguageSwitchMessage>(RefreshText);
			LoadingIndicator.transform.rotation = Quaternion.identity;
		}

		public void FillText()
		{
			NameText.Init(localizationService.GetLocalizationText(1));
			LoadingText.Init(localizationService.GetLocalizationText(2));
			VersionText.Init(localizationService.GetLocalizationText(3) + " " + applicationService.GetAppVersion());			
		}

		public void RefreshText(LocalizationMessages.LanguageSwitchMessage message)
		{
			FillText();
		}

		protected override void OnUnloaded()
		{
			messageHubService.Unsubscribe<LocalizationMessages.LanguageSwitchMessage>(RefreshText);			
		}

		private void Update()
		{
			LoadingIndicator.transform.Rotate(Time.deltaTime * 50 * Vector3.back);
		}
	}
}