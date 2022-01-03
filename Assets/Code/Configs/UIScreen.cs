using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(order = 2, fileName = "UIScreen", menuName = "Config/UIScreen")]
    public class UIScreen:ScriptableObject
    {
        public GameObject TTSScreen;
        public GameObject GameplayScreen;
        public GameObject WinScreen;
        public GameObject ConfigScreen;
    }
}