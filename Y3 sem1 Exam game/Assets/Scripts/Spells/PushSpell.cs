using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSpell : MonoBehaviour
{
    //public float duration;
    public float cooldownLength;
    public float cooldownTimer;
    public bool isOnCooldown;
    public GameObject pushObj;
    public float pushAngle;
    public float pushSpeed;
    public float pushBackDistance;
    public float pushBackDuration;
    public float pushTravelDistance;

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
            CastPush();
        }
    }

    void CastPush()
    {
        if (isOnCooldown) return;
        GameObject.FindWithTag("Floor").GetComponent<SimpleSonarShader_Parent>().StartSonarRing(transform.position,1);

        GameObject push = Instantiate(pushObj, transform);
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 pos = hit.point;
            transform.forward = (new Vector3(pos.x,0,pos.z) - transform.position).normalized;

        }
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //Debug.Log(Input.mousePosition);

        Rigidbody[] orbRBs = push.gameObject.GetComponentsInChildren<Rigidbody>();
        int numOrbs = orbRBs.Length;
        float angleStep = pushAngle / (float)numOrbs;
        
        transform.Rotate(Vector3.up, ((int)(numOrbs/2f) * -angleStep));

        for (int i = 0; i < numOrbs; i++)
        {
            orbRBs[i].transform.Rotate(Vector3.up, i * angleStep);
            orbRBs[i].velocity = pushSpeed * orbRBs[i].transform.forward;
            PushBack pushBack = orbRBs[i].gameObject.GetComponent<PushBack>();
            pushBack.distance = pushBackDistance;
            pushBack.duration = pushBackDuration;
        }
        
        Destroy(push,pushTravelDistance/pushSpeed);

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
