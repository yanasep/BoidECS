using UnityEngine;
using System.Collections;

namespace Mine.Mono
{
    [CreateAssetMenu(menuName = "Scriptable Object/Params")]
    public class Params : ScriptableObject
    {
        public float neighborDistance;
        public float minSpeed;
        public float maxSpeed;
        public float wallReflectionDistance;
        public float wallReflectionForce;
    }
}