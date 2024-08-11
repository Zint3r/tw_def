namespace Core.UiService
{
    public interface IUiDefinition
    {
        OpenBehaviour OpenBehaviour { get; }
		
        string ToString();
    }
}