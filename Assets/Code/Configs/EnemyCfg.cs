using System;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(order = 5, fileName = "EnemyCfg", menuName = "Config/Enemy")]
    public class EnemyCfg:ScriptableObject
    {
        [Serializable]
        public class EnemyConfig
        {
            public GameObject Prefab;
            public float HealthPoint;
            public int SpawnID;
        }

        public EnemyConfig[] Enemies;
    }
}