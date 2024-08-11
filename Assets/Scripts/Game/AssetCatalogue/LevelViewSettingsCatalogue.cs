using System;
using System.Collections.Generic;
using System.Linq;
using Game.Features.Level.Configuration;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.AssetCatalogue
{
    [Serializable]
    public class LevelSegmentBackgroundPrefabReference
    {
        public EntitySegmentId SegmentId;
        public AssetReference Prefab;
    }
    
    [Serializable]
    public class LevelBackgroundPrefabReference
    {
        public string LevelDefinitionId;
        public List<LevelSegmentBackgroundPrefabReference> Segments;
    }
    
    [CreateAssetMenu(fileName = "NewLevelViewSettingsCatalogue", menuName = "AssetCatalogue/LevelViewSettingsCatalogue", order = 0)]
    public class LevelViewSettingsCatalogue : ScriptableObject
    {
        public AssetReference LevelViewConfigurations;
        
        public List<LevelBackgroundPrefabReference> PrefabBackgroundReferences;
        
        public AssetReference GetBackgroundPrefabReference(string levelId, EntitySegmentId segmentId)
        {
            return PrefabBackgroundReferences.
                FirstOrDefault(x => x.LevelDefinitionId == levelId)?.Segments.
                FirstOrDefault(x => x.SegmentId == segmentId)?.Prefab;
        }
    }
}