using Code.Configs;
using Code.Gameplay.Components;
using Code.Gameplay.Systems;
using Code.LevelsLoader;
using Code.StatesSwitcher;
using Code.StatesSwitcher.Events;
using Code.StatesSwitcher.States;
using Code.UI.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;

        [SerializeField] private LevelList _levels;
        [SerializeField] private UIScreen _uiScreen;
        [SerializeField] private PlayerCfg _playerCfg;
        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            ChangeGameState.World = _world;
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                .Add (new ChangeStateSystem ())
                .Add(new StateMachine())
                .Add(new LoadLevelSystem())
                .Add(new CreatePlayerSystem())
                .Add(new ChangeScreenSystem())
                .Add(new JoystickInputSystem())
                .Add(new PlayerMoveSystem())

                .OneFrame<ChangeState> ()
                .OneFrame<LoadLevelSignal> ()
                .OneFrame<TapToStart>()
                .OneFrame<InputMovementVector>()
                
                .Inject (_levels)
                .Inject(_uiScreen)
                .Inject(_playerCfg)
                .Init ();
        }
        
        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}