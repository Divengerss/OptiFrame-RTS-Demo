using Unity.Entities;
using Unity.Mathematics;

public struct PlayerComponent : IComponentData
{
    public float3 spawnPosition;
    public float3 moveDirection;
    public float moveSpeed;
}
