using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public bool isAllowingInputs;
    public int nextScene;
    public int menuScene;

    public SoundManager _soundManager;

    public void Win()
    {
        Time.timeScale = 0;
        isAllowingInputs = false;
        GameObject.FindWithTag("HUD").GetComponent<HUDController>().DisplayWinScreen();
    }

    public void GoToMenu()
    {
        _soundManager.StopAllSound();
        SceneManager.LoadScene(menuScene);
    }

    public void GoToNextScene()
    {
        _soundManager.StopAllSound();
        SceneManager.LoadScene(nextScene);
    }
}
