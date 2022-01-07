using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(order = 3, fileName = "PlayerCfg", menuName = "Config/Player")]
    public class PlayerCfg:ScriptableObject
    {
        public GameObject Prefab;
        public float HealthPoint;
        public float Speed;
    }
}