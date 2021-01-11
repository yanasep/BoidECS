using UnityEngine;

namespace Mine.MyECS
{
    public class MoveSystem : SystemBase
    {
        public override void OnUpdate(float deltaTime)
        {
            var param = Singleton.Instance.Params;

            foreach (var boid in Singleton.Instance.Module.GetBoids())
            {
                boid.velocity += boid.acceleration * deltaTime;
                boid.acceleration = Vector3.zero;
                float magnitude = boid.velocity.magnitude;
                var normalized = boid.velocity / magnitude;
                boid.velocity = normalized * Mathf.Clamp(magnitude, param.minSpeed, param.maxSpeed);
                boid.transform.position += boid.velocity * deltaTime;
                boid.transform.rotation = Quaternion.LookRotation(boid.velocity);
            }
        }
    }
}