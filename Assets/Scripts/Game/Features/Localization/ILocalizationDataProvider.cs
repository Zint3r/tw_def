using Game.Features.GameDesign;

namespace Game.Features.Localization
{
	public interface ILocalizationDataProvider
	{
		LanguageEnum GetCurrentLanguage();
		public void SetCurrentLanguage(LanguageEnum language);
	}
}

