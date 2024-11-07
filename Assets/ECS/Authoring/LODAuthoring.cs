using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public class LODAuthoring : MonoBehaviour
    {
        public GameObject LOD0Prefab;
        public GameObject LOD1Prefab;
        public GameObject LOD2Prefab;
        public float LOD1Distance = 50f;
        public float LOD2Distance = 100f;
    }

    public class LODBaker : Baker<LODAuthoring>
    {
        public override void Bake(LODAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new LODComponent
            {
                LOD0Prefab = GetEntity(authoring.LOD0Prefab, TransformUsageFlags.Dynamic),
                LOD1Prefab = GetEntity(authoring.LOD1Prefab, TransformUsageFlags.Dynamic),
                LOD2Prefab = GetEntity(authoring.LOD2Prefab, TransformUsageFlags.Dynamic),
                LOD1Distance = authoring.LOD1Distance,
                LOD2Distance = authoring.LOD2Distance
            });
        }
    }
}
