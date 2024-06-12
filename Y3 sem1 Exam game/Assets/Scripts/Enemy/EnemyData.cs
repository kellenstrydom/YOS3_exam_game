using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType
    {
        follow,
        straight,
        up,
        down,
        left,
        right,
        
    }

    public float lifeTime;
    public string path;
    public EnemyType type;
    public float stress;
    public float turnSpeed;
    public float sightRadius;
    public float stressRadius;
    public float moveSpeed;
}
