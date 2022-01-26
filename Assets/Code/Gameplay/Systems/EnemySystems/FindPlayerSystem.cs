using System;
using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems.EnemySystems
{
    public class FindPlayerSystem:IEcsRunSystem
    {
        private const int VisibleDistance = 25;
        private readonly EcsFilter<GameObjectRef, Enemy> _enemy;
        private readonly EcsFilter<GameObjectRef, Player> _player;
        
        public void Run()
        {
            foreach (var edx in _enemy)
            {
                ref var enemyTransform = ref _enemy.Get1(edx).Transform;
                foreach (var pdx in _player)
                {
                    ref var playerTransform = ref _player.Get1(pdx).Transform;
                    var vectorDirection = CalculateBetweenVector(playerTransform, enemyTransform, out Vector3 normalize);
                    if (vectorDirection.sqrMagnitude < VisibleDistance)
                    {
                        if (Vector3.Dot(normalize, enemyTransform.forward) > 0)
                        {
                            ref var entity = ref _enemy.GetEntity(edx);
                            entity.Get<Target>().Value = playerTransform.position;
                        }
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