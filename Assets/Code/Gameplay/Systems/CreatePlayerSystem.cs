using Code.Components;
using Code.Configs;
using Code.Gameplay.Components;
using Code.MonoBehavioursComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public class CreatePlayerSystem:IEcsRunSystem
    {
        private readonly EcsFilter<SpawnPoint> _spawnPoint;
        private readonly EcsFilter<Player> _player;
        private readonly EcsWorld _world;
        private readonly PlayerCfg _playerCfg;
        
        public void Run()
        {
            if (_spawnPoint.IsEmpty() || !_player.IsEmpty())
                return;
            foreach (var idx in _spawnPoint)
            {
                ref var spawnPoint = ref _spawnPoint.Get1(idx).Position;
                ref var spawnFraction = ref _spawnPoint.Get1(idx).Fraction;
                if (spawnFraction == Fraction.Player)
                {
                    GameObject.Instantiate(_playerCfg.Prefab, spawnPoint, Quaternion.identity).GetComponent<MonoBehavioursEntity>().Initial(_world.NewEntity(), _world);
                }

                ref var entity = ref _spawnPoint.GetEntity(idx);
                entity.Destroy();
            }
        }
    }
}