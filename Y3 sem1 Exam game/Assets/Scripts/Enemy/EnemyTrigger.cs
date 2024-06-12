using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public Transform enemies;

    private void Awake()
    {
        foreach (Transform enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        foreach (Transform enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
}
