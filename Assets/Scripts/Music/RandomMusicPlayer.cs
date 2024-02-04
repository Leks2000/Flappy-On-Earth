using UnityEngine;
using System.Collections.Generic;

public class UniqueMusicPlayer : MonoBehaviour
{
    public AudioClip[] musicTracks;
    public AudioSource audioSource;
    private PlayerController player;
    private bool isPaused;

    private List<int> playedTracks = new List<int>();

    void Start()
    {
        PlayRandomMusic();
        player = GameObject.Find("Player")?.GetComponent<PlayerController>();
    }

    void PlayRandomMusic()
    {
        if (musicTracks.Length > 0)
        {
            int randomIndex = GetRandomUnplayedTrackIndex();
            PlayerPrefs.SetInt("IndexMusic", randomIndex);
            if (randomIndex != -1)
            {
                audioSource.clip = musicTracks[randomIndex];
                Play();
                playedTracks.Add(randomIndex);
            }
            else
            {
                playedTracks.Clear();
                PlayRandomMusic();
            }
        }
    }

    int GetRandomUnplayedTrackIndex()
    {
        List<int> unplayedTracks = new List<int>();

        for (int i = 0; i < musicTracks.Length; i++)
        {
            if (!playedTracks.Contains(i))
            {
                unplayedTracks.Add(i);
            }
        }

        if (unplayedTracks.Count > 0)
        {
            int randomIndex = Random.Range(0, unplayedTracks.Count);
            return unplayedTracks[randomIndex];
        }

        return -1;
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
