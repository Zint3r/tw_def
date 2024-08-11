using Core.MessageHub;
using Core.UiService;
using Game.Features.GameDesign;
using Game.Features.Resource;
using Game.UI.Widgets.Misc;
using Game.Utils;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.UI.Panels
{
	public class TopInterfacePanelPresenter : UiPresenter
    {
        //[SerializeField]
        //private ResourceIconWidget _experienceWidget;

        //[SerializeField]
        //private ResourceIconWidget _energyWidget;
        
        [SerializeField]
        private ResourceIconWidget _softCurrencyWidget;
        
        [SerializeField]
        private ResourceIconWidget _hardCurrencyWidget;
        
        [Inject]
        private IMessageHubService messageHubService;

        private Dictionary<string, ResourceIconWidget> widgets = new Dictionary<string, ResourceIconWidget>();
        private Dictionary<string, float> shownValues = new Dictionary<string, float>();
        
        protected override void OnLoaded()
        {
            //widgets.Add(ResourceConstants.Experience, _experienceWidget);
            //widgets.Add(ResourceConstants.Energy, _energyWidget);
            widgets.Add(ResourceConstants.SoftCurrency, _softCurrencyWidget);
            widgets.Add(ResourceConstants.HardCurrency, _hardCurrencyWidget);
            
            //shownValues.Add(ResourceConstants.Experience, 0);
            //shownValues.Add(ResourceConstants.Energy, 0);
            shownValues.Add(ResourceConstants.SoftCurrency, 0);
            shownValues.Add(ResourceConstants.HardCurrency, 0);
            
            messageHubService.Subscribe<ResourceMessages.ResourceUpdatedMessage>(OnResourceUpdated);

            Refresh();
        }

        public void Refresh()
        {
            foreach (KeyValuePair<string,ResourceIconWidget> pair in widgets)
            {
                pair.Value.Init(DPL.ResourceDataProvider.GetResource(pair.Key));
            }
        }
        
        private void OnResourceUpdated(ResourceMessages.ResourceUpdatedMessage message)
        {
            foreach (ResourceVO resourceVo in message.ChangedResources)
            {
                UpdateWidget(resourceVo);
            }
        }

        private void UpdateWidget(ResourceVO resourceVo)
        {
            shownValues[resourceVo.ResourceDefinition.DefinitionId] = resourceVo.Amount;
            widgets[resourceVo.ResourceDefinition.DefinitionId].UpdateLabel(shownValues[resourceVo.ResourceDefinition.DefinitionId]);
        }
        
        protected override void OnUnloaded()
        {
            messageHubService.Unsubscribe<ResourceMessages.ResourceUpdatedMessage>(OnResourceUpdated);
        }
    }
}