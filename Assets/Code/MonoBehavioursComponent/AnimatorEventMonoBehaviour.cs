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
        _world.NewEntity().Get<AnimatorEvent>();
    }
}
