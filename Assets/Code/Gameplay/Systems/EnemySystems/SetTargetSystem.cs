using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems.EnemySystems
{
    public sealed class SetTargetSystem:IEcsRunSystem
    {
        private readonly EcsFilter<NonTarget> _navigation = null;
        private readonly EcsFilter<GameObjectRef, Player> _player;
        
        public void Run()
        {
            foreach (var ndx in _navigation)
            {
                ref var entity = ref _navigation.GetEntity(ndx);
                foreach (var pdx in _player)
                {
                    ref var position = ref _player.Get1(pdx).Transform;
                    entity.Get<Target>().Value = position;
                }
                entity.Del<NonTarget>();
            }
        }
    }
}