using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public string level1;

    private AudioSource[] allAudioSources;

    public void LoadLevelOne()
    {
        sceneFader.FadeTo(level1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void Awake()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (allAudioSources.Length >= 2)
        {
            Destroy(allAudioSources[0]);
        }
        
    }
}
