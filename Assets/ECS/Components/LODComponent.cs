using Unity.Entities;

namespace ECS
{
    public struct LODComponent : IComponentData
    {
        public Entity LOD0Prefab; // High detail
        public Entity LOD1Prefab; // Medium detail
        public Entity LOD2Prefab; // Low detail
        public Entity LOD3Prefab; // Low detail
        public float LOD1Distance; // Distance to switch to LOD1
        public float LOD2Distance; // Distance to switch to LOD2
        public float LOD3Distance; // Distance to switch to LOD2
    }
}
