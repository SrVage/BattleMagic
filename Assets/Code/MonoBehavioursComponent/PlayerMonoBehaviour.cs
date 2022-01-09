using System;
using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public class PlayerMonoBehaviour:MonoBehavioursEntity
    {
        [SerializeField] private Fraction _fraction;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _attackPoint;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            switch (_fraction)
            {
                case Fraction.Player:
                    entity.Get<Player>();
                    entity.Get<AttackPoint>().Value = _attackPoint;
                    entity.Get<Physic>().Value = _rigidbody;
                    break;
                case Fraction.Enemy:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}