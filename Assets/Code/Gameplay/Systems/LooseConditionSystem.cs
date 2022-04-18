using Code.Components;
using Code.StatesSwitcher;
using Code.StatesSwitcher.States;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class LooseConditionSystem:IEcsRunSystem
    {
        private readonly EcsFilter<PlayState> _play;
        private readonly EcsFilter<SpawnPoint, Player> _spawnPoint;
        private readonly EcsFilter<Player, HealthPoint> _player;
        public void Run()
        {
            if (_play.IsEmpty())
                return;
            if (_spawnPoint.IsEmpty()&&_player.IsEmpty())
            {
                ChangeGameState.Change(GameStates.LoseState);
            }
        }
    }
}