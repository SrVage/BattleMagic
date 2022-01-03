using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Configs
{
    [CreateAssetMenu(order = 0, menuName = "Config/Level/LevelList")]
    public class LevelList:ScriptableObject
    {
        public List<AssetReference> Levels;
    }
}