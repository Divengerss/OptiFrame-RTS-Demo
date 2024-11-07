using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public class PlayerSpawnerAuthoring : MonoBehaviour
    {
        public GameObject LOD0Prefab;
        public GameObject LOD1Prefab;
        public GameObject LOD2Prefab;
        public GameObject LOD3Prefab;
        public float spawnRate;
    }

    public class PlayerSpawnerBaker : Baker<PlayerSpawnerAuthoring>
    {
        public override void Bake(PlayerSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new PlayerSpawnerComponent
            {
                LOD0Prefab = GetEntity(authoring.LOD0Prefab, TransformUsageFlags.Dynamic),
                LOD1Prefab = GetEntity(authoring.LOD1Prefab, TransformUsageFlags.Dynamic),
                LOD2Prefab = GetEntity(authoring.LOD2Prefab, TransformUsageFlags.Dynamic),
                LOD3Prefab = GetEntity(authoring.LOD3Prefab, TransformUsageFlags.Dynamic),
                spawnPos = authoring.transform.position,
                nextSpawnTime = 0.0f,
                spawnRate = authoring.spawnRate
            });

            AddComponent(entity, new SpawnCounterComponent
            {
                count = 0
            });
        }
    }
}
