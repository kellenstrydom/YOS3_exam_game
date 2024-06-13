using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public void DestinationReached()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().Win();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        DestinationReached();
    }
}
