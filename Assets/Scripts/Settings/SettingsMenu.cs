using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Toggle soundToggle;
    public Slider volumeSlider;

    private void Start()
    {
        soundToggle.isOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeMultiplier", 1.0f);
    }

    public void ApplySettings()
    {
        // Сохранение измененных настроек
        PlayerPrefs.SetInt("SoundOn", soundToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("VolumeMultiplier", volumeSlider.value);
        PlayerPrefs.Save();
    }
    public void ApplySettingsButton()
    {
        Debug.Log("ac");
        ApplySettings();
    }
}