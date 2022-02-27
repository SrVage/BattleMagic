using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

public class AnimatorEventMonoBehaviour : MonoBehaviour
{
    private EcsWorld _world;
    private EcsEntity _entity;

    public  void Initial(EcsWorld world, EcsEntity entity)
    {
        _world = world;
        _entity = entity;
    }

    public void Fire()
    {
        _entity.Get<AnimatorEventAttack>();
    }
}
