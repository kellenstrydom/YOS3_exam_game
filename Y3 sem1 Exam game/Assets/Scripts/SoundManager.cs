using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance staticSound;
    private FMOD.Studio.EventInstance currentSong;
    private FMOD.Studio.VCA staticVCA;
    private FMOD.Studio.VCA musicVCA;

    [SerializeField]
    private bool isStaticPlaying = false;

    // [SerializeField]
    // private float staticStartStressValue;
    // [SerializeField]
    // private float staticMaxStressLevel;
    //
    // [SerializeField] private float musicBeginFadeStress;
    // [SerializeField] private float musicEndFadeStress;

    public float staticVolume;
    public float musicVolume;

    public AnimationCurve staticCurve;
    public AnimationCurve musicCurve;
    // [SerializeField] private float staticLowestDb;
    // [SerializeField] [Range(-80f, 10f)] 
    // private float musicVCAvol;
    // [SerializeField][Range(-80f, 10f)] 
    // private float vcaVolume;

    //public ChildInformation _child;
    

    private void Awake()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManager>()._soundManager = this;
        
        if (!staticSound.isValid())
        {
            staticSound = FMODUnity.RuntimeManager.CreateInstance("event:/static-noise");
            staticSound.start();
            staticSound.setPaused(!isStaticPlaying);
            staticVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Static");
            musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        }
        
        StartStatic();
        
    }

    public void ManageStressLevel(float stress)
    {
        musicVolume = musicCurve.Evaluate(stress / 100f);
        musicVCA.setVolume(musicVolume);
        
        staticVolume = staticCurve.Evaluate(stress / 100f);
        staticVCA.setVolume(staticVolume);
        
        // if (stress < staticStartStressValue)
        // {
        //     if (isStaticPlaying)
        //     {
        //         StopStatic();
        //     }
        //     return;
        // }
        // if (!isStaticPlaying)
        // {
        //     StartStatic();
        // }
        
        // StaticVolume(stress);
        // MusicVolume(stress);
        
        //SimpleStressVolume(stress);
    }
    
    void StartStatic()
    {
        isStaticPlaying = true;
        staticSound.setPaused(!isStaticPlaying);
    }

    // void SimpleStressVolume(float stress)
    // {
    //     float musicScale = (stress - musicBeginFadeStress) / (musicEndFadeStress - musicBeginFadeStress);
    //     float staticScale = (stress - staticStartStressValue) / (staticMaxStressLevel - staticStartStressValue);
    //
    //     if (musicScale < 0)
    //         musicVolume = 1;
    //     else if (musicScale > 1)
    //         musicVolume = 0;
    //     else
    //         musicVolume = 1 - musicScale;
    //     
    //     if (staticScale < 0)
    //         staticVolume = 0;
    //     else if (staticScale > 1)
    //         staticVolume = 1;
    //     else
    //         staticVolume = staticScale;
    //
    //     musicVCA.setVolume(musicVolume);
    //     staticVCA.setVolume(staticVolume);
    //
    //
    // }
    
    // void StaticVolume(float stress)
    // {
    //
    //     float volScale = (stress - staticStartStressValue) / (staticMaxStressLevel - staticStartStressValue);
    //     vcaVolume = staticLowestDb + volScale * -staticLowestDb;
    //     staticVolume = Mathf.Pow(10.0f, vcaVolume / 20f);
    //     if (staticVolume > 1)
    //     {
    //         staticVolume = 1;
    //     }        
    //     
    //     staticVCA.setVolume(staticVolume);
    // }
    //
    // void MusicVolume(float stress)
    // {
    //     
    //     float volScale = (stress - musicBeginFadeStress) / (musicEndFadeStress - musicBeginFadeStress);
    //     musicVCAvol = volScale * staticLowestDb;
    //     musicVolume = Mathf.Pow(10.0f, musicVCAvol / 20f);
    //
    //     if (musicVolume > 1)
    //         musicVolume = 1;
    //     
    //     musicVCA.setVolume(musicVolume);
    // }

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

    public void StopAllSound()
    {
        currentSong.stop(STOP_MODE.IMMEDIATE);
        currentSong.release();
        staticSound.stop(STOP_MODE.IMMEDIATE);
        staticSound.release();
    }

}
