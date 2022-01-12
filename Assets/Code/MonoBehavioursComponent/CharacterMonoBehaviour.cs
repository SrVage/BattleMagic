using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public abstract class CharacterMonoBehaviour:MonoBehavioursEntity
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _attackPoint;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            entity.Get<AttackPoint>().Value = _attackPoint;
            entity.Get<Physic>().Value = _rigidbody;
        }
    }
}