using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChildMovement : MonoBehaviour
{
    [SerializeField] 
    private Vector3 direction;
    
    [SerializeField]
    private float speed;
    
    // Update is called once per frame
    void Update()
    {
        Move(AutoMove() + PlayerMovement());
    }

    Vector3 AutoMove()
    {
        return direction;
    }
    
    void Move(Vector3 dir)
    {
        dir = new Vector3(fixVal(dir.x), 0, fixVal(dir.z)).normalized;
        if (dir.magnitude != 0) transform.forward = dir;
        transform.position += (dir * (speed * Time.deltaTime));
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
