using Game.Features.GameDesign;
using System.Collections.Generic;

namespace Game.Features.Localization
{
	public interface ILocalizationService
	{
		void InitializeLocalizations(List<LocalizationVO> localizations);
		string GetLocalizationText(int id);
		void SwitchToEnglishLanguage();
		void SwitchToRussianLanguage();
	}
}