using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public string[] messages;
    private Tutorial _tutorial;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _tutorial = GameObject.FindWithTag("Tutorial").GetComponent<Tutorial>();
        
        _tutorial.LoadDialog(messages);
    }
}
