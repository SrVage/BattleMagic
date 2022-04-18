using Code.Components;
using Code.Configs;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems.EnemySystems
{
    public sealed class SetNavigationSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Navigation, Target> .Exclude<Death, Delay> _navigationFollow = null;
        private readonly EcsFilter<Navigation, StartShooting> _navigationShooting = null;
        private readonly EcsFilter<Navigation, Death>  _death = null;
        private readonly EnemyCfg _enemyCfg = null;
        
        public void Run()
        {
            foreach (var ndx in _navigationFollow)
            {
                ref var agent = ref _navigationFollow.Get1(ndx).Value;
                ref var target = ref _navigationFollow.Get2(ndx).Value;
                if (target == null)
                {
                    agent.isStopped = true;
                    agent.SetDestination(agent.transform.position);
                    _navigationFollow.GetEntity(ndx).Get<NonTarget>();
                    continue;
                }
                ref var speed = ref _navigationFollow.Get1(ndx).Speed;
                agent.SetDestination(target.position);
                agent.speed = speed;
                agent.isStopped = false;
            }
            foreach (var ddx in _death)
            {
                ref var agent = ref _death.Get1(ddx).Value;
                agent.isStopped = true;
            }

            foreach (var ndx in _navigationShooting)
            {
                ref var agent = ref _navigationShooting.Get1(ndx).Value;
                agent.isStopped = true;
            }
        }
    }
}