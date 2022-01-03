using Code.MonoBehavioursComponent;
using Code.StatesSwitcher.Events;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class GameplayScreen:UIEntity
    {
        [SerializeField] private Button _reset;
        [SerializeField] private Button _clear;
        public override void Initial(EcsWorld world)
        {
            base.Initial(world);
            _reset.onClick.AddListener(ResetGame);
            _clear.onClick.AddListener(Clear);
        }

        private void ResetGame()
        {
            _reset.onClick.RemoveAllListeners();
            _clear.onClick.RemoveAllListeners();
            _world.NewEntity().Get<ResetGame>();
        }

        private void Clear()
        {
            PlayerPrefs.DeleteAll();
            _reset.onClick.RemoveAllListeners();
            _clear.onClick.RemoveAllListeners();
        }
    }
}