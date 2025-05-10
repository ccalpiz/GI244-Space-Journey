using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Image[] hpImages; 

    void Start()
    {
        currentHealth = maxHealth;
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
