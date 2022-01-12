using System;
using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public class SpawnPointMonoBehaviour:MonoBehavioursEntity
    {
        [SerializeField] private Fraction _fraction;
        [Tooltip("Количество появлений из данной точки")]
        [SerializeField] private int _number;
        [SerializeField] private int _id;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            switch (_fraction)
            {
                case Fraction.Player:
                    entity.Get<Player>();
                    break;
                case Fraction.Enemy:
                    entity.Get<Enemy>().SpawnID = _id;
                    break;
            }
            entity.Get<SpawnPoint>().Position = transform.position;
            entity.Get<SpawnPoint>().Number = _number;
        }
    }
}