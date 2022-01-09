using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public class TriggerListener:MonoBehaviour
    {
        private EcsWorld _world;
        private EcsEntity _entity;

        public void Initial(EcsWorld world, EcsEntity entity)
        {
            _world = world;
            _entity = entity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<EntityRef>())
            {
                ref var entity = ref _world.NewEntity().Get<AttackTrigger>();
                entity.Entity = other.GetComponentInParent<EntityRef>().Entity;
                entity.Self = _entity;
            }
            else
            {
                ref var entity = ref _world.NewEntity().Get<AttackTrigger>();
                entity.Self = _entity;
            }
        }
    }
}