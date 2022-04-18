using Code.Abstractions;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Components
{
    public struct BulletPool
    {
        public IPool Value;
        public IPool Alternative;

        public BulletPool(int count, GameObject prefab, EcsWorld world, GameObject prefabAlter)
        {
            Value = new Pool.BulletPool(count, prefab, world);
            Alternative = new Pool.BulletPool(count, prefabAlter, world);
        }
    }
}