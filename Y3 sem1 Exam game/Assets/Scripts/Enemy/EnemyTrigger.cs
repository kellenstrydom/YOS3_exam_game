using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject enemies;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        foreach (GameObject enemy in enemies.GetComponentsInChildren<GameObject>())
        {
            enemy.SetActive(true);
        }
    }
}
