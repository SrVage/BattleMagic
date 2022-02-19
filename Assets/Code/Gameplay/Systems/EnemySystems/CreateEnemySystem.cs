using System.Linq;
using Code.Abstractions;
using Code.Components;
using Code.Configs;
using Code.StatesSwitcher.States;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems.EnemySystems
{
    public sealed class CreateEnemySystem:IEcsRunSystem
    {
        private readonly EcsFilter<SpawnPoint, Enemy> _spawnPoint;
        private readonly EcsFilter<Enemy, HealthPoint> _enemy;
        private readonly EcsFilter<PlayState> _playState;
        private readonly EcsWorld _world;
        private readonly EnemyCfg _enemyCfg;
        public void Run()
        {
            if (_playState.IsEmpty())
                return;
            if (_spawnPoint.IsEmpty() || _enemy.GetEntitiesCount()>=_spawnPoint.GetEntitiesCount())
                return;
            foreach (var sdx in _spawnPoint)
            {
                ref var spawnID = ref _spawnPoint.Get2(sdx).SpawnID;
                bool breakCycle = false;
                foreach (var edx in _enemy)
                {
                    ref var enemyID = ref _enemy.Get1(edx).SpawnID;
                    if (spawnID == enemyID)
                    {
                        breakCycle = true;
                        break;
                    }
                }

                if (breakCycle)
                {
                    continue;
                }
                ref var spawnPoint = ref _spawnPoint.Get1(sdx).Position;
                ref var number = ref _spawnPoint.Get1(sdx).Number;
                InitializeEnemy(spawnPoint, spawnID);
                number--;
                if (number <= 0)
                {
                    ref var entity = ref _spawnPoint.GetEntity(sdx);
                    entity.Destroy();
                }
            }
        }
        
        private void InitializeEnemy(Vector3 spawnPoint, int id)
        {
            var prefab = _enemyCfg.Enemies.Where(e => e.SpawnID == id).Select(p => p.Prefab).First();
            GameObject enemy = GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
            var entity = _world.NewEntity();
            enemy.GetComponent<MonoBehavioursEntity>().Initial(entity, _world);
            entity.Get<Navigation>().Speed = _enemyCfg.Enemies.Where(e => e.SpawnID == id).Select(p => p.Speed).First();
            entity.Get<Enemy>().SpawnID = id;
            entity.Get<HealthPoint>().Value = _enemyCfg.Enemies.Where(e => e.SpawnID == id).Select(p => p.HealthPoint).First();
            entity.Get<NonTarget>();
            entity.Get<Delay>().Value = 3f;
        }
    }
}