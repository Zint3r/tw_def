using Game.Features.MapCreator;
using Game.UI.Widgets.Misc;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Panels
{
	public class MapSelectPanelItemView : MonoBehaviour
	{
		[SerializeField]
		private AssetIconWidget _assetIconWidget;

		[SerializeField]
		private Button _activateBtn;

		private MapVO mapVO = MapVO.Empty;
		public event Action<MapVO> OnMapClick;


		public void SetMapData(MapVO map)
		{
			mapVO = map;
			_activateBtn.onClick.AddListener(OnSelectMap);
			_assetIconWidget.Init(map.MapDefinition);
		}

		private void OnSelectMap()
		{
			OnMapClick?.Invoke(mapVO);
		}

		public void RemoveToPull()
		{
			//mapVO = MapVO.Empty;
			_activateBtn.onClick.RemoveAllListeners();
		}
	}
}