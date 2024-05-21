using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChildInformation : MonoBehaviour
{
    [Header("Ipod Information")]
    [SerializeField] 
    private SoundManager _soundManager;
    [SerializeField]
    public List<Sound> sounds = new List<Sound>();
    public bool isSoundHeard = false;
    public Sound targetSound = null;
    

    [Header("Child Stats")] 
    [SerializeField]
    [Range(0, 100f)]
    public float stressLevel;
    
    [Header("UI")]
    [SerializeField]
    private HUDController _hud;

    public Material childMaterial;
    // Update is called once per frame
    private void Update()
    {
        CheckTargetSound();
        
        CheckStressLevel();
    }

    void CheckStressLevel()
    {
        //_hud.DisplayStress();
        float t = stressLevel / 100f;

        childMaterial.color = Color.Lerp(Color.white, Color.black, t);
        
        _soundManager.ManageStressLevel(stressLevel);
        
    }

    void CheckTargetSound()
    {
        if (sounds.Count == 0)
        {
            targetSound = null;
            isSoundHeard = false;
            return;
        }
        
        
        foreach (var sound in sounds)
        {
            if (sound.strength == 0)
            {
                if (targetSound == sound)
                    targetSound = null;
                break;
            }

            //isSoundHeard = true;
            if (targetSound == null)
            {
                targetSound = sound;
            }
            else
            {
                float targetDistance = Vector3.Distance(transform.position, targetSound.transform.position);
                float checkDistance = Vector3.Distance(transform.position, sound.transform.position);

                if (targetSound.strength / targetDistance < sound.strength / checkDistance)
                {
                    targetSound = sound;
                }
            }
        }

        isSoundHeard = targetSound != null;
    }

    public void HearSound(Sound sound)
    {
        sounds.Add(sound);
        //StartCoroutine(SoundLife(sound));
    }

    // IEnumerator SoundLife(Sound sound)
    // {
    //     yield return new WaitForSeconds(sound.lifeTime);
    //     if (sound == targetSound)
    //     {
    //         targetSound = null;
    //         isSoundHeard = false;
    //     }
    //     sounds.Remove(sound);
    // }

    public float DistanceFromSound()
    {
        return Vector3.Distance(transform.position, targetSound.transform.position);
    }

   
}
