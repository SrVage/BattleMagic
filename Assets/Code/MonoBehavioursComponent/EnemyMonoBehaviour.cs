using Code.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Code.MonoBehavioursComponent
{
    public class EnemyMonoBehaviour:CharacterMonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            entity.Get<Enemy>();
            entity.Get<Navigation>().Value = _agent;
        }
    }
}