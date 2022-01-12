using Code.Components;
using Leopotam.Ecs;

namespace Code.MonoBehavioursComponent
{
    public class EnemyMonoBehaviour:CharacterMonoBehaviour
    {
        public override void Initial(EcsEntity entity, EcsWorld world)
        {
            base.Initial(entity, world);
            entity.Get<Enemy>();
        }
    }
}