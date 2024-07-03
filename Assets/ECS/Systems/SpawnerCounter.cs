using UnityEngine;
using UnityEngine.UI;
using Unity.Entities;

public class SpawnCounterDisplay : MonoBehaviour
{
    public Text counterText;
    private EntityManager entityManager;

    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    void Update()
    {
        // if (SystemAPI.TryGetSingletonEntity<SpawnCounterComponent>(out Entity spawnEntity))
        // {
        //     var counter = entityManager.GetComponentData<SpawnCounterComponent>(spawnEntity);
        //     counterText.text = "Spawned Entities: " + counter.count;
        // }
    }
}
