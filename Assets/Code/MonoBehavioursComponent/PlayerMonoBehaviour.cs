using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehavioursComponent
{
    public class PlayerMonoBehaviour:CharacterMonoBehaviour
    {
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            entity.Get<Player>();
            var animEvent = gameObject.AddComponent<AnimatorEventMonoBehaviour>();
            animEvent.Initial(world);
        }
    }
}