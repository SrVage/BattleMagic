using Code.Abstractions;
using Code.Components;
using Code.Configs;
using Leopotam.Ecs;
using UnityEngine;
using Camera = Code.Components.Camera;

namespace Code.Gameplay.Systems
{
    public class CreatePlayerSystem:IEcsRunSystem
    {
        private readonly EcsFilter<SpawnPoint, Player> _spawnPoint;
        private readonly EcsFilter<Player, HealthPoint> _player;
        private readonly EcsFilter<Camera> _camera;
        private readonly EcsWorld _world;
        private readonly PlayerCfg _playerCfg;
        
        public void Run()
        {
            if (_spawnPoint.IsEmpty() || !_player.IsEmpty())
                return;
            foreach (var idx in _spawnPoint)
            {
                ref var spawnPoint = ref _spawnPoint.Get1(idx).Position;
                ref var lives = ref _spawnPoint.Get1(idx).Number;
                var playerGO = InitializePlayer(spawnPoint);
                lives--;
                if (lives <= 0)
                {
                    ref var entity = ref _spawnPoint.GetEntity(idx);
                    entity.Destroy();
                }
                foreach (var jdx in _camera)
                {
                    BindCamera(jdx, playerGO);
                }
            }
        }

        private GameObject InitializePlayer(Vector3 spawnPoint)
        {
            GameObject player = GameObject.Instantiate(_playerCfg.Prefab, spawnPoint, Quaternion.identity);
            var entity = _world.NewEntity();
            player.GetComponent<MonoBehavioursEntity>().Initial(entity, _world);
            entity.Get<HealthPoint>().Value = _playerCfg.HealthPoint;
            return player;
        }

        private void BindCamera(int jdx, GameObject playerGO)
        {
            ref var camera = ref _camera.Get1(jdx).Value;
            camera.Follow = playerGO.transform;
        }
    }
}