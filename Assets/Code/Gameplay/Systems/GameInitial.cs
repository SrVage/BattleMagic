using Code.Abstractions;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class GameInitial:IEcsInitSystem
    {
        private readonly EcsWorld _world;
        public void Init()
        {
            var objects = Object.FindObjectsOfType<MonoBehavioursEntity>();
            foreach (var obj in objects)
            {
                obj.Initial(_world.NewEntity(), _world);
            }
        }
    }
}