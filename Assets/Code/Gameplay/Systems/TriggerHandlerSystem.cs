using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class TriggerHandlerSystem:IEcsRunSystem
    {
        private readonly EcsFilter<AttackTrigger> _trigger;
        public void Run()
        {
            foreach (var idx in _trigger)
            {
                ref var bulletEntity = ref _trigger.Get1(idx).Self;
                ref var attackedEntity = ref _trigger.Get1(idx).Entity;
                if (!attackedEntity.IsNull())
                {
                    if (attackedEntity.Has<HealthPoint>())
                    {
                        ref var damage = ref bulletEntity.Get<Damage>().Value;
                        attackedEntity.Get<Damage>().Value = damage;
                    }

                    if (attackedEntity.Has<Destructible>())
                    {
                        attackedEntity.Get<Destroy>();
                    }
                }
                bulletEntity.Get<PoolReturn>();
            }
        }
    }
}