namespace Game.Features.GameDesign
{
    public class GameDefinition
    {
        public string DefinitionId;
        private string assetId;

        public string AssetId
        {
            get => string.IsNullOrEmpty(assetId) ? DefinitionId : assetId;
            set => assetId = value;
        }
        
        public override bool Equals(object obj)
        {
            return obj is GameDefinition definition
                   && definition.GetType() == GetType()
                   && definition.DefinitionId == DefinitionId;
        }

        public override int GetHashCode()
        {
            return DefinitionId.GetHashCode();
        }
    }
}