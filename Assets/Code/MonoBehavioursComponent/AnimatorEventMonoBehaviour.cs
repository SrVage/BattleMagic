using Code.Components;
using Code.MonoBehavioursComponent;
using Leopotam.Ecs;
using UnityEngine;

public class AnimatorEventMonoBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMonoBehaviour player;
    
    private EcsWorld _world;
    private EcsEntity _entity;
    
    public  void Initial(EcsEntity entity, EcsWorld world)
    {
        _world = world;
        _entity = entity;
    }

    public void Fire()
    {
        var entity =  _world.NewEntity().Get<AnimatorEvent>();
    }
}
