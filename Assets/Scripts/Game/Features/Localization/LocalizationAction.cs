using Game.Features.Actions;
using Game.Features.GameDesign;
using Game.Utils;
using System;
using Zenject;

namespace Game.Features.Localization
{
	public class LocalizationAction : AbstractAction<LocalizationSwitchRequest, LocalizationParams>
	{
		[Inject]
		private ILocalizationService localizationService;

		protected override LocalizationSwitchRequest CreateNetworkRequest(LocalizationParams actionParams)
		{
			return new LocalizationSwitchRequest
			{

			};
		}
		protected override void UpdateModel(LocalizationParams actionParams, DateTime timeStamp)
		{
			LanguageEnum language = actionParams.language;
			DPL.LocalizationDataProvider.SetCurrentLanguage(language);
			//localizationService.set(existBuff);
			messageHubService.Publish(new LocalizationMessages.LanguageSwitchMessage(language));
		}
	}	
}