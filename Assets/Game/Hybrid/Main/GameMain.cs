using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Mine.ECS
{
    public class GameMain : MonoBehaviour
    {
        [SerializeField] GameObject boidPrefab;
        [SerializeField] int spawnCount;
        [SerializeField] float halfExtent;

        private void Start()
        {
            Singleton.Init();
            AreaBounds bounds = new AreaBounds()
            {
                minX = transform.position.x - halfExtent,
                maxX = transform.position.x + halfExtent,
                minY = transform.position.y - halfExtent,
                maxY = transform.position.y + halfExtent,
                minZ = transform.position.z - halfExtent,
                maxZ = transform.position.z + halfExtent,
            };
            Singleton.Instance.Module.GetAreaBound = () => bounds;

            for (int i = 0; i < spawnCount; i++)
            {
                var boid = Instantiate(boidPrefab);
                boid.AddComponent<BoidComponent>();
                boid.AddComponent<ConvertToEntity>();
            }

            DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(
                World.DefaultGameObjectInjectionWorld,
                new System.Type[]
                {
                    typeof(BoidSetupSystem),
                    typeof(NeighborDetectionSystem),
                    typeof(BoundsSystem),
                    typeof(BoidBehaviorSystem),
                    typeof(MoveSystem)
                }
            );
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
