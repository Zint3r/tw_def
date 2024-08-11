using Zenject;

namespace Game.Features.GameDesign
{
    public class AbstractDefinitionParser
    {
        [Inject]
        protected GameDefinitionParserService parser;
    }
}