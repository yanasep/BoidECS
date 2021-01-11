using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

namespace Mine.Mono
{
    /// <summary>
    /// 魚
    /// </summary>
    public class Boid : MonoBehaviour
    {
        public Vector3 Velocity { get; private set; }
        IEnumerable<Boid> neighbors;
        Vector3 acceleration;

        public void Init()
        {
            Velocity = transform.forward * Mathf.Lerp(Singleton.Instance.Params.minSpeed, Singleton.Instance.Params.maxSpeed, 0.5f);
        }

        public void OnUpdate()
        {
            FindNeighbors();

            acceleration = Vector3.zero;
            CheckWalls();
            if (Singleton.Instance.Params.separation)
                ApplySeparation();
            if (Singleton.Instance.Params.alignment)
                ApplyAlignment();
            if (Singleton.Instance.Params.cohesion)
                ApplyCohesion();

            Move();
        }

        // 近くの魚を探す: 距離と向きが近いもの
        void FindNeighbors()
        {
            neighbors = Singleton.Instance.Modules.GetBoids()
                .Where(other => other != this)
                .Where(other => (other.transform.position - transform.position).sqrMagnitude < Singleton.Instance.Params.neighborSqrDistance)
                .Where(other => Vector3.Dot(other.transform.forward, transform.forward) > Singleton.Instance.Params.neighborRotationDot)
                .ToArray();
        }

        // 壁で反射
        void CheckWalls()
        {
            var bound = Singleton.Instance.Modules.GetAreaBound();
            acceleration +=
                Vector3.left * CalculateReflectionForce(bound.maxX - transform.position.x)
                + Vector3.right * CalculateReflectionForce(transform.position.x - bound.minX)
                + Vector3.down * CalculateReflectionForce(bound.maxY - transform.position.y)
                + Vector3.up * CalculateReflectionForce(transform.position.y - bound.minY)
                + Vector3.back * CalculateReflectionForce(bound.maxZ - transform.position.z)
                + Vector3.forward * CalculateReflectionForce(transform.position.z - bound.minZ);
        }

        public static float CalculateReflectionForce(float dist)
        {
            float t = Mathf.InverseLerp(0, Singleton.Instance.Params.wallReflectionDistance, dist);
            return Mathf.Lerp(Singleton.Instance.Params.wallReflectionForce, 0, t);
        }

        // 近すぎると離れる
        void ApplySeparation()
        {
            if (neighbors.Count() == 0) return;
            var list = neighbors.Select(other => (transform.position - other.transform.position).normalized);
            Vector3 dir = new Vector3(list.Average(v => v.x), list.Average(v => v.y), list.Average(v => v.z));
            acceleration += dir * Singleton.Instance.Params.separationForce;
        }

        // 周りと同じ向き、速度で進む
        void ApplyAlignment()
        {
            if (neighbors.Count() == 0) return;
            var list = neighbors.Select(other => other.Velocity);
            Vector3 dir = new Vector3(list.Average(v => v.x), list.Average(v => v.y), list.Average(v => v.z));
            acceleration += dir * Singleton.Instance.Params.alignmentForce;
        }

        // 集団の重心へ向かう
        void ApplyCohesion()
        {
            if (neighbors.Count() == 0) return;
            var list = neighbors.Select(other => other.transform.position);
            Vector3 avgPos = new Vector3(list.Average(v => v.x), list.Average(v => v.y), list.Average(v => v.z));
            acceleration += (avgPos - transform.position) * Singleton.Instance.Params.cohesionForce;
        }

        void Move()
        {
            Velocity += acceleration * Time.deltaTime;
            float magnitude = Velocity.magnitude;
            var normalized = Velocity / magnitude;
            Velocity = normalized * Mathf.Clamp(magnitude, Singleton.Instance.Params.minSpeed, Singleton.Instance.Params.maxSpeed);
            transform.position += Velocity * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(Velocity);
        }

        private void OnDrawGizmosSelected()
        {
            if (neighbors == null) return;

            Gizmos.color = Color.green;
            foreach (var b in neighbors)
            {
                Gizmos.DrawWireCube(b.transform.position, b.transform.localScale);
            }
        }
    }
}