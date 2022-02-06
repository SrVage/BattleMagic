using Code.Components;
using Leopotam.Ecs;
using UnityEngine;

public class AnimatorEventMonoBehaviour : MonoBehaviour
{
    private EcsWorld _world;

    public  void Initial(EcsWorld world)
    {
        _world = world;
    }

    public void Fire()
    {
        var entity =  _world.NewEntity().Get<AnimatorEvent>();
    }
}
