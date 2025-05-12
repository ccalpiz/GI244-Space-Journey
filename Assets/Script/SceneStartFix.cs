using UnityEngine;

public class SceneStartFix : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
        Debug.Log("[SceneStartFix] Time.timeScale reset to 1");
    }
}
