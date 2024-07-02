using Unity.Entities;
using Unity.Mathematics;

public struct SphereComponent : IComponentData
{
    public float3 moveDirection;
    public float moveSpeed;
}
