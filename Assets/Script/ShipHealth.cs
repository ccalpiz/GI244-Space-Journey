using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("UI References")]
    [HideInInspector] public Image[] hpImages;

    void Start()
    {
        if (UIManager.Instance != null)
        {
            hpImages = UIManager.Instance.hpImages;
        }

        currentHealth = Mathf.Clamp(currentHealth <= 0 ? maxHealth : currentHealth, 1, maxHealth);
        UpdateHPUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHPUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
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
        SceneManager.LoadScene("MainMenu");
    }
}
