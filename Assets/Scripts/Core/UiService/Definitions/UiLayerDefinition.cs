namespace Core.UiService
{
    public class UiLayerDefinition
    {
        public readonly string Name;
        public readonly int Ordinal;

        public UiLayerDefinition(string name, int ordinal)
        {
            Name = name;
            Ordinal = ordinal;
        }
    }
}