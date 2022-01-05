using Code.Components;
using Code.Configs;
using Code.Gameplay.Components;
using Code.MonoBehavioursComponent.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public class PlayerMoveSystem:IEcsRunSystem
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
                    ref var playerTransform = ref _player.Get1(jdx).Transform;
                    Vector3 playerVector = new Vector3(vector.x * _playerCfg.Speed, 0,
                        vector.y * _playerCfg.Speed);
                    if (playerVector.sqrMagnitude<DeadZone)
                        break;
                    playerTransform.rotation = Quaternion.LookRotation(playerVector, Vector3.up);
                    ref var playerRB = ref _player.Get3(jdx).Value;
                    playerRB.velocity= new Vector3(playerVector.x, 0, playerVector.z);
                }
            }
        }
    }
}