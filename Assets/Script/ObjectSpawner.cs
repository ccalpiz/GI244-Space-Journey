using UnityEngine;

public class ObjectSpawner : MonoBehaviour
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

    private float spawnTimer = 0f;

    void Update()
    {
        if (GameManager.Instance?.Player == null || !GameManager.Instance.isGameActive)
            return;

        float interval = GameManager.Instance.IsSpeedBoostActive() ? boostedSpawnInterval : normalSpawnInterval;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= interval)
        {
            SpawnRandomObject();
            spawnTimer = 0f;
        }
    }

    void SpawnRandomObject()
    {
        float roll = Random.value;
        GameObject prefabToSpawn = roll switch
        {
            < 0.4f => mineralPrefab,
            < 0.7f => obstaclePrefab,
            < 0.85f => speedBoostPrefab,
            _ => healPrefab
        };

        Vector3 spawnPos = GameManager.Instance.Player.position + new Vector3(
            Random.Range(-xRange, xRange),
            Random.Range(-yRange, yRange),
            spawnDistance
        );

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }

    public void ResetSpawner()
    {
        spawnTimer = 0f;
    }
}
