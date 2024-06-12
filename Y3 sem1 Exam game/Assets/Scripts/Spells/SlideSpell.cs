using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideSpell : MonoBehaviour
{
    public float duration;
    public float slideSpeed;
    public float cooldownLength;
    public float cooldownTimer;
    public bool isOnCooldown;
    public ChildMovement _child;

    private HUDController _hud;

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _hud = GameObject.FindWithTag("HUD").GetComponent<HUDController>();
        _hud.SpellCooldown(0,1);
        _child = GameObject.FindWithTag("Player").GetComponent<ChildMovement>();
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

        StartCoroutine(StartSlide());
        
        StartCoroutine(StartCooldown());
    }

    IEnumerator StartSlide()
    {
        float oldSpeed = _child.speed;
        _child.speed = slideSpeed;

        yield return new WaitForSeconds(duration);
        _child.speed = oldSpeed;
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
