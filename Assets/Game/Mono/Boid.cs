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
            ApplySeparation();
            ApplyAlignment();
            ApplyCohesion();

            Move();
        }

        // 近くの魚を探す
        void FindNeighbors()
        {
            neighbors = Singleton.Instance.Modules.GetBoids()
                .Where(other => (other.transform.position - transform.position).sqrMagnitude < Singleton.Instance.Params.neighborDistance)
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
            float t = Mathf.InverseLerp(0, Singleton.Instance.Params.neighborDistance, dist);
            return Mathf.Lerp(Singleton.Instance.Params.wallReflectionForce, 0, t);
        }

        // 近すぎると離れる
        void ApplySeparation()
        {

        }

        // 周りと同じ向き、速度で進む
        void ApplyAlignment()
        {

        }

        // 集団の重心へ向かう
        void ApplyCohesion()
        {

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
    }
}