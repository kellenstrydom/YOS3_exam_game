using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IpodInformation : MonoBehaviour
{
    [Serializable]
    public enum Genre
    {
        rock,
        lofi,
        pop,
        hiphop,
    }
    
    
    public ChildInformation _childInformation;
    public SoundManager _SoundManager;

    [Header("Lure Data")]
    public float lureStrength;
    public float lureLifeTime;

    [Header("Song Data")]
    [SerializeField]
    private List<SongObject> songQueue = new List<SongObject>();

    [SerializeField]
    private SongObject currentSong;

    private void Start()
    {
        PlayNextSong();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            PlayNextSong();
    }


    void PlayNextSong()
    {
        if (currentSong != null)
        {
            songQueue.Add(currentSong);
        }
        currentSong = songQueue[0];
        songQueue.Remove(currentSong);
        _SoundManager.PlaySong(currentSong.path);
        
    }
}
