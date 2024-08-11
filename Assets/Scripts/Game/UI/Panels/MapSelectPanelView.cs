using Core.MessageHub;
using Game.Features.Actions;
using Game.Features.MapCreator;
using Module.DateTimeProvider;
using Module.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.UI.Panels
{
	public class MapSelectPanelView : MonoBehaviour
	{
		[SerializeField]
		private MapSelectPanelItemView _itemPrefab;

		[SerializeField]
		private Transform _parent;

		[Inject]
		private UiPool _pool;

		[Inject]
		private ActionLocator actionLocator;

		[Inject]
		private IDateTimeProvider dateTimeProvider;

		[Inject]
		private IMessageHubService messageHubService;

		private List<MapSelectPanelItemView> _items = new List<MapSelectPanelItemView>();
		private MapVO selectedMap = MapVO.Empty;

		public event Action<MapVO> OnMapSelect;

		public void SetMapsData(List<MapVO> maps)
		{
			Clean();
			for (int i = 0; i < maps.Count; i++)
			{
				MapSelectPanelItemView itemView = _pool.GetPooledPrefab(_itemPrefab, _parent);
				itemView.SetMapData(maps[i]);
				itemView.OnMapClick += OnSelectMap;
				_items.Add(itemView);
			}
		}

		private void OnSelectMap(MapVO mapVO)
		{
			selectedMap = mapVO;
			OnMapSelect?.Invoke(selectedMap);
			//messageHubService.Publish(new MapCreatorMessages.MapCreatorMessage(selectedMap));

			MapSelectParams args = new MapSelectParams(mapVO);
			if (!actionLocator.MapCreatorAction.CanExecute(args, dateTimeProvider.UtcNow, out string errMessage))
			{
				UnityEngine.Debug.LogWarning(errMessage);
				return;
			}
			actionLocator.MapCreatorAction.Execute(args);
		}

		private void Clean()
		{
			for (int i = _items.Count - 1; i >= 0; i--)
			{
				_items[i].OnMapClick -= OnSelectMap;
				_items[i].RemoveToPull();
				_pool.ReturnPooledObject(_items[i].gameObject);
				_items.RemoveAt(i);
			}
		}
	}
}