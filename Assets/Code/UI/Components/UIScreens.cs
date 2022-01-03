using UnityEngine;

namespace Code.UI.Components
{
    public enum Screens
    {
        TapToStart=0,
        GamePlay=1,
        Win = 2,
        Lose = 3
    }
    public struct UIScreens
    {
        public Screens Screens;
        public GameObject Screen;
    }
}