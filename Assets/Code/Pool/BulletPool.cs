using System.Collections.Generic;
using Code.Abstractions;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Pool
{
    public sealed class BulletPool:IPool
    {
        private Queue<GameObject> _bullet;
        private readonly int _capacityPool;
        private Transform _rootPool;

        public BulletPool(int capacityPool, GameObject prefab, EcsWorld world)
        {
            _bullet = new Queue<GameObject>();
            _capacityPool = capacityPool;
            if (!_rootPool)
            {
                _rootPool = new GameObject("RootPool").transform;
            }
            for (int i = 0; i < _capacityPool; i++)
            {
                var bullet = GameObject.Instantiate(prefab, new Vector3(0, 0, -2), Quaternion.identity);
                bullet.GetComponent<MonoBehavioursEntity>().Initial(world.NewEntity(), world);
                bullet.transform.SetParent(_rootPool);
                bullet.SetActive(false);
                _bullet.Enqueue(bullet);
            }
            Debug.Log(_bullet.Count);
        }

        public GameObject GetBullet()
        {
            var bullet = _bullet.Dequeue();
            return bullet;
        }

        public void ReturnToPool(GameObject gameObject)
        {
            gameObject.SetActive(false);
            _bullet.Enqueue(gameObject);
            gameObject.transform.position = new Vector3(0, 0, -2);
            gameObject.transform.SetParent(_rootPool);
        }
    }
}