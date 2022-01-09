using Code.Abstractions;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Components
{
    public struct BulletPool
    {
        public IPool Value;

        public BulletPool(int count, GameObject prefab, EcsWorld world)
        {
            Value = new Pool.BulletPool(count, prefab, world);
        }
    }
}