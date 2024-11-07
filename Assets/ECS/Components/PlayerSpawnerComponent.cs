using Unity.Entities;
using Unity.Mathematics;

namespace ECS
{
    public struct PlayerSpawnerComponent : IComponentData
    {
        public Entity LOD0Prefab; // High detail
        public Entity LOD1Prefab; // Medium detail
        public Entity LOD2Prefab; // Low detail
        public Entity LOD3Prefab; // Lowest detail
        public float3 spawnPos;
        public float nextSpawnTime;
        public float spawnRate;
    }
}
