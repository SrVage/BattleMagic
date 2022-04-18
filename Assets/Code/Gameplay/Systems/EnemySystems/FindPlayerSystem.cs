using System;
using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Systems.EnemySystems
{
    public class FindPlayerSystem:IEcsRunSystem
    {
        private const int VisibleDistance = 15;
        private readonly EcsFilter<GameObjectRef, Enemy>.Exclude<Death> _enemy;
        private readonly EcsFilter<GameObjectRef, Player>.Exclude<Death> _player;
        
        public void Run()
        {
            foreach (var edx in _enemy)
            {
                ref var enemyTransform = ref _enemy.Get1(edx).Transform;
                foreach (var pdx in _player)
                {
                    ref var playerTransform = ref _player.Get1(pdx).Transform;
                    var vectorDirection = CalculateBetweenVector(playerTransform, enemyTransform, out Vector3 normalize);
                    ref var entity = ref _enemy.GetEntity(edx);
                    if (vectorDirection.sqrMagnitude < VisibleDistance+Random.Range(0,5) &&
                        Vector3.Dot(normalize, enemyTransform.forward) > 0)
                    {
                        if (!entity.Has<Delay>())
                        {
                            enemyTransform.LookAt(playerTransform);
                            enemyTransform.Rotate(0, Random.Range(-5f, 5f), 0);
                            entity.Get<StartShooting>();
                        }
                    }
                    else
                    {
                        entity.Del<StartShooting>();
                    }
                }
            }
        }

        private Vector3 CalculateBetweenVector(Transform playerTransform, Transform enemyTransform, out Vector3 normalize)
        {
            Vector3 betweenVector = playerTransform.position - enemyTransform.position;
            normalize = betweenVector.normalized;
            return betweenVector;
        }
    }
}