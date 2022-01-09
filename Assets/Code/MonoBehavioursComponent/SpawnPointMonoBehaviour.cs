using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public class SpawnPointMonoBehaviour:MonoBehavioursEntity
    {
        [SerializeField] private Fraction _fraction;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            entity.Get<SpawnPoint>().Fraction = _fraction;
            entity.Get<SpawnPoint>().Position = transform.position;
        }
    }
}