using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems.EnemySystems
{
    public sealed class SetTargetSystem:IEcsRunSystem
    {
        private readonly EcsFilter<NonTarget> _navigation = null;
        private readonly EcsFilter<SpawnPoint, Player> _playerSpawn;
        
        public void Run()
        {
            foreach (var ndx in _navigation)
            {
                ref var entity = ref _navigation.GetEntity(ndx);
                foreach (var pdx in _playerSpawn)
                {
                    ref var spawn = ref _playerSpawn.Get1(pdx).Position;
                    entity.Get<Target>().Value = spawn;
                }
                entity.Del<NonTarget>();
            }
        }
    }
}