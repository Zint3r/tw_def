using Game.Features.Actions;
using Game.Features.GameDesign;

namespace Game.Features.Localization
{
	public class LocalizationParams : IActionParams
	{
		public LanguageEnum language;
		public LocalizationParams(LanguageEnum language)
		{
			this.language = language;
		}
	}
}