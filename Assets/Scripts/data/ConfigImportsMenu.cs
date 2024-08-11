using Game.Features.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigImportsMenu
{
	private const string CREDENTIALS_PATH = "disco-amphora-422609-j1-89dadae6e2ed.json";
	private const string SPREADSHEET_ID = "15E3SB38yV02fCw8K3HQYdzELI6WtsaIptZI2afLvvcU";
	private const string FILE_PATCH = @"Assets\resources\";
	private const string ENEMY_SHEET_NAME = "enemy_list";
	//private const string ITEM_SHEET_NAME = "item_list";
	//private const string SKILL_SHEET_NAME = "skill_list";
	//private const string HERO_SHEET_NAME = "hero_list";
	private const string LOCALIZATION_SHEET_NAME = "localization";	

	//[UnityEditor.MenuItem("GoogleSheet/Import Enemy Settings")]
	private static async void LoadEnemySettings()
	{
		GoogleSheetsUnity sheetsImporter = new GoogleSheetsUnity(CREDENTIALS_PATH, SPREADSHEET_ID);
		GameSettings gameSettings = LoadSettings(ENEMY_SHEET_NAME);
		GameSettingsParser enemyParser = new GameSettingsParser(gameSettings);
		await sheetsImporter.DownloadAndParseSheet(ENEMY_SHEET_NAME, enemyParser);
		string jsonForSaving = JsonUtility.ToJson(gameSettings);
		CreateSettingsFile(ENEMY_SHEET_NAME, jsonForSaving);
	}
	//[MenuItem("GoogleSheet/Import Localization Settings")]
	private static async void LoadLocalizationSettings()
	{
		GoogleSheetsUnity sheetsImporter = new GoogleSheetsUnity(CREDENTIALS_PATH, SPREADSHEET_ID);
		GameSettings gameSettings = LoadSettings(LOCALIZATION_SHEET_NAME);
		LocalizationSettingsParser localizationParser = new LocalizationSettingsParser(gameSettings);
		await sheetsImporter.DownloadAndParseSheet(LOCALIZATION_SHEET_NAME, localizationParser);
		string jsonForSaving = JsonUtility.ToJson(gameSettings);
		CreateSettingsFile(LOCALIZATION_SHEET_NAME, jsonForSaving);
	}
	private static void CreateSettingsFile(string sheetName, string jsonForSaving)
	{
		StreamWriter sw = File.CreateText(FILE_PATCH + sheetName + ".json");
		sw.Close();
		File.WriteAllText(FILE_PATCH + sheetName + ".json", jsonForSaving);
		Debug.Log(jsonForSaving);
	}
	private static GameSettings LoadSettings(string sheetName)
	{
		string jsonLoaded = LoadJson(sheetName);
		GameSettings gameSettings = new GameSettings();
		if (string.IsNullOrEmpty(jsonLoaded) == false)
		{
			switch(sheetName)
			{
				case "enemy_list":
					gameSettings.Enemy = JsonUtility.FromJson<GameSettings>(jsonLoaded).Enemy;
					break;
				case "localization":
					gameSettings.Localization = JsonUtility.FromJson<GameSettings>(jsonLoaded).Localization;
					Debug.Log(gameSettings.Localization.Count);
					break;
				default:
					throw new Exception($"Empty sheet name!");
			}					
		}
		else
		{
			switch (sheetName)
			{
				case "enemy_list":
					gameSettings.Enemy = new List<EnemyTestVO>();
					break;
				case "localization":
					gameSettings.Localization = new List<LocalizationVO>();
					break;
				default:
					throw new Exception($"Empty sheet name!");
			}
		}
		return gameSettings;
	}
	private static string LoadJson(string jsonName)
	{
		string jsonLoaded = string.Empty;
		if (File.Exists(FILE_PATCH + jsonName + ".json"))
		{
			jsonLoaded = File.ReadAllText(FILE_PATCH + jsonName + ".json");
		}
		if (string.IsNullOrEmpty(jsonLoaded) == false)
		{
			return jsonLoaded;
		}
		else
		{
			Debug.LogError(message: $"Error load: {jsonName}");
			return string.Empty;
		}
	}
}