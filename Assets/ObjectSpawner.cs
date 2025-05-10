using UnityEngine;

public class SpaceObjectSpawner : MonoBehaviour
{
    public GameObject mineralPrefab;
    public GameObject obstaclePrefab;

    public float spawnInterval = 1.5f;
    public float spawnDistance = 25f;
    public float xRange = 8f;
    public float yRange = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomObject), 1f, spawnInterval);
    }

    void SpawnRandomObject()
    {
        if (GameManager.Instance.Player == null) return;

        GameObject prefabToSpawn = Random.value < 0.7f ? mineralPrefab : obstaclePrefab;

        Vector3 spawnPos = GameManager.Instance.Player.position + new Vector3(
            Random.Range(-xRange, xRange),
            Random.Range(-yRange, yRange),
            spawnDistance
        );

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
