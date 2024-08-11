using Game.Features.Heroes;
using Game.UI.Widgets.Misc;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Panels
{
	public class MainControlPanelItemView : MonoBehaviour
	{
		[SerializeField]
		private AssetIconWidget _assetIconWidget;

		[SerializeField]
		private Button _activateBtn;

		public HeroRaceVO HeroRaceVO { get; set; }
		public HeroClassVO HeroClassVO { get; set; }
		public event Action<HeroRaceVO> OnRaceClick;
		public event Action<HeroClassVO> OnClassClick;


		public void SetRaceData(HeroRaceVO hero)
		{
			HeroRaceVO = hero;
			//_activateBtn.onClick.RemoveListener(OnSelectClass);
			_activateBtn.onClick.AddListener(OnSelectRace);
			_assetIconWidget.Init(hero.HeroRaceDefinition);
		}

		public void SetClassData(HeroClassVO heroClassVO)
		{			
			HeroClassVO = heroClassVO;
			//_activateBtn.onClick.RemoveListener(OnSelectRace);
			_activateBtn.onClick.AddListener(OnSelectClass);
			_assetIconWidget.Init(heroClassVO.HeroClassDefinition);
		}

		private void OnSelectRace()
		{
			OnRaceClick?.Invoke(HeroRaceVO);
		}

		private void OnSelectClass()
		{
			OnClassClick?.Invoke(HeroClassVO);
		}

		public void RemoveToPull()
		{
			HeroRaceVO = HeroRaceVO.Empty;
			HeroClassVO = HeroClassVO.Empty;
			_activateBtn.onClick.RemoveAllListeners();
		}
	}
}