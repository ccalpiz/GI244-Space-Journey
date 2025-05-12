using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gameplay")]
    public Transform Player;
    public int score = 0;

    [Header("UI")]
    public Text scoreText;
    public Image[] hpImages;

    [Header("Power-Up Settings")]
    public float speedMultiplier = 2f;
    public float speedBoostDuration = 5f;

    [Header("Player")]
    public GameObject playerPrefab;
    public Transform spawnPoint;

    private bool isSpeedBoostActive = false;
    private float speedBoostTimer = 0f;
    public bool isGameActive = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (isSpeedBoostActive)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0f)
                isSpeedBoostActive = false;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        isGameActive = true;
        score = 0;
        UpdateScoreUI();

        // Safely destroy only scene-instance player
        if (Player != null && Player.gameObject.scene.IsValid())
        {
            Destroy(Player.gameObject);
        }

        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        Player = newPlayer.transform;

        var shipHealth = newPlayer.GetComponent<ShipHealth>();
        if (shipHealth != null && hpImages != null)
        {
            shipHealth.hpImages = hpImages;
        }

        FindFirstObjectByType<ObjectSpawner>()?.ResetSpawner();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Mineral Value : {score}";
    }

    public void ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        speedBoostTimer = speedBoostDuration;
    }

    public bool IsSpeedBoostActive() => isSpeedBoostActive;

    public void EndGame()
    {
        isGameActive = false;

        if (Player != null && Player.gameObject.scene.IsValid())
        {
            Destroy(Player.gameObject);
        }

        Player = null;
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        isGameActive = false;
        score = 0;
        Player = null;
        isSpeedBoostActive = false;
        speedBoostTimer = 0f;
    }
}
