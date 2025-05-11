using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gameplay")]
    public Transform Player;
    public int score = 0;

    [Header("UI")]
    public Text scoreText;

    [Header("Power-Up Settings")]
    public float speedMultiplier = 2f;
    public float speedBoostDuration = 5f;
    public bool isGameActive = true;

    [Header("Player")]
    public GameObject playerPrefab;
    public Transform spawnPoint;

    private bool isSpeedBoostActive = false;
    private float speedBoostTimer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // 🚨 REMOVE this line:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }



    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        UpdateScoreUI();

        if (Player != null)
        {
            Destroy(Player.gameObject);
        }

        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        Player = newPlayer.transform;

        ObjectSpawner spawner = FindFirstObjectByType<ObjectSpawner>();
        if (spawner != null)
        {
            spawner.ResetSpawner();
        }
    }

    private void Update()
    {
        HandleSpeedBoost();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Mineral Value : " + score.ToString();
        }
    }

    public void ActivateSpeedBoost()
    {
        Debug.Log("[PowerUp] Speed boost activated for " + speedBoostDuration + " seconds!");
        isSpeedBoostActive = true;
        speedBoostTimer = speedBoostDuration;
    }

    private void HandleSpeedBoost()
    {
        if (isSpeedBoostActive)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0f)
            {
                isSpeedBoostActive = false;
                Debug.Log("[PowerUp] Speed boost ended");
            }
        }
    }

    public bool IsSpeedBoostActive()
    {
        return isSpeedBoostActive;
    }

    public void ResetGame()
    {
        isGameActive = false;
        score = 0;
        Player = null;
        isSpeedBoostActive = false;
        speedBoostTimer = 0f;
    }

    public void EndGame()
    {
        ResetGame();
        SceneManager.LoadScene("MainMenu");
    }
}
