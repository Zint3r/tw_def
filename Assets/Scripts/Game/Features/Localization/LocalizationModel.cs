using Game.Features.GameDesign;
using System.Collections.Generic;

namespace Game.Features.Localization
{
	public class LocalizationModel : ILocalizationDataProvider
	{
		private Dictionary<int, LocalizationVO> LocalizationTexts = new Dictionary<int, LocalizationVO>();
		private LanguageEnum language = LanguageEnum.Ru;

		//public Dictionary<LanguageEnum, LocalizationVO> LocalizationTextss = new Dictionary<LanguageEnum, LocalizationVO>();
		/*public LocalizationVO GetLocalizationData(int definitionId)
		{
			if (LocalizationTexts.TryGetValue(definitionId, out LocalizationVO localizationVO))
			{
				return localizationVO;
			}

			return LocalizationVO.Empty;
		}*/

		public LocalizationVO GetLocalizationData(int definitionId)
		{
			if (LocalizationTexts.TryGetValue(definitionId, out LocalizationVO localizationVO))
			{
				return localizationVO;
			}

			return LocalizationVO.Empty;
		}

		public void SetLocalizationData(int id, LocalizationVO data)
		{
			LocalizationTexts.Add(id, data);
		}

		public LanguageEnum GetCurrentLanguage()
		{
			return language;
		}

		public void SetCurrentLanguage(LanguageEnum language)
		{
			this.language = language;
		}
	}
}