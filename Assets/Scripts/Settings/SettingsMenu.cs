using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Toggle soundToggle;
    public Slider volumeSlider;
    private AudioSource audioSource;

    private void Start()
    {
        if (soundToggle.TryGetComponent<AudioSource>(out audioSource))
        {
            audioSource.enabled = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        }

        volumeSlider.value = PlayerPrefs.GetFloat("VolumeMultiplier", 1.0f);
    }


    public void ApplySettings()
    {
        PlayerPrefs.SetInt("SoundOn", soundToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("VolumeMultiplier", volumeSlider.value);
        PlayerPrefs.Save();
    }
    public void ApplySettingsButton()
    {
        ApplySettings();
    }
}