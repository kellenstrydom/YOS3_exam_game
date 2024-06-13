using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogDisplay : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text textBox;
    private int messageIndex;
    private string[] messages;
    
    public string[] startDialog;

    private void Start()
    {
        if (startDialog.Length > 0)
            LoadDialog(startDialog);
    }

    private GameManager gm;

    private float preTimeScale;

    private void Awake()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    public void LoadDialog(string[] messages)
    {
        gm.isAllowingInputs = false;
        preTimeScale = Time.timeScale;
        Time.timeScale = 0;
        this.messages = messages;
        messageIndex = 0;
        NextMessage();
        dialogBox.SetActive(true);
    }

    public void NextMessage()
    {
        if (messageIndex < messages.Length)
        {
            textBox.text = messages[messageIndex++];
        }
        else
        {
            Time.timeScale = preTimeScale;
            gm.isAllowingInputs = true;
            messages = new string[] {};
            dialogBox.SetActive(false);
        }
    }
    
}
