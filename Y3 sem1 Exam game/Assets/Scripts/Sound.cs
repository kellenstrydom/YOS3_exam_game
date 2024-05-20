using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound
{
    
    public Transform transform;
    public float strength;
    public bool isBadSound;


    public Sound(Transform transform, float strength = 0, bool isBadSound = false)
    {
        this.transform = transform;
        this.strength = strength;
        this.isBadSound = isBadSound;
    }
}
