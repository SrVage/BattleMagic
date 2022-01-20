using Code.Components;
using Code.StatesSwitcher;
using Code.StatesSwitcher.States;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class WinConditionSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Enemy> _enemy;
        private readonly EcsFilter<PlayState> _play;
        public void Run()
        {
            if (_enemy.IsEmpty()&&!_play.IsEmpty())
            {
                ChangeGameState.Change(GameStates.WinState);
            }
        }
    }
}