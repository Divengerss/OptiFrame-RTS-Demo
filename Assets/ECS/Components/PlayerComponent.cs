using Unity.Entities;
using Unity.Mathematics;

namespace ECS
{
    public struct PlayerComponent : IComponentData
    {
        public float3 spawnPosition; // Position to spawn
        public float3 moveDirection; // Direction for movement
        public float moveSpeed; // Speed of movement
        public Entity currentLOD; // Current LOD prefab
    }
}
