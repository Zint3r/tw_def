using Game.Features.Localization;
using System;
using System.Collections.Generic;

[Serializable]
public class GameSettings
{
    public List<EnemyTestVO> Enemy;
	public List<LocalizationVO> Localization;
}