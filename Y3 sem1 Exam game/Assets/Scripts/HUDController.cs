using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    
    
    private IpodInformation _ipod;
    private ChildInformation _child;

    private int numSongs = 0;
    private List<SongSelectUI> selectUIs = new List<SongSelectUI>();
    
    [Header("Stress")]
    [SerializeField]
    private TMP_Text stressText;

    [SerializeField] private Image stressSlider;
    
    [Header("Playlist")]
    [SerializeField] private TMP_Text currentText;
    [SerializeField] private TMP_Text runtimeText;
    [SerializeField] private TMP_Text playlistTitle;
    [SerializeField] private TMP_Text stressStatsText;
    [SerializeField] private TMP_Text speedStatsText;
    [SerializeField] private GameObject songPlaylistPanel;
    [SerializeField] private GameObject songSelectPrefab;

    [Header("Spell")] 
    [SerializeField] private Image spellBackground;
    [SerializeField] private TMP_Text spellText;
    [SerializeField] private Image spellCooldown;
    
    [Header("Pause")]
    public bool isPaused;
    [SerializeField] private GameObject pausePanel;

    [Header("Win")] 
    [SerializeField] private GameObject winPanel;

    private GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _ipod = GameObject.FindWithTag("Player").GetComponent<IpodInformation>();
        _child = GameObject.FindWithTag("Player").GetComponent<ChildInformation>();
        DisplayPlaylistScreen();
        
        PopulateSelect();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        DisplayStress();
        DisplayCurrentSong();
       
        if (!gm.isAllowingInputs) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
        

        if (isPaused) return;
        // if (Input.GetKeyDown(KeyCode.Tab) && !songPlaylistPanel.activeInHierarchy)
        //     DisplayPlaylistScreen();
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
            Time.timeScale = 0;
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
        stressText.text = $"Stress: {(int)_child.stressLevel}";
        stressSlider.fillAmount = _child.stressLevel / 100f;
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

    public void DisplayPlaylist(PlaylistObject playlist)
    {
        ChangeSpell(playlist.spellName,playlist.color);
        playlistTitle.text = playlist.name;
        speedStatsText.text = "Speed: ";
        speedStatsText.text += playlist.stats.speedMultiplier > 1 ? $"+{(playlist.stats.speedMultiplier - 1) * 100}%" : $"{(playlist.stats.speedMultiplier - 1) * 100}%";
        stressStatsText.text = "Stress: ";
        stressStatsText.text += playlist.stats.stressMultiplier > 1 ? $"+{(playlist.stats.stressMultiplier - 1) * 100}%" : $"{(playlist.stats.stressMultiplier - 1) * 100}%";
    }

    public void ChangeSpell(string name, Color color)
    {
        spellText.text = name;
        spellBackground.color = color;
    }

    public void SpellCooldown(float timeLeft, float totalLength)
    {
        spellCooldown.fillAmount = timeLeft / totalLength;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;
        
        pausePanel.SetActive(isPaused);
    }

    public void MainMenu()
    {
        gm.GoToMenu();
    }

    public void DisplayWinScreen()
    {
        winPanel.SetActive(true);
        winPanel.GetComponent<Image>().color = Color.Lerp(Color.grey, Color.black, _child.stressLevel / 100f);
    }

    public void WinScreenButtonClick()
    {
        gm.GoToNextScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
