using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        ApplySavedSettings();
    }

    public void ApplySavedSettings()
    {
        float volumeMultiplier = PlayerPrefs.GetFloat("VolumeMultiplier", 1.0f);
        audioSource.volume = volumeMultiplier;
        audioSource.enabled = volumeMultiplier <= 0 ? false : true;
    }
    public void Play()
    {
        if (audioSource != null && audioSource.enabled)
        {
            audioSource.Play();
        }
    }
}
