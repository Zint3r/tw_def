using System;
using Game.Features.GameDesign;
using UnityEngine;

namespace Game.UI.Configuration
{
    [Serializable]
    public class RarityColorMapping
    {
        public RarityEnum Rarity;
        public Color Color;
    }
    
    [CreateAssetMenu(fileName = nameof(RarityColorConfiguration), menuName = "Configurations/" + nameof(RarityColorConfiguration))]
    public class RarityColorConfiguration : ScriptableObject
    {
        [SerializeField]
        private RarityColorMapping[] mappings;

        public Color GetRarityColor(RarityEnum rarity)
        {
            if (mappings != null)
            {
                foreach (RarityColorMapping colorMapping in mappings)
                {
                    if (colorMapping.Rarity == rarity)
                        return colorMapping.Color;
                }
            }
            return Color.white;
        }
    }
}