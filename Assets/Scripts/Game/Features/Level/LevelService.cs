using System.Collections.Generic;
using Zenject;

namespace Game.Features.Level
{
    public interface ILevelService
    {
        void InitializeLevels(List<LevelVO> levels);
    }
    
    public class LevelService : ILevelService
    {
        [Inject]
        private LevelModel levelModel;
        
        public void InitializeLevels(List<LevelVO> levels)
        {
            levelModel.Levels.AddRange(levels);
        }
    }
}