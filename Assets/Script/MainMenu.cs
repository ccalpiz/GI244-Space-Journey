using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject optionsPanel;
    public Button startButton;
    public Button settingsButton;

    void Start()
    {
        Debug.Log("[MainMenu] Start() called on: " + gameObject.name);
        Debug.Log("[MainMenu] Time.timeScale = " + Time.timeScale);

        if (startButton == null || settingsButton == null)
        {
            Debug.LogError("[MainMenu] Buttons are not assigned in Inspector!");
        }
        else
        {
            Debug.Log("[MainMenu] Buttons are assigned. Reconnecting events...");
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(StartGame);

            settingsButton.onClick.RemoveAllListeners();
            settingsButton.onClick.AddListener(OpenOptions);
        }
    }

    public void StartGame()
    {
        Debug.Log("[MainMenu] StartGame() called");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame();
        }

        SceneManager.LoadScene("MainGame");
    }

    public void OpenOptions()
    {
        Debug.Log("[MainMenu] OpenOptions() called");

        if (optionsPanel != null)
            optionsPanel.SetActive(true);
        else
            Debug.LogWarning("[MainMenu] optionsPanel not assigned!");
    }

    public void CloseOptions()
    {
        Debug.Log("[MainMenu] CloseOptions() called");

        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }
}
