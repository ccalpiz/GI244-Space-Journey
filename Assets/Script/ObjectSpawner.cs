using UnityEngine;

public class SpaceObjectSpawner : MonoBehaviour
{
    [Header("Object Prefab")]
    public GameObject mineralPrefab;
    public GameObject obstaclePrefab;

    [Header("Power-Up Prefabs")]
    public GameObject speedBoostPrefab;
    public GameObject healPrefab;

    [Header("Spawn Settings")]
    public float normalSpawnInterval = 1.5f;
    public float boostedSpawnInterval = 0.75f;
    public float spawnDistance = 25f;
    public float xRange = 8f;
    public float yRange = 5f;

    private float currentSpawnInterval;
    private float spawnTimer = 0f;

    void Start()
    {
        currentSpawnInterval = normalSpawnInterval;
    }

    void Update()
    {
        // Adjust spawn interval based on speed boost
        currentSpawnInterval = GameManager.Instance.IsSpeedBoostActive() ? boostedSpawnInterval : normalSpawnInterval;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval)
        {
            SpawnRandomObject();
            spawnTimer = 0f;
        }
    }

    void SpawnRandomObject()
    {
        if (GameManager.Instance.Player == null) return;

        float roll = Random.value;
        GameObject prefabToSpawn;

        if (roll < 0.4f)
        {
            prefabToSpawn = mineralPrefab;  // 40% 
        }
        else if (roll < 0.7f)
        {
            prefabToSpawn = obstaclePrefab; // 30% 
        }
        else if (roll < 0.85f)
        {
            prefabToSpawn = speedBoostPrefab; // 15% 
        }
        else
        {
            prefabToSpawn = healPrefab; // 15% 
        }

        Vector3 spawnPos = GameManager.Instance.Player.position + new Vector3(
            Random.Range(-xRange, xRange),
            Random.Range(-yRange, yRange),
            spawnDistance
        );

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
