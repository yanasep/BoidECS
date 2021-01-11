using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Mine.MyECS
{
    public class BoidBehaviorSystem : SystemBase
    {
        public override void OnUpdate(float deltaTime)
        {
            var param = Singleton.Instance.Params;
            var boids = Singleton.Instance.Module.GetBoids();

            foreach (var boid in boids)
            {
                //var sepDir = boids
                //    .Where(other => other != boid)
                //    .Select(other => (boid.transform.position - other.transform.position).normalized)
                //    .Average();

                //var avgVel = boids
                //    .Where(other => other != boid)
                //    .Select(other => other.velocity)
                //    .Average();

                //var avgPos = boids
                //    .Where(other => other != boid)
                //    .Select(other => other.transform.position)
                //    .Average();

                Vector3 sepDir = Vector3.zero;
                Vector3 avgVel = Vector3.zero;
                Vector3 avgPos = Vector3.zero;

                foreach (var other in boids)
                {
                    if (other == boid) continue;
                    sepDir += (boid.transform.position - other.transform.position).normalized;
                    avgVel += other.velocity;
                    avgPos += other.transform.position;
                }
                sepDir /= boids.Count();
                avgVel /= boids.Count();
                avgPos /= boids.Count();
                
                if (param.separation)
                    boid.acceleration += sepDir * param.separationForce;
                if (param.alignment)
                    boid.acceleration += avgVel * param.alignmentForce;
                if (param.cohesion)
                    boid.acceleration += (avgPos - boid.transform.position) * param.cohesionForce;
            }
        }

    }

    public static class EnumerableExtensions
    {
        public static Vector3 Sum(this IEnumerable<Vector3> source)
        {
            Vector3 result = Vector3.zero;

            foreach (var item in source)
            {
                result += item;
            }
            return result;
        }

        public static Vector3 Average(this IEnumerable<Vector3> source)
        {
            var sum = source.Sum();
            return sum / source.Count();
        }
    }
}