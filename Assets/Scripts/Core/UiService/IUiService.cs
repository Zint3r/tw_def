using System.Collections;

namespace Core.UiService
{
    public interface IUiService
    {
        IEnumerator PreloadUi(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition);
        IEnumerator OpenUiAsync(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition, IUiData uiData = null);
        void OpenUi(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition, IUiData uiData = null);
        void CloseUi(IUiDefinition uiDefinition, UiLayerDefinition uiLayerDefinition);
    }
}