using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public AnimationCurve curve;
    
    public void ShakeScreenOnHit(float duration, float stress)
    {
        float strengthScale = 1 + stress / 100f;
        StartCoroutine(Shaking(duration, strengthScale));
    }

    IEnumerator Shaking(float duration, float strengthScale)
    {
        Vector3 startPos = Vector3.zero;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration) * strengthScale;
            transform.localPosition = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.localPosition = startPos;
    }
}
