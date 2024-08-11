using Core.UiService;

namespace Core.Map
{
    public class MapPresenter : UiPresenter
    {
        protected override bool Interactable { get; set; }
        protected override bool Visible { get; set; }
    }
}