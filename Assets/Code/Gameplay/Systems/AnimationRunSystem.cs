using Code.Components;
using Leopotam.Ecs;

namespace Code.Gameplay.Systems
{
    public sealed class AnimationRunSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Physic, AnimatorView>.Exclude<StartShooting, Death> _animator = null;
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
                    animator.SetBool(isStanding, false);
                }
                else animator.SetBool(isStanding, true);
            }

            foreach (var sdx in _shoot)
            {
                ref var animator = ref _shoot.Get1(sdx).Value;
                if (!_shoot.IsEmpty())
                {
                    animator.SetTrigger(Attack);
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