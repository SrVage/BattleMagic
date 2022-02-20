using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public class DestructibleMonoBehaviour:MonoBehavioursEntity
    {
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            Debug.Log(rigidbodies.Length);
            entity.Get<Destructible>().Values = rigidbodies;
        }
    }
}