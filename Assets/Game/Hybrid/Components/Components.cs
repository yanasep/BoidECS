using Unity.Entities;
using Unity.Mathematics;

namespace Mine.ECS
{
    // フラグ用
    public struct Boid : IComponentData
    {

    }

    public struct Acceleration : IComponentData
    {
        public float3 Value;
    }

    public struct Velocity : IComponentData
    {
        public float3 Value;
    }

    [InternalBufferCapacity(16)]
    public struct NeighborEntitiesBuffer : IBufferElementData
    {
        public Entity Value;
    }
}
