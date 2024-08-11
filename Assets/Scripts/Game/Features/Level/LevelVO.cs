using Game.Features.GameDesign.DefinitionObjects.Level;
using System.Collections.Generic;
using System.Linq;

namespace Game.Features.Level
{
	public class LevelVO
    {
        public LevelDefinition Definition;
        public List<LevelSegmentVO> Segments = new List<LevelSegmentVO>();
        
        public LevelSegmentVO GetSegmentByDefinitionId(LevelSegmentDefinition definition)
        {
            return GetSegmentByDefinitionId(definition.DefinitionId);
        }
        
        public LevelSegmentVO GetSegmentByDefinitionId(string definitionId)
        {
            return Segments.FirstOrDefault(x => x.Definition.DefinitionId == definitionId);
        }
    }
}