using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using System.Diagnostics;


[BurstCompile]
public partial struct SphereSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;

        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

        foreach (Entity entity in entities)
        {
            if (entityManager.HasComponent<SphereComponent>(entity))
            {
                SphereComponent sphere = entityManager.GetComponentData<SphereComponent>(entity);
                LocalTransform localTransform = entityManager.GetComponentData<LocalTransform>(entity);

                float3 moveDirection = sphere.moveDirection * SystemAPI.Time.DeltaTime * sphere.moveSpeed;

                localTransform.Position = localTransform.Position + moveDirection;
                entityManager.SetComponentData<LocalTransform>(entity, localTransform);

                if (localTransform.Position.y >= 0) {
                    if (sphere.moveSpeed > 0) {
                        sphere.moveSpeed -= 1 * SystemAPI.Time.DeltaTime;
                        sphere.moveDirection.y -= 1 * SystemAPI.Time.DeltaTime;
                    } else {
                        sphere.moveSpeed = 0;
                        sphere.moveDirection.y = 0;
                    }
                } else {
                    if (sphere.moveSpeed > 0)
                        localTransform.Position.y = 0;
                    sphere.moveDirection.y = -sphere.moveDirection.y;
                }
                entityManager.SetComponentData<SphereComponent>(entity, sphere);
            }
        }
    }
}
