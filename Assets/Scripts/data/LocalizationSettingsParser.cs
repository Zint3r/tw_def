using Game.Features.Localization;
using System;
using System.Collections.Generic;

public class LocalizationSettingsParser : IGoogleSheetParser
{
	private readonly GameSettings _settings;
	private LocalizationVO _localizationVO;
	public LocalizationSettingsParser(GameSettings settings)
	{
		_settings = settings;
		_settings.Localization = new List<LocalizationVO>();
	}
	public void Parse(string header, string token)
	{
		switch (header)
		{
			case "id":
				_localizationVO.Id = Convert.ToInt32(token);
				break;
			case "en":
				_localizationVO.En = token;
				break;
			case "ru":
				_localizationVO.Ru = token;
				_settings.Localization.Add(_localizationVO);
				break;
			default:
				throw new Exception($"Invalid header: {header}");
		}		
	}	
}