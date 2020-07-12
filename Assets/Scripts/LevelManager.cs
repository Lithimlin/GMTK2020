using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> cases = new List<GameObject>();

    public SceneFader sceneFader;
    public string nextLevel;

    private void Start()
    {
        foreach(GameObject casse in cases)
        {
            casse.GetComponent<ButtonScript>().manager = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        }

        if (cases.Count == 0)
        {
            //WIN
            print("win");
            sceneFader.FadeTo(nextLevel);
        }
    }

    public void Death()
    {
        Debug.Log("Death...");
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
}
