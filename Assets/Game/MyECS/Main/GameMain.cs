using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine.MyECS
{
    public class GameMain : MonoBehaviour
    {
        [SerializeField] GameObject boidPrefab;
        [SerializeField] int spawnCount;
        [SerializeField] float halfExtent;

        List<SystemBase> systems;
        List<Boid> boids;
        AreaBounds bounds;

        private void Start()
        {
            Singleton.Init();
            
            bounds = new AreaBounds()
            {
                minX = transform.position.x - halfExtent,
                maxX = transform.position.x + halfExtent,
                minY = transform.position.y - halfExtent,
                maxY = transform.position.y + halfExtent,
                minZ = transform.position.z - halfExtent,
                maxZ = transform.position.z + halfExtent,
            };
            boids = new List<Boid>();

            Singleton.Instance.Module.GetAreaBounds = () => bounds;
            Singleton.Instance.Module.GetBoids = () => boids;
            Singleton.Instance.Module.GetBoidPrefab = () => boidPrefab;

            systems = new List<SystemBase>()
            {
                new BoidSetupSystem(),
                new BoidBehaviorSystem(),
                new BoundsSystem(),
                new MoveSystem()
            };

            Singleton.Instance.GameEvent.onSpawnBoids.Invoke(spawnCount);
        }

        private void Update()
        {
            foreach (var sys in systems)
            {
                sys.OnUpdate(Time.deltaTime);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, Vector3.one * halfExtent * 2);
        }
    }

    public struct AreaBounds
    {
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
        public float minZ;
        public float maxZ;
    }
}
