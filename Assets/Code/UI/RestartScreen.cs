using Code.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MonoBehavioursComponent
{
    public class RestartScreen:UIEntity
    {
        [SerializeField] private Button _restart;
        private EcsWorld _world;
        public override void Initial(EcsWorld world)
        {
            _world = world;
            _restart.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            _world.NewEntity().Get<Restart>();
            _restart.onClick.RemoveAllListeners();
        }
    }
}