using System;

namespace Game.Features.Localization
{
	[Serializable]
	public struct LocalizationVO
	{
		public string Definition;
		public int Id;
		public string En;
		public string Ru;

		public static LocalizationVO Empty =>
			new LocalizationVO
			{
				Definition = null,
				Id = 0,
				En = "Empty",
				Ru = "Пустой"
			};
	}	
}