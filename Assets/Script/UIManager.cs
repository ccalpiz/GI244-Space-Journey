using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Image[] hpImages;

    void Awake()
    {
        Instance = this;
    }
}
