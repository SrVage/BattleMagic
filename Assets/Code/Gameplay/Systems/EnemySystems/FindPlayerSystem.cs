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
        private const float DelayTimeFind = 0.5f;
        private const float DelayTimeLost = 5f;
        private readonly EcsFilter<GameObjectRef, Enemy, Finish>.Exclude<Delay,Death> _enemy;
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
                    if (vectorDirection.sqrMagnitude < VisibleDistance)
                    {
                        if (Vector3.Dot(normalize, enemyTransform.forward) > 0)
                        {
                            ref var entity = ref _enemy.GetEntity(edx);
                            entity.Get<Follow>().Value = playerTransform;
                            entity.Get<Delay>().Value = DelayTimeFind;
                            //entity.Del<Finish>();
                            //entity.Get<Target>().Value = playerTransform.position;
                        }
                        else
                        {
                            ref var entity = ref _enemy.GetEntity(edx);
                            entity.Del<Follow>();
                            entity.Get<Delay>().Value = DelayTimeLost;
                            //entity.Del<Finish>();
                        }
                    }
                    else
                    {
                        ref var entity = ref _enemy.GetEntity(edx);
                        entity.Del<Follow>();
                        entity.Get<Delay>().Value = DelayTimeLost;
                        //entity.Del<Finish>();
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