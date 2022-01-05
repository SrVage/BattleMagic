using System;
using Code.Components;
using Code.Gameplay.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public class PlayerMonoBehaviour:MonoBehavioursEntity
    {
        [SerializeField] private Fraction _fraction;
        [SerializeField] private Rigidbody _rigidbody;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            switch (_fraction)
            {
                case Fraction.Player:
                    entity.Get<Player>();
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