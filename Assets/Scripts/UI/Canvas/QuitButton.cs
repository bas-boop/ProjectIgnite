using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvas
{
    public sealed class QuitButton : Button
    {
        public void QuitGame() => Application.Quit();
    }
}