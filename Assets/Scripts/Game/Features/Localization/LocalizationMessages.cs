using Core;
using Game.Features.GameDesign;

namespace Game.Features.Localization
{
	public static class LocalizationMessages
	{
		public struct LanguageSwitchMessage : IMessage
		{
			public readonly LanguageEnum language;

			public LanguageSwitchMessage(LanguageEnum language)
			{
				this.language = language;
			}
		}
	}
}