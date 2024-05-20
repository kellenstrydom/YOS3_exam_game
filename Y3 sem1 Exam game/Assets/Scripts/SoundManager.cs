using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance staticSound;
    private FMOD.Studio.EventInstance currentSong;

    [SerializeField]
    private bool isStaticPlaying = false;

    [SerializeField] 
    private float staticStartStressValue;

    private void Awake()
    {
        if (!staticSound.isValid())
        {
            staticSound = FMODUnity.RuntimeManager.CreateInstance("event:/static-noise");
            staticSound.start();
            staticSound.setPaused(!isStaticPlaying);
        }
    }

    public void ManageStressLevel(float stress)
    {
        if (stress < staticStartStressValue)
        {
            if (isStaticPlaying)
            {
                StopStatic();
            }
            return;
        }

        if (!isStaticPlaying)
        {
            StartStatic();
        }
        
        StaticVolume(stress);
    }
    
    void StartStatic()
    {
        isStaticPlaying = true;
        staticSound.setPaused(!isStaticPlaying);
    }

    void StaticVolume(float stress)
    {
        staticSound.setVolume(1f);
    }

    void StopStatic()
    {
        isStaticPlaying = false;
        staticSound.setPaused(!isStaticPlaying);
    }

    public void PlaySong(string songPath)
    {
        currentSong.stop(STOP_MODE.IMMEDIATE);
        currentSong.release();
        currentSong = FMODUnity.RuntimeManager.CreateInstance(songPath);
        currentSong.start();
    }
    
}
