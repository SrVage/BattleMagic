using Code.Gameplay.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public class JoystickInputSystem:IEcsRunSystem
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        private readonly EcsWorld _world;

        public void Run()
        {
            float xAxis = SimpleInput.GetAxis(Horizontal);
            float yAxis = SimpleInput.GetAxis(Vertical);
            _world.NewEntity().Get<InputMovementVector>().Value = new Vector2(xAxis, yAxis);
        }
    }
}