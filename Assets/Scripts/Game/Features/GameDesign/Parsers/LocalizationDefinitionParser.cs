using Game.Features.Localization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

namespace Game.Features.GameDesign.Parsers
{
	public class LocalizationDefinitionParser
	{        
        [Inject]
        private ILocalizationService localizationService;

		private const string FILE_PATCH = @"Assets\Resources\";		
		private const string LOCALIZATION_SHEET_NAME = "localization";

		public IEnumerator Initialize()
        {
			List<LocalizationVO> localizations = new List<LocalizationVO>{ LocalizationVO.Empty	};
			GameSettings gameSettings = new GameSettings();
			string jsonLoaded = string.Empty;
			if (File.Exists(FILE_PATCH + LOCALIZATION_SHEET_NAME + ".json"))
			{
				jsonLoaded = File.ReadAllText(FILE_PATCH + LOCALIZATION_SHEET_NAME + ".json");
			}
			if (string.IsNullOrEmpty(jsonLoaded) == false)
			{
				gameSettings.Localization = JsonUtility.FromJson<GameSettings>(jsonLoaded).Localization;
                
				foreach (LocalizationVO localization in gameSettings.Localization)
				{
					//LocalizationDefinition definition = gameDefinitionModel.GetOrCreate<LocalizationDefinition>(LocalizationConstants.Text);					
					localizations.Add(
						new LocalizationVO
						{
							Definition = localization.Definition,
							Id = localization.Id,
							En = localization.En,
							Ru = localization.Ru
						});
				}
			}
			localizationService.InitializeLocalizations(localizations);            
            yield return null;
        }
    }
}