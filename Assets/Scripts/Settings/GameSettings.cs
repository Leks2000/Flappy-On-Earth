using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        ApplySavedSettings();
    }

    private void ApplySavedSettings()
    {
        bool isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        float volumeMultiplier = PlayerPrefs.GetFloat("VolumeMultiplier", 1.0f);
        audioSource.enabled = isSoundOn;
        audioSource.volume = volumeMultiplier;
    }
}
