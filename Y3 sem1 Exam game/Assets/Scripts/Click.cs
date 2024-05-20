using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerDownHandler
{
    public IpodInformation _ipod;
    public Lure _lure;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 clickPos = eventData.pointerPressRaycast.worldPosition;
        Debug.Log($"click at {clickPos}");
        _lure.Placement(clickPos);
        
        //_ipod.MakeLureSound(clickPos);
        //StartCoroutine(MultiClick(3, .3f, clickPos));

        //gameObject.GetComponent<SimpleSonarShader_Object>().StartSonarRing(clickPos,1);
    }

    private IEnumerator MultiClick(int number, float timeBetween, Vector3 pos)
    {
        SimpleSonarShader_Object shaderScript = gameObject.GetComponent<SimpleSonarShader_Object>();
        for (int i = 0; i < number; i++)
        {
            //Debug.Log($"click {i}");
            shaderScript.StartSonarRing(pos, 1);
            yield return new WaitForSeconds(timeBetween);
        }
    }
}
