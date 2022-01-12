using System.Linq;
using Code.Abstractions;
using Code.Components;
using Code.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public class CreateEnemySystem:IEcsRunSystem
    {
        private readonly EcsFilter<SpawnPoint, Enemy> _spawnPoint;
        private readonly EcsFilter<Enemy, HealthPoint> _enemy;
        private readonly EcsWorld _world;
        private readonly EnemyCfg _enemyCfg;
        public void Run()
        {
            if (_spawnPoint.IsEmpty() || _enemy.GetEntitiesCount()>=_spawnPoint.GetEntitiesCount())
                return;
            foreach (var sdx in _spawnPoint)
            {
                ref var spawnID = ref _spawnPoint.Get2(sdx).SpawnID;
                bool breakCycle = false;
                foreach (var edx in _enemy)
                {
                    ref var enemyID = ref _enemy.Get1(sdx).SpawnID;
                    if (spawnID == enemyID)
                    {
                        breakCycle = true;
                        break;
                    }
                }
                if (breakCycle)
                    break;
                ref var spawnPoint = ref _spawnPoint.Get1(sdx).Position;
                ref var number = ref _spawnPoint.Get1(sdx).Number;
                InitializeEnemy(spawnPoint, number);
            }
        }
        
        private void InitializeEnemy(Vector3 spawnPoint, int number)
        {
            var prefab = _enemyCfg.Enemies.Where(e => e.SpawnID == number).Select(p => p.Prefab).First();
            GameObject enemy = GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
            var entity = _world.NewEntity();
            enemy.GetComponent<MonoBehavioursEntity>().Initial(entity, _world);
            entity.Get<HealthPoint>().Value = _enemyCfg.Enemies.Where(e => e.SpawnID == number).Select(p => p.HealthPoint).First();
        }
    }
}