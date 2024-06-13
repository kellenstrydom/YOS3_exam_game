using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "Song")]
public class SongObject : ScriptableObject
{
    public string title;
    public IpodInformation.Genre genre;
    public string path;
    public float songLength;
    //public Color color;
    //public ChildInformation.Stats stats;
}
