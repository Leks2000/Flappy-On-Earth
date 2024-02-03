using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    public AudioClip[] musicTracks;
    public AudioSource audioSource;
    private PlayerController player;
    private bool isPaused;
    void Start()
    {
        PlayRandomMusic();
        player = GameObject.Find("Player")?.GetComponent<PlayerController>();
    }

    void PlayRandomMusic()
    {
        if (musicTracks.Length > 0)
        {
            int randomIndex = Random.Range(0, musicTracks.Length);
            audioSource.clip = musicTracks[randomIndex];
            Play();
        }
    }
    void Update()
    {
        if (!audioSource.isPlaying && audioSource.enabled && (player == null || player.GameOver() == false) && isPaused == true)
        {
            PlayRandomMusic();
        }
    }
    public void Pause()
    {
        if (audioSource.enabled)
        {
            audioSource.Pause();
            isPaused = false;
        }
    }
    public void Play()
    {
        if (audioSource.enabled)
        {
            audioSource.Play();
            isPaused = true;
        }
    }
}
