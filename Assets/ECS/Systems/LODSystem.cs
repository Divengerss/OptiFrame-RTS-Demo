using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;
using Unity.Jobs;

namespace ECS
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class LODSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            // Get the main camera position
            float3 cameraPosition = Camera.main.transform.position;

            // Create an EntityCommandBuffer to handle structural changes
            var ecb = new EntityCommandBuffer(Allocator.TempJob);

            // Create a job handle to track dependencies
            JobHandle jobHandle = Entities
                .ForEach((Entity entity, in LODComponent lodComponent, in LocalToWorld localToWorld, in PlayerComponent player) =>
                {
                    // Calculate distance to camera
                    float distance = math.distance(cameraPosition, localToWorld.Position);

                    // Determine the desired LOD based on distance
                    Entity desiredLOD;
                    if (distance < lodComponent.LOD1Distance)
                    {
                        desiredLOD = lodComponent.LOD0Prefab; // High detail
                    }
                    else if (distance < lodComponent.LOD2Distance)
                    {
                        desiredLOD = lodComponent.LOD1Prefab; // Medium detail
                    }
                    else if (distance < lodComponent.LOD3Distance)
                    {
                        desiredLOD = lodComponent.LOD2Prefab; // Low detail
                    }
                    else
                    {
                        desiredLOD = lodComponent.LOD3Prefab; // Very low detail
                    }

                    // Check if the current LOD is different from the desired LOD
                    if (desiredLOD != player.currentLOD)
                    {
                        // Update the PlayerComponent directly using the command buffer
                        ecb.SetComponent(entity, new PlayerComponent
                        {
                            spawnPosition = player.spawnPosition,
                            moveDirection = player.moveDirection,
                            moveSpeed = player.moveSpeed,
                            currentLOD = desiredLOD
                        });
                    }

                }).Schedule(Dependency);

            // Playback the EntityCommandBuffer after all jobs are done
            jobHandle.Complete();
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}
