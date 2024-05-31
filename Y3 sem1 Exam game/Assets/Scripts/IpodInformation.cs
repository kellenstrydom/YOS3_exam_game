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
    public Material headphoneMat;

    [Header("Lure Data")]
    public float lureStrength;
    public float lureLifeTime;

    [Header("Song Data")]
    public List<SongObject> allSongs = new List<SongObject>();
    [SerializeField]
    private List<SongObject> songQueue = new List<SongObject>();
    [SerializeField]
    public SongObject currentSong;

    public float songTimer;
    
    
    
    
    // flags and trackers
    private Coroutine songTimerCoroutine;

    private void Start()
    {
        //PlayNextSong();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            PlayNextSong();
        
        HeadphoneColor();
    }

    public void PlaySelectedSong(SongObject song)
    {
        if (currentSong != null)
        {
            StopCoroutine(songTimerCoroutine);
            songQueue.Add(currentSong);
        }
        
        currentSong = song;
        songQueue.Remove(currentSong);
        _SoundManager.PlaySong(currentSong.path);

        songTimerCoroutine = StartCoroutine(SongTimer(currentSong.songLength));
    }

    public void PlayNextSong()
    {
        if (currentSong != null)
        {
            StopCoroutine(songTimerCoroutine);
            songQueue.Add(currentSong);
        }
        currentSong = songQueue[0];
        songQueue.Remove(currentSong);
        Debug.Log(currentSong.stats.speedMultiplier);
        _childInformation.NewStats(currentSong.stats);
        
        _SoundManager.PlaySong(currentSong.path);
        


        songTimerCoroutine = StartCoroutine(SongTimer(currentSong.songLength));

    }

    IEnumerator SongTimer(float songLength)
    {
        songTimer = 0;
        while (songTimer < songLength)
        {
            yield return new WaitForEndOfFrame();
            songTimer += Time.deltaTime;
        }
        PlayNextSong();
    }

    void HeadphoneColor()
    {
        if (currentSong == null) 
            headphoneMat.color = Color.grey;
        else
            headphoneMat.color = currentSong.color;
    }

}
