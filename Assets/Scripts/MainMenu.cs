using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Scene level1;
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(level1.name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
