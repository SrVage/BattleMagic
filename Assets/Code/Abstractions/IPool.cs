using UnityEngine;

namespace Code.Abstractions
{
    public interface IPool
    {
        public GameObject GetBullet();
        public void ReturnToPool(GameObject bullet);
    }
}