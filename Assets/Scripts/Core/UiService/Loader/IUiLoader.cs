using System;
using System.Collections;
using UnityEngine;

namespace Core.UiService
{
    public interface IUiLoader
    {
        IEnumerator LoadUi(IUiDefinition uiDefinition, Action<IUiDefinition, UiPresenter> callback);
        
        void UnloadUi(UiPresenter uiPresenter);
        
        UiPresenter Instantiate(UiPresenter uiPresenter);
    }
}