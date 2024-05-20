using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundPoint
{
    public Vector3 position;
    public float strength;
    public float lifeTime;

    public bool isBadSound;


    public SoundPoint(Vector3 position, float strength, float lifeTime)
    {
        this.position = position;
        this.strength = strength;
        this.lifeTime = lifeTime;
    }
    
    
    
}
