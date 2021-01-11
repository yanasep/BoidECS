using UnityEngine;
using System.Collections;

namespace Mine.MyECS
{
    public class BoundsSystem : SystemBase
    {
        public override void OnUpdate(float deltaTime)
        {
            var bounds = Singleton.Instance.Module.GetAreaBounds();

            foreach (var boid in Singleton.Instance.Module.GetBoids())
            {
                boid.acceleration +=
                    Vector3.left * GetReflectionForce(Mathf.Abs(boid.transform.position.x - bounds.maxX))
                    + Vector3.right * GetReflectionForce(Mathf.Abs(boid.transform.position.x - bounds.minX))
                    + Vector3.down * GetReflectionForce(Mathf.Abs(boid.transform.position.y - bounds.maxY))
                    + Vector3.up * GetReflectionForce(Mathf.Abs(boid.transform.position.y - bounds.minY))
                    + Vector3.back * GetReflectionForce(Mathf.Abs(boid.transform.position.z - bounds.maxZ))
                    + Vector3.forward * GetReflectionForce(Mathf.Abs(boid.transform.position.z - bounds.minZ));
            }
        }

        public static float GetReflectionForce(float dist)
        {
            float t = Mathf.InverseLerp(0, Singleton.Instance.Params.wallReflectionDistance, dist);
            return Mathf.Lerp(Singleton.Instance.Params.wallReflectionForce, 0, t);
        }
    }
}