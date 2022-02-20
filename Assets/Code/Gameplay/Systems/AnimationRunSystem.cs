using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public class AnimationRunSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Physic, AnimatorView>.Exclude<StartShooting> _animator;
        private readonly EcsFilter<AnimatorView, StartShooting> _shoot;

        private const string isStanding = "IsStanding";
        private const string isAttack = "Attack";
        
        public void Run()
        {
            foreach (var index in _animator)
            {
                ref var rigidbody = ref _animator.Get1(index).Value;
                ref var animator = ref _animator.Get2(index).Value;

                if (rigidbody.velocity.sqrMagnitude > 0.1f)
                {
                    animator.SetBool(isStanding, false);
                }
                else animator.SetBool(isStanding, true);
            }

            foreach (var sdx in _shoot)
            {
                ref var animator = ref _shoot.Get1(sdx).Value;
                if (!_shoot.IsEmpty())
                {
                    animator.SetTrigger(isAttack);
                }
            }
        }
    }
}