using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        ApplySavedSettings();
    }

    public void ApplySavedSettings()
    {
        bool isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        float volumeMultiplier = PlayerPrefs.GetFloat("VolumeMultiplier", 1.0f);
        audioSource.enabled = isSoundOn;
        audioSource.volume = volumeMultiplier;
    }
    public void Play()
    {
        if (audioSource != null && audioSource.enabled)
        {
            audioSource.Play();
        }
    }
}
