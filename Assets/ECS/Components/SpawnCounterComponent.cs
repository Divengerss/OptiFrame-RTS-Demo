using Unity.Entities;

namespace ECS
{
    public struct SpawnCounterComponent : IComponentData
    {
        public int count;
    }
}