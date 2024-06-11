using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChildInformation : MonoBehaviour
{
    [Serializable]
    public struct Stats
    {
        public float speedMultiplier;
        public float stressMultiplier;
    }
    
    
    
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

    public Stats currentStats;

    [Header("UI")]
    [SerializeField]
    private HUDController _hud;
    private ScreenShake cameraShake;

    public Material childMaterial;

    private void Start()
    {
        //currentStats = new Stats();
        currentStats.speedMultiplier = 1;
        currentStats.stressMultiplier = 1;
        cameraShake = Camera.main.GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    private void Update()
    {
        //CheckTargetSound();
        
        CheckStressLevel();


        if (Input.GetKey(KeyCode.Comma))
        {
            AddStress(-10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Period))
        {
            AddStress(10 * Time.deltaTime);
        }
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

    public float DistanceFromSound()
    {
        return Vector3.Distance(transform.position, targetSound.transform.position);
    }

    public void Hit(float stressAmount)
    {
        AddStress(stressAmount);
        cameraShake.ShakeScreenOnHit(1,stressLevel);
    }
    
    //test stress level
    public void AddStress(float amount)
    {
        stressLevel += currentStats.speedMultiplier * amount;

        if (stressLevel > 100) stressLevel = 100;
        
        if (stressLevel < 0) stressLevel = 0;
    }

    public void NewStats(Stats stats)
    {
        Debug.Log(stats.speedMultiplier);
        currentStats.speedMultiplier = stats.speedMultiplier;
        currentStats.stressMultiplier = stats.stressMultiplier;
    }

}
