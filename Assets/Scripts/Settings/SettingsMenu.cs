using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    private AudioSource audioSource;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeMultiplier", 1.0f);

        // Получаем компонент AudioSource или создаем его, если не существует
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        float volumeMultiplier = PlayerPrefs.GetFloat("VolumeMultiplier", 1.0f);
        audioSource.enabled = volumeMultiplier <= 0 ? false : true;
    }

    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("VolumeMultiplier", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void ApplySettingsButton()
    {
        ApplySettings();
    }
}
