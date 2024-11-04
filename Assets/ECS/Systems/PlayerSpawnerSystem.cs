using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms; // Required for LocalTransform or Translation

namespace ECS
{
    [BurstCompile]
    public partial struct PlayerSpawnerSystem : ISystem
    {
        private static float3 CalculateGridPosition(int index, float3 startPos, int rows, int cols, float cellSize)
        {
            // Calculate row and column based on index
            int row = index / cols;
            int col = index % cols;

            // Calculate x and z based on row and column
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
                // Calculate grid position for the new entity
                float3 startPos = new float3(-190, 0, 180);
                int gridRows = 190;
                int gridCols = 190;
                float cellSize = 2f;

                // Use the counter to determine the position in the grid
                float3 spawnPosition = CalculateGridPosition(counter.ValueRO.count, startPos, gridRows, gridCols, cellSize);

                Entity newEntity = entityCmdBuffer.Instantiate(spawner.ValueRO.prefab);

                // Set the spawn position as the entity's LocalTransform
                entityCmdBuffer.AddComponent(newEntity, new LocalTransform
                {
                    Position = spawnPosition,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });

                entityCmdBuffer.AddComponent(newEntity, new PlayerComponent
                {
                    spawnPosition = spawnPosition,
                    moveDirection = Unity.Mathematics.Random.CreateFromIndex((uint)(SystemAPI.Time.ElapsedTime / SystemAPI.Time.DeltaTime)).NextFloat3(),
                    moveSpeed = 30
                });

                spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;

                counter.ValueRW.count += 1;

                entityCmdBuffer.Playback(state.EntityManager);
            }
        }
    }
}
