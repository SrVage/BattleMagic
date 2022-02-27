using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems.PlayerSystems
{
    public sealed class JoystickInputSystem:IEcsRunSystem
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Attack = "Attack";

        private readonly EcsWorld _world;

        public void Run()
        {
            float xAxis = SimpleInput.GetAxis(Horizontal);
            float yAxis = SimpleInput.GetAxis(Vertical);
            var attack = SimpleInput.GetButton(Attack);
            _world.NewEntity().Get<InputMovementVector>().Value = new Vector2(xAxis, yAxis);
            if (attack)
                _world.NewEntity().Get<Attack>();
        }
    }
}