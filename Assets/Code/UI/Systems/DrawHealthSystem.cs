using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.UI.Systems
{
    public class DrawHealthSystem:IEcsRunSystem
    {
        private readonly EcsFilter<HPView, HealthPoint> _hp;
        private readonly Transform _camera;

        public DrawHealthSystem(Transform camera)
        {
            _camera = camera;
        }
        
        public void Run()
        {
            foreach (var hdx in _hp)
            {
                ref var transformHP = ref _hp.Get1(hdx).Transform;
                transformHP.LookAt(_camera.position);
                ref var hp = ref _hp.Get2(hdx).Value;
                ref var hpView = ref _hp.Get1(hdx).Value;
                hpView.fillAmount = hp / 100;
            }
        }
    }
}