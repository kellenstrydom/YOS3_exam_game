using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SongSelectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private IpodInformation _ipod;
    public TMP_Text title;
    private PlaylistObject playlist;

    public HUDController _hud;

    private void Awake()
    {
        _ipod = GameObject.FindWithTag("Player").GetComponent<IpodInformation>();
        _hud = GameObject.FindWithTag("HUD").GetComponent<HUDController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            gameObject.GetComponent<Image>().color = Color.white;
    }

    public void SongData(PlaylistObject playlist)
    {
        this.playlist = playlist;
        title.text = $"{this.playlist.playlistName}";
    }

    public void Reset()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = playlist.color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _hud.SelectedPlaylist(playlist);
    }
}
