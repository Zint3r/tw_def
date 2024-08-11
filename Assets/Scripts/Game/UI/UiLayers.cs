using Core.UiService;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
	public static class UiLayers
	{
		private static readonly RenderMode RenderMode = RenderMode.ScreenSpaceCamera;

		private static readonly List<UiLayerDefinition> CreatedLayers = new List<UiLayerDefinition>();
		public static IEnumerable<UiLayerDefinition> AllLayers => CreatedLayers;


		public static readonly UiLayerDefinition Panels = CreateLayer("Panels", 15);

		public static readonly UiLayerDefinition Popups = CreateLayer("Popups", 13);

		public static readonly UiLayerDefinition InitialLoading = CreateLayer("InitialLoading", 2);

		private static UiLayerDefinition CreateLayer(string name, int ordinal)
		{
			UiLayerDefinition layerDefinition = new UiLayerDefinition(name, ordinal);
			CreatedLayers.Add(layerDefinition);
			return layerDefinition;
		}
	}
}