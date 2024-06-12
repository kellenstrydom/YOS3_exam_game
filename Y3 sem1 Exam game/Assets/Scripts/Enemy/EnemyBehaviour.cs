using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform child;
    [SerializeField]
    private EnemyData data;
    private SimpleSonarShader_Parent _shaderParent;
    private bool isPushed;
    
    private void Awake()
    {
        child = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _shaderParent = GameObject.FindWithTag("Floor").GetComponent<SimpleSonarShader_Parent>();

        switch (data.type)
        {
            case EnemyData.EnemyType.follow:
            case EnemyData.EnemyType.straight:
                transform.forward = Vector3.Normalize(child.position - transform.position);
                break;
            case EnemyData.EnemyType.down:
                transform.forward = Vector3.back;
                break;
            case EnemyData.EnemyType.up:
                transform.forward = Vector3.forward;
                break;
            case EnemyData.EnemyType.left:
                transform.forward = Vector3.left;
                break;
            case EnemyData.EnemyType.right:
                transform.forward = Vector3.right;
                break;
        }
    }

    private void Start()
    {
        _shaderParent.StartSonarRing(transform.position,1);
        Destroy(gameObject,data.lifeTime);
    }

    private void Update()
    {
        switch (data.type)
        {
            case EnemyData.EnemyType.follow:
                FollowPlayer();
                break;
            case EnemyData.EnemyType.straight:
            case EnemyData.EnemyType.down:
            case EnemyData.EnemyType.up:
            case EnemyData.EnemyType.left:
            case EnemyData.EnemyType.right:
                StraightMovement();
                break;
        }
    }

    void FollowPlayer()
    {
        Vector3 playerDir = Vector3.Normalize(child.position - transform.position);

        float singleStep = data.turnSpeed * Mathf.PI / 180f * Time.deltaTime;

        transform.forward = Vector3.RotateTowards(transform.forward, playerDir, singleStep, 0.0f);
        transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
        
        transform.position += transform.forward * (data.moveSpeed * Time.deltaTime);
    }

    void StraightMovement()
    {
        transform.position += transform.forward * (data.moveSpeed * Time.deltaTime);
    }

    IEnumerator Pushed(Vector3 dir, float duration, float distance)
    {
        float timer = 0;
        isPushed = true;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.position += (dir *(distance/duration * Time.deltaTime));
            yield return null;
        }

        isPushed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
            return;
        }

        if (other.GetComponent<PushBack>() != null)
        {
            if (!isPushed)
            {
                PushBack pushBack = other.GetComponent<PushBack>();
                
                Vector3 dir = other.GetComponent<Rigidbody>().velocity.normalized;
                StartCoroutine(Pushed(dir, pushBack.duration, pushBack.distance));
            }        
        }
        
        if (!other.CompareTag("Player")) return;
        
        other.GetComponent<ChildInformation>().Hit(data.stress);
        Destroy(gameObject);
        
    }
}
