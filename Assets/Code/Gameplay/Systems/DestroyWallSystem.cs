using Code.Abstractions;
using Code.Components;
using Code.Gameplay.Extensions;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Systems
{
    public sealed class DestroyWallSystem:IEcsRunSystem
    {
        private const float DelayTime = 3f;
        private readonly EcsFilter<Destructible, Destroy> _wall;
        private readonly EcsFilter<Destructible, Finish, GameObjectRef> _wallDestroy;
        
        public void Run()
        {
            foreach (var wdx in _wall)
            {
                ref var physics = ref _wall.Get1(wdx).Values;
                ref var entity = ref _wall.GetEntity(wdx);
                foreach (var physic in physics)
                {
                    physic.isKinematic = false;
                    physic.AddForce(physic.transform.localPosition*Random.Range(30,40), ForceMode.Impulse);
                }
                entity.Get<Delay>().Value = DelayTime;
                entity.Del<Destroy>();
            }

            foreach (var wdx in _wallDestroy)
            {
                ref var physics = ref _wallDestroy.Get1(wdx).Values;
                foreach (var physic in physics)
                {
                    physic.isKinematic = true;
                }
                var entity = _wallDestroy.GetEntity(wdx);
                ref var transform = ref _wallDestroy.Get3(wdx).Transform;
                transform.DOScale(Vector3.zero, 1f).OnComplete(() => entity.DestroyWithGameObject());
            }
        }
    }
}