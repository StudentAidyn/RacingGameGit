using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//test

public class MusicPlayer : MonoBehaviour
{
    

    [SerializeField]
    private AudioSource audioSource;
    public Album[] albums;
    private int lastAlbumInd;
    public int currentAlbum;
    //private int currentTrack;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.loop = false;
        currentAlbum = 0;
        RandomSelect();
    }

    void RandomSelect()
    {
        albums[currentAlbum].currentTrack = Random.Range(0, albums[currentAlbum].Clips.Length);
    }

    // Update is called once per frame
    void Update()
    {
        AudioClip[] track = albums[currentAlbum].Clips;
        if(currentAlbum != lastAlbumInd)
        {
            audioSource.Stop();
            lastAlbumInd = currentAlbum;
        }
        if (!audioSource.isPlaying)
        { 
            updateTrack();
            audioSource.Play();
        }

    }

    void updateTrack()
    {
        albums[currentAlbum].currentTrack = (albums[currentAlbum].currentTrack + 1) % albums[currentAlbum].Clips.Length;
        audioSource.clip = albums[currentAlbum].Clips[albums[currentAlbum].currentTrack];
    }

    void ChangeAlbum()
    {
        currentAlbum = (currentAlbum + 1) % albums.Length;
    }
}
[System.Serializable]
public struct Album
{
    public AudioClip[] Clips;
    public int currentTrack;
}
