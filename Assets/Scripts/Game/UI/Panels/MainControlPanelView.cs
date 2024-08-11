using Core.MessageHub;
using Game.Features.HeroBuilding;
using Game.Features.Heroes;
using Game.UI.Factories;
using Game.Utils;
using Module.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.UI.Panels
{
	public class MainControlPanelView : MonoBehaviour
	{
		[SerializeField]
		private MainControlPanelItemView _itemPrefab;

		[SerializeField]
		private Transform _parent;

		[Inject]
		private UiPool _pool;

		[Inject]
		private GameObjectFactory _gameObjectFactory;

		[Inject]
		private IMessageHubService messageHubService;

		private List<MainControlPanelItemView> _items = new List<MainControlPanelItemView>();		
		public event Action OnSelect;

		public void SetRacesData(List<HeroRaceVO> races)
		{
			Clean();
			for (int i = 0; i < races.Count; i++)
			{
				MainControlPanelItemView itemView = _pool.GetPooledPrefab(_itemPrefab, _parent);
				itemView.SetRaceData(races[i]);
				itemView.OnRaceClick += OnActivateRaceMenu;
				_items.Add(itemView);
			}
		}

		public void SetClassesData(List<HeroClassVO> classes)
		{
			Clean();
			for (int i = 0; i < classes.Count; i++)
			{
				MainControlPanelItemView itemView = _pool.GetPooledPrefab(_itemPrefab, _parent);
				itemView.SetClassData(classes[i]);
				itemView.OnClassClick += CreateHeroGO;
				_items.Add(itemView);
			}
			OnSelect?.Invoke();
		}

		private void CreateHeroGO(HeroClassVO heroClass)
		{
			messageHubService.Publish(new HeroBuildingMessages.HeroTryBuildingMessage(heroClass));
			//GameObject heroGO = _gameObjectFactory.GetGameObjectByDefinition(heroClass.HeroClassDefinition);
			//Instantiate(heroGO, new Vector3(), Quaternion.identity);
		}

		//public void UpdateHero(HeroVO data)
		//{
		//	var itemView = _items.FirstOrDefault(x => x.DefinitionId == data.HeroDefinition.DefinitionId);
		//	if (itemView == null) {	return;	}		
		//	itemView.SetData(data);
		//}

		private void OnActivateRaceMenu(HeroRaceVO definitionId)
		{
			//OnActivateClick?.Invoke(definitionId);
			SetClassesData(DPL.HeroCollectionDataProvider.GetClassesByRace(definitionId));			
		}

		private void OnActivateClassMenu(HeroClassVO definitionId)
		{
			//OnActivateClick?.Invoke(definitionId);
			//SetData(DPL.HeroCollectionDataProvider.GetClassesByRace(definitionId));
		}

		private void Clean()
		{
			for (int i = _items.Count - 1; i >= 0; i--)
			{
				_items[i].OnRaceClick -= OnActivateRaceMenu;
				_items[i].RemoveToPull();
				//Destroy(_items[i].gameObject);
				_pool.ReturnPooledObject(_items[i].gameObject);
				_items.RemoveAt(i);
			}
		}
		private void OnDisable()
		{
			for (int i = _items.Count - 1; i >= 0; i--)
			{
				_items[i].OnRaceClick -= OnActivateRaceMenu;
			}
		}
	}
}