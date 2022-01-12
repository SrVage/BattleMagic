using Code.Components;
using Code.Configs;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public class CreatePoolSystem:IEcsInitSystem
    {
        private readonly EcsFilter<BulletPool> _pool;
        private readonly EcsWorld _world;
        private readonly BulletsCfg _bulletsCfg;
        
        public void Init()
        {
            if (!_pool.IsEmpty())
                return;
            BulletPool pool = new BulletPool(10, _bulletsCfg.Bullets[0].Prefab, _world);
            ref var bulletPool = ref _world.NewEntity().Get<BulletPool>();
            bulletPool = pool;
        }
    }
}