using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldSpell : MonoBehaviour
{
    public float duration;
    public float cooldownLength;
    public float cooldownTimer;
    public bool isOnCooldown;
    public GameObject shield;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastShield();
        }
    }

    void CastShield()
    {
        if (isOnCooldown) return;
        
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        GameObject obj = Instantiate(shield, player);
        Destroy(obj,duration);

        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = 0;

        while (cooldownTimer < cooldownLength)
        {
            cooldownTimer += Time.deltaTime;
            yield return null;
        }

        isOnCooldown = false;
    }
}
