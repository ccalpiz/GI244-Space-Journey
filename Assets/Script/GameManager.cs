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

    [Header("Power-Up Settings")]
    public float speedMultiplier = 2f;
    public float speedBoostDuration = 5f;

    private bool isSpeedBoostActive = true;
    private float speedBoostTimer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
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
}
