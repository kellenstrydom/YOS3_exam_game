using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Playlist")]
public class PlaylistObject : ScriptableObject
{
    public string playlistName;
    public List<SongObject> songList;
    public string spellName;
    public GameObject spell;
    public Color color;
    public ChildInformation.Stats stats;
}
