using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.Level.Configuration
{
    public enum EntitySegmentId
    {
        A1,
        A2,
        A3,
        A4,
        A5,
        A6,
        A7,
        A8,
        A9
    }
    
    [Serializable]
    public class EntityEntry
    {
        public EntitySegmentId SegmentId;
        public AbstractEntity Entity;
        public Vector3 Position;
    }
    
    [CreateAssetMenu(fileName = nameof(LevelViewConfiguration), menuName = "Level/" + nameof(LevelViewConfiguration))]
    public class LevelViewConfiguration : ScriptableObject
    {
        [Tooltip("This MUST match the Level Id given in the GameDesign data.")]
        public string LevelDefinitionId;
        public List<EntityEntry> Entities;
        
        public EntityEntry GetEntityEntry(EntitySegmentId segmentId)
        {
            foreach (EntityEntry entityEntry in Entities)
            {
                if (entityEntry.SegmentId == segmentId)
                {
                    return entityEntry;
                }
            }

            return null;
        }
    }
}