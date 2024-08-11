using System.Collections;
using System.Collections.Generic;
using Core.Helper;
using Game.AssetCatalogue;
using Game.UI.Configuration;
using Zenject;

namespace Game.UI.Factories
{
    public class UiConfigurationFactory
    {
        [Inject]
        private UiConfigurationsCatalogue uiConfigurationsCatalogue;
        
        public RarityColorConfiguration ManagerRarityColorConfiguration { get; private set; }

        public UiElementColorConfiguration UiElementColorConfiguration { get; private set; }
        
        public IEnumerator Initialize()
        {
            yield return CoroutineHelper.RunInParallel(new List<IEnumerator>
            {
                AddressablesHelper.LoadAssetAsync<RarityColorConfiguration>(uiConfigurationsCatalogue.ManagerRarityColorConfiguration,
                    op => ManagerRarityColorConfiguration = op.Result),
                AddressablesHelper.LoadAssetAsync<UiElementColorConfiguration>(uiConfigurationsCatalogue.UiElementColorConfiguration, op => UiElementColorConfiguration = op.Result)
            });
        }
    }
}