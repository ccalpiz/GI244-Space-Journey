using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void SetVolume(float rawValue)
    {
        PlayerPrefs.SetFloat("Volume", rawValue);
        float clampedValue = Mathf.Clamp(rawValue, 0.0001f, 1f);
        float dB = Mathf.Log10(clampedValue) * 20f;
        mixer.SetFloat("MasterVolume", dB);
    }
}
