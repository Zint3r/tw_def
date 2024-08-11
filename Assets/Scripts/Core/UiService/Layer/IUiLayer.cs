using System.Collections;

namespace Core.UiService
{
    public interface IUiLayer
    {
        IEnumerator PreloadUi(IUiDefinition uiDefinition);
        void UnloadUi(IUiDefinition uiDefinition);
        IEnumerator OpenUiAsync(IUiDefinition uiDefinition, IUiData uiData);
        void OpenUi(IUiDefinition uiDefinition, IUiData uiData);
        void CloseUi(IUiDefinition uiDefinition);
    }
}