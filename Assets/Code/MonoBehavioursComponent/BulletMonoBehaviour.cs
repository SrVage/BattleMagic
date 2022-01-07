using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public class BulletMonoBehaviour:MonoBehavioursEntity
    {
        [SerializeField] private Rigidbody _rigidbody;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            entity.Get<Physic>().Value = _rigidbody;
            gameObject.AddComponent<TriggerListener>().Initial(world, entity);
        }
    }
}