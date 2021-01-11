using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Mine.ECS
{
    [DisableAutoCreation]
    public class MoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var param = Singleton.Instance.Params;
            var minSpeed = param.minSpeed;
            var maxSpeed = param.maxSpeed;
            var deltaTime = Time.DeltaTime;

            Entities
                .WithAll<Boid>()
                .ForEach((ref Translation translation, ref Acceleration acceleration, ref Rotation rotation, ref Velocity velocity) =>
            {
                velocity.Value += acceleration.Value * deltaTime;
                acceleration.Value = float3.zero;

                float magnitude = math.length(velocity.Value);
                var normalized = velocity.Value / magnitude;
                velocity.Value = normalized * math.clamp(magnitude, minSpeed, maxSpeed);
                translation.Value += velocity.Value * deltaTime;
                rotation.Value = quaternion.LookRotationSafe(velocity.Value, new float3(0, 1, 0));
            }).Run();
        }
    }
}
