using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance staticSound;
    private FMOD.Studio.EventInstance currentSong;
    private FMOD.Studio.VCA staticVCA;
    private FMOD.Studio.VCA musicVCA;

    [SerializeField]
    private bool isStaticPlaying = false;

    [SerializeField]
    private float staticStartStressValue;
    [SerializeField]
    private float staticMaxStressLevel;

    [SerializeField] private float musicBeginFadeStress;
    [SerializeField] private float musicEndFadeStress;


    private void Awake()
    {
        if (!staticSound.isValid())
        {
            staticSound = FMODUnity.RuntimeManager.CreateInstance("event:/static-noise");
            staticSound.start();
            staticSound.setPaused(!isStaticPlaying);
            staticVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Static");
            musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
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
        MusicVolume(stress);
    }
    
    void StartStatic()
    {
        isStaticPlaying = true;
        staticSound.setPaused(!isStaticPlaying);
    }

    void StaticVolume(float stress)
    {
        
        float volume = (stress - musicBeginFadeStress) / (musicEndFadeStress - musicBeginFadeStress);
        volume *= volume;
        
        if (volume > 1)
            staticVCA.setVolume(1);
        else
            staticVCA.setVolume(volume);
    }
    
    void MusicVolume(float stress)
    {
        
        float volume = (stress - staticStartStressValue) / (staticMaxStressLevel - staticStartStressValue);
        volume *= volume;
        
        if (volume > 1)
            musicVCA.setVolume(0);
        else
            musicVCA.setVolume(1-volume);
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
