using System.Collections.Generic;
using System.Linq;

namespace Game.Features.Level
{
    public interface ILevelDataProvider
    {
        LevelVO GetLevel(string definitionId);
    }
    
    public class LevelModel : ILevelDataProvider
    {
        public List<LevelVO> Levels = new List<LevelVO>();

        public LevelVO GetLevel(string definitionId)
        {
            return Levels.FirstOrDefault(x => x.Definition.DefinitionId == definitionId);
        }
    }
}