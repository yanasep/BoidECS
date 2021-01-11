using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

namespace Mine.ECS
{
    [DisableAutoCreation]
    public class NeighborDetectionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var boidQuery = GetEntityQuery(ComponentType.ReadOnly<Boid>(), ComponentType.ReadOnly<Translation>(),
                                        ComponentType.ReadOnly<Rotation>());
            var boids = boidQuery.ToEntityArray(Allocator.TempJob);
            var param = Singleton.Instance.Params;
            var neighborSqrDistance = param.neighborSqrDistance;
            var neighborRotationDot = param.neighborRotationDot;

            Entities
                .WithAll<Boid>()
                .ForEach((Entity e, ref Translation translation, ref Rotation rotation) =>
            {
                var neighbors = manager.GetBuffer<NeighborEntitiesBuffer>(e);
                for (int i = 0, max = boids.Length; i < max; i++)
                {
                    var item = boids[i];
                    if (e.Index == item.Index && e.Version == item.Version) continue;

                    var otherTrans = manager.GetComponentData<Translation>(item);
                    var otherRot = manager.GetComponentData<Rotation>(item);
                    if (math.lengthsq(translation.Value - otherTrans.Value) > neighborSqrDistance) continue;
                    if (math.dot(rotation.Value, otherRot.Value) < neighborRotationDot) continue;
                    neighbors.Add(new NeighborEntitiesBuffer { Value = item });
                }
            })
                .Run();
            boids.Dispose();
        }
    }
}
