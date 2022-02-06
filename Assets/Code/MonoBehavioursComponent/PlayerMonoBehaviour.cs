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
            var animevent = gameObject.AddComponent<AnimatorEventMonoBehaviour>();
            animevent.Initial(entity, world);
        }
    }
}