using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance?.StartGame();
    }
}
