using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Mine.ECS
{
    [DisableAutoCreation]
    public class BoidSetupSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var random = new Unity.Mathematics.Random(1);
            var bound = Singleton.Instance.Module.GetAreaBound();

            Entities
                .WithAll<Boid>()
                .WithNone<Acceleration, Velocity>()
                .ForEach((Entity e, ref Translation translation, ref Rotation rotation) =>
                {
                    float x = random.NextFloat(bound.minX, bound.maxX);
                    float y = random.NextFloat(bound.minY, bound.maxY);
                    float z = random.NextFloat(bound.minZ, bound.maxZ);
                    translation.Value = new float3(x, y, z);
                    rotation.Value = random.NextQuaternionRotation();
                    float3 initialSpeed = math.forward(rotation.Value) * math.lerp(Singleton.Instance.Params.minSpeed, Singleton.Instance.Params.maxSpeed, 0.5f);

                    manager.AddComponentData(e, new Acceleration() { Value = 0 });
                    manager.AddComponentData(e, new Velocity() { Value = initialSpeed });
                    manager.AddBuffer<NeighborEntitiesBuffer>(e);
                })
                .WithStructuralChanges()
                .WithoutBurst()
                .Run();
        }
    }
}
