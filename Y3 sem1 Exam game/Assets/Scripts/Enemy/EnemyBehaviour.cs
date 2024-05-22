using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private ChildInformation _child;
    private NavMeshAgent agent;
    [SerializeField]
    private EnemyData data;
    
    private void Awake()
    {
        _child = GameObject.FindWithTag("Player").GetComponent<ChildInformation>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = data.moveSpeed;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _child.transform.position) < data.sightRadius)
        {
            agent.SetDestination(_child.transform.position);
        }
        else if (!agent.isStopped)
        {
            agent.Stop();
        }
    }
}
