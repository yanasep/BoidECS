using UnityEngine;
using System.Collections;

namespace Mine
{
    [CreateAssetMenu(menuName = "Scriptable Object/Params")]
    public class Params : ScriptableObject
    {
        public float neighborSqrDistance;
        [Range(-1, 1)] public float neighborRotationDot;
        public float minSpeed;
        public float maxSpeed;
        public float wallReflectionDistance;

        public float wallReflectionForce;
        public float separationForce;
        public float alignmentForce;
        public float cohesionForce;

        public bool separation = true;
        public bool alignment = true;
        public bool cohesion = true;
    }
}