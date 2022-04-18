using Code.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;
using Renderer = Code.Components.Renderer;

namespace Code.MonoBehavioursComponent
{
    public class EnemyMonoBehaviour:CharacterMonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private MeshRenderer _meshRenderer;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            entity.Get<Enemy>();
            entity.Get<Navigation>().Value = _agent;
            entity.Get<Renderer>().Value = _meshRenderer;
            var animEvent = gameObject.GetComponentInChildren<AnimatorEventMonoBehaviour>();
            animEvent.Initial(world, entity);
        }
    }
}