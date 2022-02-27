using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems.BulletSystems
{
    public sealed class AttackHandlerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private readonly EcsFilter<Player> _player = null;
        private EcsFilter<Attack> _attack;
        
        public void Run()
        {
            if (_attack.IsEmpty())
                return;
            foreach (var pdx in _player)
            {
                _player.GetEntity(pdx).Get<StartShooting>();
            }
        }
    }
}