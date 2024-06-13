using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public string[] messages;
    private DialogDisplay _tutorial;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _tutorial = GameObject.FindWithTag("Dialog UI").GetComponent<DialogDisplay>();
        
        _tutorial.LoadDialog(messages);
    }
}
