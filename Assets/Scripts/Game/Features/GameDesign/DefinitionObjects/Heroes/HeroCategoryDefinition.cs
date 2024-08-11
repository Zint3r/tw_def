namespace Game.Features.GameDesign
{
    public class HeroCategoryDefinition : GameDefinition
    {
        public static HeroCategoryDefinition Empty
        {
            get
            {
                return new HeroCategoryDefinition
                {
                    AssetId = string.Empty,
                    DefinitionId = string.Empty
                };
            }
        }
    }
}