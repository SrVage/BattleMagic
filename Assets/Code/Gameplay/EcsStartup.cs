using Code.Components;
using Code.Configs;
using Code.Gameplay.Systems;
using Code.Gameplay.Systems.EnemySystems;
using Code.LevelsLoader;
using Code.StatesSwitcher;
using Code.StatesSwitcher.Events;
using Code.StatesSwitcher.States;
using Code.UI.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Camera = UnityEngine.Camera;

namespace Code.Gameplay {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;

        [SerializeField] private LevelList _levels;
        [SerializeField] private UIScreen _uiScreen;
        [SerializeField] private PlayerCfg _playerCfg;
        [SerializeField] private BulletsCfg _bulletsCfg;
        [SerializeField] private EnemyCfg _enemyCfg;
        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            ChangeGameState.World = _world;
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                .Add(new GameInitial())
                .Add(new CreatePoolSystem())
                .Add(new WinConditionSystem())
                .Add (new ChangeStateSystem ())
                .Add(new StateMachine())
                .Add(new LoadLevelSystem())
                .Add(new CreatePlayerSystem())
                .Add(new CreateEnemySystem())
                .Add(new ChangeScreenSystem())
                .Add(new JoystickInputSystem())
                .Add(new AttackHandlerSystem())
                .Add(new CreateBulletSystem())
                .Add(new PlayerMoveSystem())
                .Add(new TriggerHandlerSystem())
                .Add(new DamageHealthSystem())
                .Add(new DestroyWallSystem())
                .Add(new ReturnInPoolSystem())
                .Add(new DelaySystem())
                .Add(new DrawHealthSystem(Camera.main.transform))
                .Add(new FindPlayerSystem())
                .Add(new SetTargetSystem())
                .Add(new SetNavigationSystem())
                .Add(new AnimationRunSystem())
                

                .OneFrame<ChangeState> ()
                .OneFrame<LoadLevelSignal> ()
                .OneFrame<TapToStart>()
                .OneFrame<InputMovementVector>()
                .OneFrame<Attack>()
                .OneFrame<AttackTrigger>()
                .OneFrame<StartShooting>()

                .Inject (_levels)
                .Inject(_uiScreen)
                .Inject(_playerCfg)
                .Inject(_bulletsCfg)
                .Inject(_enemyCfg)
                .Init ();
        }
        
        void Update ()
        {
            TimeService.Time = Time.deltaTime;
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