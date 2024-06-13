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

    // [Header("Lure Data")]
    // public float lureStrength;
    // public float lureLifeTime;

    [Header("Song Data")] 
    public List<PlaylistObject> allPlaylists = new List<PlaylistObject>();
    public PlaylistObject currentPlaylist;
    //public List<SongObject> allSongs = new List<SongObject>();
    [SerializeField]
    private List<SongObject> songQueue = new List<SongObject>();
    [SerializeField]
    public SongObject currentSong;

    public float songTimer;

    public Transform spellSlot;
    
    // flags and trackers
    private Coroutine songTimerCoroutine;
    
    private HUDController _hud;
    private GameManager gm;
    private void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _hud = GameObject.FindWithTag("HUD").GetComponent<HUDController>();
        HeadphoneColor();
    }

    private void Update()
    {
        if (_hud.isPaused) return;
        if (!gm.isAllowingInputs) return;
        if (Input.GetKeyDown(KeyCode.N))
            PlayNextSong();
        
        // HeadphoneColor();
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
        //Debug.Log(currentSong.stats.speedMultiplier);
        
        _SoundManager.PlaySong(currentSong.path);
        


        songTimerCoroutine = StartCoroutine(SongTimer(currentSong.songLength));

    }

    IEnumerator SongTimer(float songLength)
    {
        songTimer = 0;
        while (songTimer < songLength)
        {
            yield return new WaitForEndOfFrame();
            songTimer += Time.unscaledDeltaTime;
        }
        PlayNextSong();
    }

    void HeadphoneColor()
    {
        if (currentPlaylist == null) 
            headphoneMat.color = Color.grey;
        else
            headphoneMat.color = currentPlaylist.color;
    }

    public void SelectPlaylist(PlaylistObject playlist)
    {
        currentPlaylist = playlist;
        songQueue = new List<SongObject>();
        songQueue.AddRange(playlist.songList);
        _childInformation.NewStats(currentPlaylist.stats);
        
        HeadphoneColor();
        PlayNextSong();
        
        ChangeSpell();
    }

    void ChangeSpell()
    {
        if (spellSlot.gameObject.GetComponentsInChildren<Transform>().Length > 1)
            Destroy(spellSlot.gameObject.GetComponentsInChildren<Transform>()[1].gameObject);
        if (currentPlaylist.spell == null) return;
        Instantiate(currentPlaylist.spell, spellSlot);
        
        _hud.DisplayPlaylist(currentPlaylist);
    }

}
