using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;

    private const float MIN_VOLUME = 0.05f;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        savedVolume = Mathf.Clamp(savedVolume, 0.05f, 1f);

        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }


    public void SetVolume(float rawValue)
    {
        float clampedValue = Mathf.Clamp(rawValue, MIN_VOLUME, 1f);
        float dB = Mathf.Log10(clampedValue) * 20f;

        mixer.SetFloat("MasterVolume", dB);
        PlayerPrefs.SetFloat("Volume", clampedValue);
    }
}
