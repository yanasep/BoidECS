using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Mine.Mono
{
    /// <summary>
    /// Main
    /// </summary>
    public class Simulator : MonoBehaviour
    {
        [SerializeField] int boidCount;
        [SerializeField] float areaExtent;
        [SerializeField] Boid boidPrefab;

        List<Boid> boids = new List<Boid>();

        public class AreaBound
        {
            public float minX;
            public float maxX;
            public float minY;
            public float maxY;
            public float minZ;
            public float maxZ;
        }
        AreaBound bound;

        private void Start()
        {
            Singleton.Init();
            float halfExtent = areaExtent / 2f;

            for (int i = 0; i < boidCount; i++)
            {
                var boid = Instantiate(boidPrefab, Random.insideUnitSphere * halfExtent, Random.rotation, transform);
                boid.Init();
                boids.Add(boid);
            }
            AreaBound bound = new AreaBound()
            {
                minX = transform.position.x - halfExtent,
                maxX = transform.position.x + halfExtent,
                minY = transform.position.y - halfExtent,
                maxY = transform.position.y + halfExtent,
                minZ = transform.position.z - halfExtent,
                maxZ = transform.position.z + halfExtent,
            };

            Singleton.Instance.Modules.GetBoids = () => boids;
            Singleton.Instance.Modules.GetAreaBound = () => bound;
        }

        private void Update()
        {
            foreach (var boid in boids)
            {
                boid.OnUpdate();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(areaExtent, areaExtent, areaExtent));
        }
    }
}