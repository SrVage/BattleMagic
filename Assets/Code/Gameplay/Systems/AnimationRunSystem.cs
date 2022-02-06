using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public class AnimationRunSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Physic, AnimatorView> _animator;
        private readonly EcsFilter<StartShooting> _shoot;

        private const string isStanding = "IsStanding";
        private const string isAttack = "IsAttack";
        
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

                if (_shoot.IsEmpty())
                {
                    animator.SetBool(isAttack, false);
                }

                foreach (var j in _shoot)
                {
                    animator.SetBool(isAttack, true);
                }
            }
        }
    }
}