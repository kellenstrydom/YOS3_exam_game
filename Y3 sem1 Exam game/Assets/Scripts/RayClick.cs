using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayClick: MonoBehaviour
{

    public Lure _lure;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
            Click();
    }

    void Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 pos = hit.point;
            Debug.Log($"{hit.transform.name} at {pos}");
            _lure.Placement(pos);
        }
    }
}
