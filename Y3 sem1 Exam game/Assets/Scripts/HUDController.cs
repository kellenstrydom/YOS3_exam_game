using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private IpodInformation _ipod;
    private ChildInformation _child;
    
    [Header("Stress")]
    [SerializeField]
    private TMP_Text stressText;
    
    [Header("Playlist")]
    [SerializeField]
    private TMP_Text currentText;
    [SerializeField]
    private TMP_Text runtimeText;

    private void Awake()
    {
        _ipod = GameObject.FindWithTag("Player").GetComponent<IpodInformation>();
        _child = GameObject.FindWithTag("Player").GetComponent<ChildInformation>();
    }

    private void Update()
    {
        DisplayStress();
        DisplayCurrentSong();
    }

    public void DisplayStress()
    {
        stressText.text = $"Stress: {_child.stressLevel}";
    }

    public void DisplayCurrentSong()
    {
        currentText.text = _ipod.currentSong.name;
        runtimeText.text = $"Playtime: {(int)_ipod.songTimer}/{(int)_ipod.currentSong.songLength}";
    }

    public void NextSongButton()
    {
        _ipod.PlayNextSong();
    }
}
