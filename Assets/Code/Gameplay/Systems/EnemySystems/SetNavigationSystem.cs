using Code.Components;
using Code.Configs;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems.EnemySystems
{
    public sealed class SetNavigationSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Navigation, Target> _navigation = null;
        private readonly EnemyCfg _enemyCfg = null;
        
        public void Run()
        {
            foreach (var ndx in _navigation)
            {
                ref var agent = ref _navigation.Get1(ndx).Value;
                ref var target = ref _navigation.Get2(ndx).Value;
                ref var speed = ref _navigation.Get1(ndx).Speed;
                agent.SetDestination(target);
                agent.speed = speed;
                ref var entity = ref _navigation.GetEntity(ndx);
                entity.Del<Target>();
            }
        }
    }
}