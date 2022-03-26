using System;
using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems.EnemySystems
{
    public sealed class DelayEnemySystem:IEcsRunSystem
    {
        private readonly EcsFilter<Delay, Enemy>.Exclude<Death> _enemy = null;
        
        public void Run()
        {
            foreach (var edx in _enemy)
            {
                ref var delay = ref _enemy.Get1(edx).Value;
                delay -= TimeService.Time;
                if (delay <= 0)
                {
                    ref var entity = ref _enemy.GetEntity(edx);
                    entity.Del<Delay>();
                    entity.Get<ChangeGoalTag>();
                }
            }
        }
    }
}