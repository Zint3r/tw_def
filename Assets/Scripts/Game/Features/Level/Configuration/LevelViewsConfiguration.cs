using UnityEngine;

namespace Game.Features.Level.Configuration
{
    [CreateAssetMenu(fileName = "LevelViewsConfiguration", menuName = "Configurations/LevelViewsConfiguration")]
    public class LevelViewsConfiguration  : ScriptableObject
    {
        [SerializeField]
        private LevelViewConfiguration[] configurations;
        
        public LevelViewConfiguration GetViewConfiguration(string definitionId)
        {
            foreach (LevelViewConfiguration viewConfiguration in configurations)
            {
                if (viewConfiguration.LevelDefinitionId == definitionId)
                    return viewConfiguration;
            }

            return null;
        }
    }
}