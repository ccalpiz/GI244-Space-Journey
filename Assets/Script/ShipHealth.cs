using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("UI References")]
    public Image[] hpImages;

    void Start()
    {
        currentHealth = Mathf.Clamp(currentHealth > 0 ? currentHealth : maxHealth, 1, maxHealth);
        UpdateHPUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        UpdateHPUI();

        if (currentHealth == 0)
        {
            GameOver();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHPUI();
    }

    void UpdateHPUI()
    {
        if (hpImages == null || hpImages.Length == 0) return;

        for (int i = 0; i < hpImages.Length; i++)
        {
            hpImages[i].enabled = i < currentHealth;
        }
    }

    void GameOver()
    {
        Time.timeScale = 1f;
        GameManager.Instance?.ShowGameOver();
    }
}
