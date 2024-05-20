using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Lure : MonoBehaviour
{
    public SimpleSonarShader_Parent shaderScript;
    public ChildInformation _ChildInformation; 

    public bool isPlaced;
    public float lifeTime;
    public float ringInterval;
    public float strength;
    public float ringRange;
    public Sound sound = null;
    public float timer;

    private void Start()
    {
        sound = new Sound(transform);
        _ChildInformation.sounds.Add(sound);
    }

    public void Placement(Vector3 pos)
    {
        transform.position = pos;
        sound.strength = strength;
        timer = 0;
        if (!isPlaced)
        {
            isPlaced = true;
            StartCoroutine(Pulse());
        }
    }

    private void Update()
    {
        if (!isPlaced) return;

        if (timer < lifeTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            EndLure();
        }
    }

    private IEnumerator Pulse()
    {
        if (!isPlaced) yield return null;
        else
        {
            shaderScript.StartSonarRing(transform.position,1);
            yield return new WaitForSeconds(ringInterval);

            StartCoroutine(Pulse());
        }
        
        
    }

    void EndLure()
    {
        sound.strength = 0;
        isPlaced = false;
    }
    
    
}
