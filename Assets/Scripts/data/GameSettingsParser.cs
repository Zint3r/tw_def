using System;
using System.Collections.Generic;

public class GameSettingsParser : IGoogleSheetParser
{
	private readonly GameSettings _settings;
	private EnemyTestVO _enemySettings;

	public GameSettingsParser(GameSettings settings)
	{
		_settings = settings;
		_settings.Enemy = new List<EnemyTestVO>();
	}
	public void Parse(string header, string token)
	{
		switch (header)
		{
			case "id":
				_enemySettings = new EnemyTestVO { id = Convert.ToInt32(token) };
				break;
			case "name":
				_enemySettings.name = token;
				break;
			case "hp":
				_enemySettings.hp = Convert.ToInt32(token);
				_settings.Enemy.Add(_enemySettings);
				break;
			default:
				throw new Exception($"Invalid header: {header}");
		}		
	}
}