using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public string path;
    public float sightRadius;
    public float stressRadius;
    public float moveSpeed;
}
