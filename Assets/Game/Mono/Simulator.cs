using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Mine.Mono
{
    /// <summary>
    /// Main
    /// </summary>
    public class Simulator : MonoBehaviour
    {
        [SerializeField] int boidCount;
        [SerializeField] float areaExtent;
        [SerializeField] GameObject boidPrefab;

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

            // 子がいたら生成しない
            var objs = GameObject.FindGameObjectsWithTag("Respawn");
            if (objs.Length > 0)
            {
                foreach (var item in objs)
                {
                    boids.Add(item.AddComponent<Boid>());
                }
            }
            else
            {
                for (int i = 0; i < boidCount; i++)
                {
                    var boidObj = Instantiate(boidPrefab, Random.insideUnitSphere * halfExtent, Random.rotation, transform);
                    Boid boid = boidObj.AddComponent<Boid>();
                    boids.Add(boid);
                }
            }
            foreach (var b in boids)
            {
                b.Init();
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