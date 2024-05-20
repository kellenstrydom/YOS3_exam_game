using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildController : MonoBehaviour
{
    private NavMeshAgent navAgent;

    [SerializeField] 
    private float stoppingDistanceFromSound;
    public ChildInformation _childInformation;

    [SerializeField] 
    private float baseMoveSpeed;

    private void Awake()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        //navAgent.SetDestination(new Vector3(1, 0, 2));
        //navAgent.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //LookAtTargetSound();

        if (_childInformation.isSoundHeard)
        {
            MoveTowardsSound();
        }
        
        
    }

    void MoveTowardsSound()
    {
        navAgent.SetDestination(_childInformation.targetSound.transform.position);

        // Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        //Debug.Log(transform.forward);
        //transform.position += transform.forward * (baseMoveSpeed * Time.deltaTime);
        //transform.Translate(transform.forward * (baseMoveSpeed * Time.deltaTime));

    }
    
    void LookAtTargetSound()
    {
        if (!_childInformation.isSoundHeard)
        {
            return;
        }

        Vector3 soundPos = _childInformation.targetSound.transform.position;
        transform.forward = new Vector3(soundPos.x - transform.position.x, 0f, soundPos.z - transform.position.z).normalized;
    }
}
