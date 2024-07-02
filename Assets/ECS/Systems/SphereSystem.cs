using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;


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

                // if (sphere.moveSpeed > 0)
                // {
                // }
                sphere.moveSpeed -= 1 * SystemAPI.Time.DeltaTime;
                sphere.moveDirection.y -= 1 * SystemAPI.Time.DeltaTime;
                // if (sphere.moveDirection.y > 0)
                // {
                // } else {
                //     // sphere.moveSpeed = 0
                // }
                entityManager.SetComponentData<SphereComponent>(entity, sphere);
            }
        }
    }
}
