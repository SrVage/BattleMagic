using Code.Abstractions;
using Code.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MonoBehavioursComponent
{
    public abstract class CharacterMonoBehaviour:MonoBehavioursEntity
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Image _imageHP;
        [SerializeField] private Transform _transformHP;
        [SerializeField] private Animator _animator;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            entity.Get<AttackPoint>().Value = _attackPoint;
            entity.Get<Physic>().Value = _rigidbody;
            entity.Get<HPView>().Value = _imageHP;
            entity.Get<HPView>().Transform = _transformHP;
            entity.Get<AnimatorView>().Value = _animator;
        }
    }
}