using Code.Abstractions;
using Code.Components;
using Code.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems.BulletSystems
{
    public sealed class CreateBulletSystem:IEcsRunSystem
    {
        private readonly EcsFilter<AttackPoint, AnimatorEventAttack> _player;
        private readonly EcsFilter<AttackPoint, Player> _playerFreeze;
        private readonly EcsFilter<BulletPool> _pool;
        private readonly EcsFilter<Freeze> _freezeSignal;
        //private readonly EcsFilter<AnimatorEventAttack> _event;
        private readonly BulletsCfg _bulletsCfg;
        private readonly EcsWorld _world;
        private float _reloadTime = 0;
        private float _reloadTime2 = 0;
        public void Run()
        {
            _reloadTime -= TimeService.Time;
            _reloadTime2 -= TimeService.Time;
            if (!_freezeSignal.IsEmpty() && _reloadTime2 <= 0)
            {
                Fire(false);
                _reloadTime2 = _bulletsCfg.Bullets[1].ReloadTime; 
            }
            foreach (var idx in _player)
            {
                if (_reloadTime > 0)
                {
                    break;
                }
                ref var entity = ref _player.GetEntity(idx);
                entity.Del<AnimatorEventAttack>();
                Fire(true);
                _reloadTime = _bulletsCfg.Bullets[0].ReloadTime;
            }
        }

        private void Fire(bool general)
        {
            if (general)
            {
                foreach (var idx in _player)
                {
                    foreach (var pdx in _pool)
                    {
                        ref var transform = ref _player.Get1(idx).Value;
                        CreateBullet(true, pdx, idx, transform);
                    }
                }
            }
            else
            {
                foreach (var idx in _playerFreeze)
                {
                    foreach (var pdx in _pool)
                    {
                        ref var transform = ref _playerFreeze.Get1(idx).Value;
                        CreateBullet(false, pdx, idx, transform);
                    }
                }
            }
        }

        private void CreateBullet(bool general, int pdx, int idx, Transform transform)
        {
            ref IPool pool = ref _pool.Get1(pdx).Value;
            ref IPool alternative = ref _pool.Get1(pdx).Alternative;
            var bullet = general ? pool.GetBullet() : alternative.GetBullet();
            Debug.Log(bullet);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            ref var entity = ref bullet.GetComponent<EntityRef>().Entity;
            entity.Get<Damage>().Value = general ? _bulletsCfg.Bullets[0].Damage : _bulletsCfg.Bullets[1].Damage;
            bullet.SetActive(true);
            entity.Get<Physic>().Value.AddForce(bullet.transform.forward * 4, ForceMode.Impulse);
            if (!general)
                bullet.GetComponent<EntityRef>().Entity.Get<FreezeTag>();
            bullet.GetComponent<EntityRef>().Entity.Del<InPool>();
        }
    }
}