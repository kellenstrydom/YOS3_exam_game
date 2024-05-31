using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChange : MonoBehaviour
{
    public Vector3 newDirection;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInChildren<ChildMovement>().setDirection(newDirection);
    }
}
