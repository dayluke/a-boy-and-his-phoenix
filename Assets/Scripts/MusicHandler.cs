using System;
using System.Threading.Tasks;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioClip[] songsToPlay;
    private AudioSource audioSource;
    private int currSongIndex = 0;

    private static MusicHandler instance = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        PlayNextSong();
    }

    private async void PlayNextSong()
    {
        audioSource.clip = songsToPlay[currSongIndex];
        audioSource.Play();
        await Task.Delay(TimeSpan.FromSeconds(songsToPlay[currSongIndex].length));
        currSongIndex++;
        if (currSongIndex > songsToPlay.Length - 1) currSongIndex = 0;
        PlayNextSong();
    }    
}
