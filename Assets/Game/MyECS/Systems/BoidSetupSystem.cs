using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine.MyECS
{
    public class BoidSetupSystem : SystemBase
    {
        public BoidSetupSystem()
        {
            Singleton.Instance.GameEvent.onSpawnBoids.AddListener(OnSpawnBoids);
        }

        public void OnSpawnBoids(int spawnCount)
        {
            var boidPrefab = Singleton.Instance.Module.GetBoidPrefab();
            var bounds = Singleton.Instance.Module.GetAreaBounds();
            var boids = Singleton.Instance.Module.GetBoids();
            var param = Singleton.Instance.Params;

            for (int i = 0; i < spawnCount; i++)
            {
                float x = Random.Range(bounds.minX, bounds.maxX);
                float y = Random.Range(bounds.minY, bounds.maxY);
                float z = Random.Range(bounds.minZ, bounds.maxZ);
                var boidObj = MonoBehaviour.Instantiate(boidPrefab, new Vector3(x, y, z), Random.rotation);
                var boid = boidObj.AddComponent<Boid>();
                boid.acceleration = Vector3.zero;
                boid.velocity = boid.transform.forward * Mathf.Lerp(param.minSpeed, param.maxSpeed, 0.5f);
                boids.Add(boid);
            }
        }

        public override void OnDestroy()
        {
            Singleton.Instance.GameEvent.onSpawnBoids.RemoveListener(OnSpawnBoids);
        }
    }
}
