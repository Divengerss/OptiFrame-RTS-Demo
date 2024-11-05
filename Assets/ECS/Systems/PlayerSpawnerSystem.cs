using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS
{
    [BurstCompile]
    public partial struct PlayerSpawnerSystem : ISystem
    {
        private static float3 CalculateGridPosition(int index, float3 startPos, int rows, int cols, float cellSize)
        {
            int row = index / cols;
            int col = index % cols;
            float x = startPos.x + col * cellSize;
            float z = startPos.z - row * cellSize;
            return new float3(x, startPos.y, z);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (!SystemAPI.TryGetSingletonEntity<PlayerSpawnerComponent>(out Entity spawnEntity))
            {
                return;
            }

            RefRW<PlayerSpawnerComponent> spawner = SystemAPI.GetComponentRW<PlayerSpawnerComponent>(spawnEntity);
            RefRW<SpawnCounterComponent> counter = SystemAPI.GetComponentRW<SpawnCounterComponent>(spawnEntity);

            EntityCommandBuffer entityCmdBuffer = new EntityCommandBuffer(Allocator.Temp);

            if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                float3 startPos = new float3(-190, 0, 180);
                int gridRows = 10;
                int gridCols = 10;
                float cellSize = 2f;
                float3 spawnPosition = CalculateGridPosition(counter.ValueRO.count, startPos, gridRows, gridCols, cellSize);

                Entity newEntity = entityCmdBuffer.Instantiate(spawner.ValueRO.LOD0Prefab);

                entityCmdBuffer.AddComponent(newEntity, new LocalTransform
                {
                    Position = spawnPosition,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });

                entityCmdBuffer.AddComponent(newEntity, new PlayerComponent
                {
                    spawnPosition = spawnPosition,
                    moveDirection = Random.CreateFromIndex((uint)(SystemAPI.Time.ElapsedTime / SystemAPI.Time.DeltaTime)).NextFloat3(),
                    moveSpeed = 30,
                    currentLOD = spawner.ValueRO.LOD0Prefab
                });

                entityCmdBuffer.AddComponent(newEntity, new LODComponent
                {
                    LOD0Prefab = spawner.ValueRO.LOD0Prefab,
                    LOD1Prefab = spawner.ValueRO.LOD1Prefab,
                    LOD2Prefab = spawner.ValueRO.LOD2Prefab,
                    LOD3Prefab = spawner.ValueRO.LOD3Prefab,
                    LOD1Distance = 50f,
                    LOD2Distance = 100f,
                    LOD3Distance = 200f
                });

                spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;
                counter.ValueRW.count += 1;

                entityCmdBuffer.Playback(state.EntityManager);
            }
        }
    }
}
