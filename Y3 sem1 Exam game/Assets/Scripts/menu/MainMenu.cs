using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int tutSceneIndex;
    public int startLevelSceneIndex;

    public void GoToTutorial()
    {
        SceneManager.LoadScene(tutSceneIndex);
    }

    public void GoToStartLevel()
    {
        SceneManager.LoadScene(startLevelSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
