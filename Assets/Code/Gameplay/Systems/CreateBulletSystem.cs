using Code.Components;
using Code.Configs;
using Code.MonoBehavioursComponent;
using Code.MonoBehavioursComponent.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public class CreateBulletSystem:IEcsRunSystem
    {
        private readonly EcsFilter<Attack> _attack;
        private readonly EcsFilter<AttackPoint, Player> _player;
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
                ref var transform = ref _player.Get1(idx).Value;
                var bullet = GameObject.Instantiate(_bulletsCfg.Bullets[0].Prefab, transform.position, transform.rotation);
                var entity = _world.NewEntity();
                bullet.GetComponent<MonoBehavioursEntity>().Initial(entity, _world);
                entity.Get<Damage>().Value = _bulletsCfg.Bullets[0].Damage;
                entity.Get<Physic>().Value.AddForce(bullet.transform.forward*5, ForceMode.Impulse);
                _reloadTime = _bulletsCfg.Bullets[0].ReloadTime;
            }
        }
    }
}