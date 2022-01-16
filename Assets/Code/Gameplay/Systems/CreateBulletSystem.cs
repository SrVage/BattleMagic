using Code.Abstractions;
using Code.Components;
using Code.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class CreateBulletSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Attack> _attack;
        private readonly EcsFilter<AttackPoint, Player> _player;
        private readonly EcsFilter<BulletPool> _pool;
        private readonly BulletsCfg _bulletsCfg;
        private readonly EcsWorld _world;
        private float _reloadTime = 0;
        public void Run()
        {
            _reloadTime -= TimeService.Time;
            if (_attack.IsEmpty())
                return;
            foreach (var idx in _player)
            {
                if (_reloadTime > 0)
                {
                    break;
                }

                foreach (var pdx in _pool)
                {
                    ref IPool pool = ref _pool.Get1(pdx).Value;
                    ref var transform = ref _player.Get1(idx).Value;
                    var bullet = pool.GetBullet();
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = transform.rotation;
                    ref var entity = ref bullet.GetComponent<EntityRef>().Entity;
                    entity.Get<Damage>().Value = _bulletsCfg.Bullets[0].Damage;
                    bullet.SetActive(true);
                    entity.Get<Physic>().Value.AddForce(bullet.transform.forward*5, ForceMode.Impulse);
                    bullet.GetComponent<EntityRef>().Entity.Del<InPool>();
                }
                _reloadTime = _bulletsCfg.Bullets[0].ReloadTime;
            }
        }
    }
}