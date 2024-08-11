using System.Collections;
using System.Collections.Generic;
using Core.CoroutineProvider;
using Core.Helper;
using Core.Map;
using Core.UiService;
using Game.Features.GameDesign;
using Game.Features.Level.Factories;
using Game.Utils;
using UnityEngine;
using Zenject;

namespace Game.Features.Level.Presenters
{
    public class LevelPresenter : MapPresenter
    {
        [Inject]
        private ICoroutineService coroutineService;
        
        [Inject]
        private LevelEnvironmentFactory levelEnvironmentFactory;
        
        [SerializeField]
        private LevelScenePositions levelScenePositions;

        protected override void OnOpening()
        {
            LevelVO level = DPL.LevelDataProvider.GetLevel(LevelConstants.AbbysLevel);

            coroutineService.StartCoroutine(LoadLevel(level));
        }

        private IEnumerator LoadLevel(LevelVO level)
        {
            for (int i = 0; i < level.Segments.Count; i++)
            {
                yield return CoroutineHelper.RunInParallel(new List<IEnumerator>
                {
                    levelEnvironmentFactory.CreateLevelSegmentBackground(level.Definition.DefinitionId, level.Segments[i].SegmentId, OnBackgroundLoaded)
                });
            }
        }

        private void OnBackgroundLoaded(GameObject background)
        {
            background.transform.SetParent(levelScenePositions.LevelBackgroundContainer, false);
            
            // messageHubService.Publish(new LevelMessages.LevelBackgroundLoadedMessage());
        }
    }
}