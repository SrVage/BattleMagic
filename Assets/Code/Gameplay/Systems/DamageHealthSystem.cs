using Code.Components;
using Code.Gameplay.Extensions;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class DamageHealthSystem:IEcsRunSystem
    {
        private readonly EcsFilter<HealthPoint, Damage> _health;
        public void Run()
        {
            foreach (var hdx in _health)
            {
                ref var hp = ref _health.Get1(hdx).Value;
                ref var damage = ref _health.Get2(hdx).Value;
                ref var entity = ref _health.GetEntity(hdx);
                hp -= damage;
                entity.Del<Damage>();
                if (hp <= 0)
                {
                    entity.DestroyWithGameObject();
                }
            }
        }
    }
}