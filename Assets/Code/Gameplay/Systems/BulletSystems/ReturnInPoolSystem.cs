using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems.BulletSystems
{
    public sealed class ReturnInPoolSystem:IEcsRunSystem
    {
        private readonly EcsFilter<BulletPool> _pool;
        private readonly EcsFilter<PoolReturn, GameObjectRef, Physic> _return;
        public void Run()
        {
            if (_return.IsEmpty())
                return;
            foreach (var pdx in _pool)
            {
                ref var pool = ref _pool.Get1(pdx).Value;
                foreach (var rdx in _return)
                {
                    ref var entity = ref _return.GetEntity(rdx);
                    ref var gameObject = ref _return.Get2(rdx).GameObject;
                    ref var physic = ref _return.Get3(rdx).Value;
                    physic.velocity = Vector3.zero;
                    entity.Del<PoolReturn>();
                    entity.Get<InPool>();
                    pool.ReturnToPool(gameObject);
                }
            }
        }
    }
}