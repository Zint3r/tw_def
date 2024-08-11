using Game.Features.Level.Configuration;
using UnityEngine;

namespace Game.Features.Level
{
    public class LevelScenePositions : MonoBehaviour
    {
        [SerializeField]
        private Transform levelContainer;

        [SerializeField]
        private Transform levelBackgroundContainer;

        //Default level config
        [SerializeField]
        private LevelViewConfiguration initialLevelViewConfiguration;

        public Transform LevelContainer => levelContainer;

        public Transform LevelBackgroundContainer => levelBackgroundContainer;
		
        public LevelViewConfiguration InitialLevelViewConfiguration => initialLevelViewConfiguration;
    }
}