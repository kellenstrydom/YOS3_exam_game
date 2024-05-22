using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    private Transform child;
    private Vector3 offset;

    private void Awake()
    {
        child = GameObject.FindWithTag("Player").transform;
        offset = transform.position - child.position;
    }

    private void Update()
    {
        transform.position = child.transform.position + offset;
    }
}
