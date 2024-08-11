using System.Collections.Generic;
using Core.UiService;
using Module.UI;
using UnityEngine;

namespace Core.Map
{
    public static class MapLayers
    {
        //private static readonly RenderMode RenderMode = RenderMode.ScreenSpaceCamera;
        
        private static readonly List<UiLayerDefinition> CreatedLayers = new List<UiLayerDefinition>();
        public static IEnumerable<UiLayerDefinition> AllLayers => CreatedLayers;
        
        public static readonly UiLayerDefinition MapPresenters = CreateLayer("MapPresenters", 15);
        
        private static UiLayerDefinition CreateLayer(string name, int ordinal)
        {
            UiLayerDefinition layerDefinition = new UiLayerDefinition(name, ordinal);
            CreatedLayers.Add(layerDefinition);
            return layerDefinition;
        }
    }
    
    public static class MapViews
    {
        public static readonly AssetReferenceUiDefinition MapPresenter = new AssetReferenceUiDefinition("MapPresenter");
        
        public static void Init(MapPrefabsCatalogue prefabCatalogue)
        {
            MapPresenter.AssetReference = prefabCatalogue.MapPresenter;
        }
    }
}