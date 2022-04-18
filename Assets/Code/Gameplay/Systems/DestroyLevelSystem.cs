using Code.Abstractions;
using Code.Components;
using Code.Gameplay.Extensions;
using Code.StatesSwitcher;
using Code.StatesSwitcher.Events;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class DestroyLevelSystem:IEcsRunSystem
    {
        private readonly EcsFilter<GameObjectRef>.Exclude<InPool> _destroyObjects = null;
        private readonly EcsFilter<SpawnPoint> _spawn = null;
        private readonly EcsFilter<Restart> _restart = null;
        private readonly EcsFilter<NextLevel> _next = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            if (_restart.IsEmpty()&&_next.IsEmpty())
                return;
            foreach (var ddx in _destroyObjects)
            {
                _destroyObjects.GetEntity(ddx).DestroyWithGameObject();
            }
            foreach (var sdx in _spawn)
            {
                _spawn.GetEntity(sdx).Destroy();
            }
            if (!_restart.IsEmpty())
                _world.NewEntity().Get<ReloadLevelSignal>();
            else
                _world.NewEntity().Get<LoadLevelSignal>();
            ChangeGameState.Change(GameStates.StartState);
        }
    }
}