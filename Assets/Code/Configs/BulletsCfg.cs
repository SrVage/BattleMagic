using System;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(order = 4, fileName = "BulletCfg", menuName = "Config/Bullet")]
    public class BulletsCfg:ScriptableObject
    {
        [Serializable]
        public class Bullet
        {
            public GameObject Prefab;
            public float Damage;
            public float ReloadTime;
        }

        public Bullet[] Bullets;
    }
}