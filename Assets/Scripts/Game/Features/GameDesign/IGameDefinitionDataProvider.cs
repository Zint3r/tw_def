namespace Game.Features.GameDesign
{
    public interface IGameDefinitionDataProvider
    {
        TDefinition Get<TDefinition>(string definitionId) where TDefinition : GameDefinition;
    }
}