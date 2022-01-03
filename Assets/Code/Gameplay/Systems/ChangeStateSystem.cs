using Code.StatesSwitcher;
using Code.StatesSwitcher.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems

{
    public class ChangeStateSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<TapToStart> _startLevelSignal;
        private readonly EcsWorld _world;

        public void Run()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                ChangeGameState.Change(GameStates.LoseState);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangeGameState.Change(GameStates.WinState);
            }
            if (!_startLevelSignal.IsEmpty())
                ChangeGameState.Change(GameStates.PlayState);
        }

        public void Init()
        {
            ChangeGameState.Change(GameStates.StartState);
        }
    }
}