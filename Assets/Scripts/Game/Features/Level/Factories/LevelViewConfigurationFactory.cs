using System.Collections;
using System.Collections.Generic;
using Core.Helper;
using Game.AssetCatalogue;
using Game.Features.Level.Configuration;
using Zenject;

namespace Game.Features.Level
{
    public class LevelViewConfigurationFactory
    {
        [Inject]
        private LevelViewSettingsCatalogue configurationsCatalogue;

        public LevelViewsConfiguration LevelViewsConfiguration { get; private set; }
		
        public IEnumerator Initialize()
        {
            yield return CoroutineHelper.RunInParallel(new List<IEnumerator>
            {
                AddressablesHelper.LoadAssetAsync<LevelViewsConfiguration>(configurationsCatalogue.LevelViewConfigurations,
                    op => LevelViewsConfiguration = op.Result)
            });
        }
    }
}