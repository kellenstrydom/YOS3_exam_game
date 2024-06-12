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
    
    private HUDController _hud;

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _hud = GameObject.FindWithTag("HUD").GetComponent<HUDController>();
        _hud.SpellCooldown(0,1);
    }

    private void Update()
    {
        
        if (!gm.isAllowingInputs || _hud.isPaused) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastShield();
        }
    }

    void CastShield()
    {
        if (isOnCooldown) return;
        GameObject.FindWithTag("Floor").GetComponent<SimpleSonarShader_Parent>().StartSonarRing(transform.position,1);

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
            _hud.SpellCooldown(cooldownLength - cooldownTimer,cooldownLength);
            yield return null;
        }

        isOnCooldown = false;
    }
}
