using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct PlayerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // EntityManager entityManager = state.EntityManager;

        // NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

        // foreach (Entity entity in entities)
        // {
        //     if (entityManager.HasComponent<PlayerComponent>(entity))
        //     {
        //         PlayerComponent playerComponent = entityManager.GetComponentData<PlayerComponent>(entity);
        //         LocalTransform localTransform = entityManager.GetComponentData<LocalTransform>(entity);

        //         float3 moveDirection = playerComponent.moveDirection * SystemAPI.Time.DeltaTime * playerComponent.moveSpeed;

        //         localTransform.Position = localTransform.Position + moveDirection;
        //         entityManager.SetComponentData<LocalTransform>(entity, localTransform);

        //         if (localTransform.Position.y >= 0) {
        //             if (playerComponent.moveSpeed > 0) {
        //                 playerComponent.moveSpeed -= 1 * SystemAPI.Time.DeltaTime;
        //                 playerComponent.moveDirection.y -= 1 * SystemAPI.Time.DeltaTime;
        //             } else {
        //                 playerComponent.moveSpeed = 0;
        //                 playerComponent.moveDirection.y = 0;
        //             }
        //         } else {
        //             if (playerComponent.moveSpeed > 0)
        //                 localTransform.Position.y = 0;
        //             playerComponent.moveDirection.y = -playerComponent.moveDirection.y;
        //         }
        //         entityManager.SetComponentData<PlayerComponent>(entity, playerComponent);
        //     }
        // }
    }
}
