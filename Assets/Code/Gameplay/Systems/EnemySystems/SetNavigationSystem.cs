using Code.Components;
using Code.Configs;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems.EnemySystems
{
    public sealed class SetNavigationSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Navigation, Target, ChangeGoalTag>.Exclude<Follow, Death> _navigation = null;
        private readonly EcsFilter<Navigation, Target, Follow, ChangeGoalTag> .Exclude<Death> _navigationFollow = null;
        private readonly EcsFilter<Navigation, StartShooting> _navigationShooting = null;
        private readonly EcsFilter<Navigation, Death>  _death = null;
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
                _navigation.GetEntity(ndx).Del<ChangeGoalTag>();
                //ref var entity = ref _navigation.GetEntity(ndx);
                //entity.Del<Target>();
            }
            foreach (var ndx in _navigationFollow)
            {
                ref var agent = ref _navigationFollow.Get1(ndx).Value;
                ref var target = ref _navigationFollow.Get3(ndx).Value;
                ref var speed = ref _navigation.Get1(ndx).Speed;
                agent.SetDestination(target.position);
                agent.speed = speed;
                _navigation.GetEntity(ndx).Del<ChangeGoalTag>();
                //ref var entity = ref _navigation.GetEntity(ndx);
                //entity.Del<Target>();
            }
            foreach (var ddx in _death)
            {
                ref var agent = ref _death.Get1(ddx).Value;
                agent.isStopped = true;
                _death.GetEntity(ddx).Del<Navigation>();
            }

            foreach (var ndx in _navigationShooting)
            {
                ref var agent = ref _navigationShooting.Get1(ndx).Value;
                agent.isStopped = true;
            }
        }
    }
}