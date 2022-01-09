using UnityEngine;

namespace Code.Components
{
    public enum Fraction
    {
        Player=0,
        Enemy = 1,
    }
    public struct SpawnPoint
    {
        public Fraction Fraction;
        public Vector3 Position;
    }
}