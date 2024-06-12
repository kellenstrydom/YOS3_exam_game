using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private IpodInformation _ipod;
    private ChildInformation _child;

    private int numSongs = 0;
    private List<SongSelectUI> selectUIs = new List<SongSelectUI>();
    
    [Header("Stress")]
    [SerializeField]
    private TMP_Text stressText;
    
    [Header("Playlist")]
    [SerializeField] private TMP_Text currentText;
    [SerializeField] private TMP_Text runtimeText;
    [SerializeField] private GameObject songPlaylistPanel;
    [SerializeField] private GameObject songSelectPrefab;

    private void Awake()
    {
        _ipod = GameObject.FindWithTag("Player").GetComponent<IpodInformation>();
        _child = GameObject.FindWithTag("Player").GetComponent<ChildInformation>();
        
        PopulateSelect();
    }

    private void Start()
    {
        DisplayPlaylistScreen();
    }

    private void Update()
    {
        DisplayStress();
        DisplayCurrentSong();
        
        if (Input.GetKeyDown(KeyCode.Tab))
            DisplayPlaylistScreen();
    }

    void DisplayPlaylistScreen()
    {
        foreach (var ui in selectUIs)
        {
            ui.Reset();
        }
        
        bool isActive = songPlaylistPanel.activeInHierarchy;
        songPlaylistPanel.SetActive(!isActive);
        
        if (!isActive)
            Time.timeScale = .2f;
        else
            Time.timeScale = 1;
    }

    public void SelectedPlaylist(PlaylistObject playlist)
    {
        _ipod.SelectPlaylist(playlist);
        DisplayPlaylistScreen();
    }

    public void SongSelected(SongObject song)
    {
        _ipod.PlaySelectedSong(song);
        DisplayPlaylistScreen();
    }

    public void DisplayStress()
    {
        stressText.text = $"Stress: {_child.stressLevel}";
    }

    public void DisplayCurrentSong()
    {
        if (!_ipod.currentSong) return;
        currentText.text = _ipod.currentSong.name;
        runtimeText.text = $"Playtime: {(int)_ipod.songTimer}/{(int)_ipod.currentSong.songLength}";
    }

    public void NextSongButton()
    {
        _ipod.PlayNextSong();
    }

    void PopulateSelect()
    {
        foreach (var x in selectUIs)
        {
            selectUIs.Remove(x);
            Destroy(x.gameObject);
        }
        
        float count = _ipod.allPlaylists.Count;
        double angle = (2 * Math.PI) / count;

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(songSelectPrefab, songPlaylistPanel.transform);
            Vector3 pos = new Vector3((float)Math.Cos(i * angle),(float)Math.Sin(i * angle),0) * 200;
            obj.GetComponent<RectTransform>().localPosition = pos;
            SongSelectUI selectUI = obj.GetComponent<SongSelectUI>();
            selectUIs.Add(selectUI);
            selectUI.SongData(_ipod.allPlaylists[i]);
        }

    }
    
}
