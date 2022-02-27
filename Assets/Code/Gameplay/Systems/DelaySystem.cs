using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class DelaySystem:IEcsRunSystem
    {
        private readonly EcsFilter<Delay>.Exclude<Enemy> _delay;
        public void Run()
        {
            foreach (var ddx in _delay)
            {
                ref var delayTime = ref _delay.Get1(ddx).Value;
                delayTime -= Time.deltaTime;
                if (delayTime <= 0)
                {
                    ref var entity = ref _delay.GetEntity(ddx);
                    entity.Del<Delay>();
                    entity.Get<Finish>();
                }
            }
        }
    }
}