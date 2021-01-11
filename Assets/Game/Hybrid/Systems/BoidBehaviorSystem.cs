using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Mine.ECS
{
    [DisableAutoCreation]
    public class BoidBehaviorSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var param = Singleton.Instance.Params;
            var separationForce = param.separationForce;
            var alignmentForce = param.alignmentForce;
            var cohesionForce = param.cohesionForce;
            bool separation = param.separation;
            bool alignment = param.alignment;
            bool cohesion = param.cohesion;

            Entities
                .WithAll<Boid>()
                .ForEach((Entity e, ref Acceleration acceleration, in Translation translation) =>
                {
                    var neighbors = manager.GetBuffer<NeighborEntitiesBuffer>(e);
                    if (neighbors.Length == 0) return;

                    float3 separationDir = float3.zero;
                    float3 avgVel = float3.zero;
                    float3 avgPos = float3.zero;

                    for (int i = 0, max = neighbors.Length; i < max; i++)
                    {
                        var other = neighbors[i];
                        var otherTrans = manager.GetComponentData<Translation>(other.Value);
                        var otherVel = manager.GetComponentData<Velocity>(other.Value);
                        separationDir += math.normalize(translation.Value - otherTrans.Value);
                        avgVel += otherVel.Value;
                        avgPos += otherTrans.Value;
                    }
                    separationDir /= neighbors.Length;
                    avgVel /= neighbors.Length;
                    avgPos /= neighbors.Length;

                    if (separation)
                        acceleration.Value += separationDir * separationForce;
                    if (alignment)
                        acceleration.Value += avgVel * alignmentForce;
                    if (cohesion)
                        acceleration.Value += (avgPos - translation.Value) * cohesionForce;
                })
                .Run();
        }
    }
}
