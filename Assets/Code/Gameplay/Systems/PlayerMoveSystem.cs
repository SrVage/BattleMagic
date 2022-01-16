using Code.Abstractions;
using Code.Components;
using Code.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class PlayerMoveSystem:IEcsRunSystem
    {
        private const float DeadZone = 0.1f;
        private readonly EcsFilter<GameObjectRef, Player, Physic> _player;
        private readonly EcsFilter<InputMovementVector> _input;
        private readonly PlayerCfg _playerCfg;
        public void Run()
        {
            if (_player.IsEmpty()||_input.IsEmpty())
                return;
            foreach (var idx in _input)
            {
                ref var vector = ref _input.Get1(idx).Value;
                foreach (var jdx in _player)
                {
                    Vector3 playerVector = new Vector3(vector.x * _playerCfg.Speed, 0,
                        vector.y * _playerCfg.Speed);
                    if (playerVector.sqrMagnitude<DeadZone)
                        break;
                    RotatePlayer(jdx, playerVector);
                    ChangeVelocity(jdx, playerVector);
                }
            }
        }

        private void ChangeVelocity(int jdx, Vector3 playerVector)
        {
            ref var playerRB = ref _player.Get3(jdx).Value;
            playerRB.velocity = new Vector3(playerVector.x, 0, playerVector.z);
        }

        private void RotatePlayer(int jdx, Vector3 playerVector)
        {
            ref var playerTransform = ref _player.Get1(jdx).Transform;
            playerTransform.rotation = Quaternion.LookRotation(playerVector, Vector3.up);
        }
    }
}