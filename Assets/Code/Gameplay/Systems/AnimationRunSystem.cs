using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Gameplay.Systems
{
    public sealed class AnimationRunSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Physic, AnimatorView>.Exclude<StartShooting, Death> _animator = null;
        private readonly EcsFilter<Navigation, AnimatorView>.Exclude<StartShooting, Death> _animatorEnemy = null;
        private readonly EcsFilter<AnimatorView, StartShooting>.Exclude<Death> _shoot = null;
        private readonly EcsFilter<AnimatorView, Death> _death = null;

        private const string isStanding = "IsStanding";
        private const string Attack = "Attack";
        private const string Death = "Die";

        public void Run()
        {
            foreach (var index in _animator)
            {
                ref var rigidbody = ref _animator.Get1(index).Value;
                ref var animator = ref _animator.Get2(index).Value;

                if (rigidbody.velocity.sqrMagnitude > 0.1f)
                {
                    if(_animator.GetEntity(index).Has<Delay>())
                        continue;
                    animator.SetBool(isStanding, false);
                }
                else animator.SetBool(isStanding, true);
            }

            foreach (var adx in _animatorEnemy)
            {
                ref var navigation = ref _animatorEnemy.Get1(adx).Value;
                ref var animator = ref _animatorEnemy.Get2(adx).Value;
                if (navigation.isStopped)
                {
                    animator.SetBool(isStanding, true);
                }
                else
                {
                    if(_animatorEnemy.GetEntity(adx).Has<Delay>())
                        continue;
                    animator.SetBool(isStanding, false);

                }
            }

            foreach (var sdx in _shoot)
            {
                ref var animator = ref _shoot.Get1(sdx).Value;
                if (!_shoot.IsEmpty())
                {
                    animator.SetTrigger(Attack);
                    _shoot.GetEntity(sdx).Get<Delay>().Value = Random.Range(1f,3f);
                }
            }

            foreach (var ddx in _death)
            {
                ref var animator = ref _death.Get1(ddx).Value;
                animator.SetTrigger(Death);
                _death.GetEntity(ddx).Del<AnimatorView>();
            }
        }
    }
}