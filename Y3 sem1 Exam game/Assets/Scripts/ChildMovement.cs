using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChildMovement : MonoBehaviour
{
    [SerializeField] 
    private Vector3 direction;
    
    [SerializeField]
    public float speed;

    private ChildInformation _child;
    public Transform lookDir;

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _child = gameObject.GetComponent<ChildInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isAllowingInputs) return;
        Move(AutoMove() + PlayerMovement());
    }

    Vector3 AutoMove()
    {
        return direction;
    }
    
    void Move(Vector3 dir)
    {
        dir = new Vector3(fixVal(dir.x), 0, fixVal(dir.z)).normalized;
        if (dir.magnitude != 0) 
            lookDir.forward = dir;
        transform.position += (dir * (speed * Time.deltaTime * _child.currentStats.speedMultiplier));
    }

    Vector3 PlayerMovement()
    {
        Vector3 playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        return playerInput;
    }

    float fixVal(float x)
    {
        if (x == 0)
            return 0;

        return Mathf.Sign(x);
    }

    public void setDirection(Vector3 newDir)
    {
        direction = newDir;
    }

}
