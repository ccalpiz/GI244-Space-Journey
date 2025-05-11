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
        if (currentHealth <= 0 || currentHealth > maxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth, 1, maxHealth);
        }

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
        Debug.Log("[Heal] Healed to " + currentHealth);
        UpdateHPUI();
    }

    void UpdateHPUI()
    {
        for (int i = 0; i < hpImages.Length; i++)
        {
            hpImages[i].enabled = i < currentHealth;
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
