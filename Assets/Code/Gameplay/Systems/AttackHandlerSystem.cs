using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public class AttackHandlerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Attack> _attack;
        
        public void Run()
        {
            if (_attack.IsEmpty())
                return;
                    
            _world.NewEntity().Get<StartShooting>();
        }
    }
}