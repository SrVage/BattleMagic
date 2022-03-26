using Code.Components;
using Code.Gameplay.Extensions;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class DestroyPlayerSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Death, Destroy> _playerDestroy = null;
        public void Run()
        {
            foreach (var pdx in _playerDestroy)
            {
                _playerDestroy.GetEntity(pdx).DestroyWithGameObject();
            }
        }
    }
}