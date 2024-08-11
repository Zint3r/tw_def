using Core.MessageHub;
using Game.Features.GameDesign;
using Game.Features.Resource;
using System.Collections.Generic;
using Zenject;

namespace Game.Features.Localization
{
	public class LocalizationService : ILocalizationService
	{
		[Inject]
		private LocalizationModel localizationModel;

		[Inject]
		private IMessageHubService messageHubService;

		/*public string GetLocalizationText(int id)
		{
			LocalizationVO localizationVO = localizationModel.GetLocalizationData(id);
			string text;
			switch (localizationModel.language)
			{
				case 0:
					text = localizationVO.En;
					break;
				case 1:
					text = localizationVO.Ru;
					break;
				default:
					text = localizationVO.En;
					break;
			}
			return text;
		}*/
		public void SwitchToEnglishLanguage()
		{
			LanguageEnum language = LanguageEnum.En;
			localizationModel.SetCurrentLanguage(language);
			messageHubService.Publish(new LocalizationMessages.LanguageSwitchMessage(language));
		}
		public void SwitchToRussianLanguage()
		{
			LanguageEnum language = LanguageEnum.Ru;
			localizationModel.SetCurrentLanguage(language);
			messageHubService.Publish(new LocalizationMessages.LanguageSwitchMessage(language));
		}
		public string GetLocalizationText(int id)
		{
			LanguageEnum language = localizationModel.GetCurrentLanguage();
			LocalizationVO localizationVO = localizationModel.GetLocalizationData(id);
			string text;
			switch (language)
			{
				case LanguageEnum.En:
					text = localizationVO.En;
					break;
				case LanguageEnum.Ru:
					text = localizationVO.Ru;
					break;
				default:
					text = localizationVO.En;
					break;
			}
			return text;
		}

		public void InitializeLocalizations(List<LocalizationVO> localizations)
		{
			foreach (LocalizationVO localization in localizations)
			{
				localizationModel.SetLocalizationData(localization.Id, localization);
			}
		}
	}
}