using System;

namespace Core.UiService
{
    public interface IUiPresenterAnimation
    {
        void PlayOpenAnimation(Action animationCompleteCallback);

        void PlayCloseAnimation(Action animationCompleteCallback);
    }
}