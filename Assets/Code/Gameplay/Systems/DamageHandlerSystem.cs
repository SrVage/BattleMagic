using Code.Components;
using Code.Gameplay.Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public class DamageHandlerSystem:IEcsRunSystem
    {
        private readonly EcsFilter<AttackTrigger> _trigger;
        public void Run()
        {
            foreach (var idx in _trigger)
            {
                ref var bulletEntity = ref _trigger.Get1(idx).Self;
                ref var attackedEntity = ref _trigger.Get1(idx).Entity;
                /*if (bulletEntity.IsAlive()||bulletEntity.IsNull())
                    break;*/
                ref var damage = ref bulletEntity.Get<Damage>().Value;
                ref var hp =ref attackedEntity.Get<HealthPoint>().Value;
                hp -= damage;
                bulletEntity.DestroyWithGameObject();
                Debug.Log(hp);
            }
        }
    }
}