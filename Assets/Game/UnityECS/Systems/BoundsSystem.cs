using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace Mine.ECS
{
    [DisableAutoCreation]
    public class BoundsSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var bounds = Singleton.Instance.Module.GetAreaBound();
            var param = Singleton.Instance.Params;
            var distance = param.wallReflectionDistance;
            var force = param.wallReflectionForce;

            Entities
                .WithAll<Boid>()
                .ForEach((ref Acceleration acceleration, in Translation translation) =>
            {
                float3 sum = float3.zero;

                sum += new float3(1, 0, 0) * GetReflectionForce(math.abs(translation.Value.x - bounds.minX), force, distance);
                sum += new float3(-1, 0, 0) * GetReflectionForce(math.abs(translation.Value.x - bounds.maxX), force, distance);
                sum += new float3(0, 1, 0) * GetReflectionForce(math.abs(translation.Value.y - bounds.minY), force, distance);
                sum += new float3(0, -1, 0) * GetReflectionForce(math.abs(translation.Value.y - bounds.maxY), force, distance);
                sum += new float3(0, 0, 1) * GetReflectionForce(math.abs(translation.Value.z - bounds.minZ), force, distance);
                sum += new float3(0, 0, -1) * GetReflectionForce(math.abs(translation.Value.z - bounds.maxZ), force, distance);

                acceleration.Value += sum;
            })
                .Run();
        }

        private static float GetReflectionForce(float dist, float force, float distance)
        {
            if (dist > distance)
            {
                return 0;
            }
            float t = inverseLerp(0, distance, dist);
            return math.lerp(force, 0, t);
        }

        private static float inverseLerp(float a, float b, float value)
        {
            return (value - a) / (b - a);
        }
    }
}
