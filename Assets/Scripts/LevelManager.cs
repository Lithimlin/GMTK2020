using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> cases = new List<GameObject>();
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
            SwitchScene(SceneManager.GetActiveScene().name);
        }

        if (cases.Count == 0)
        {
            //WIN
            print("win");
            SwitchScene(nextLevel);
        }
    }

    public void Death()
    {
        Debug.Log("Death...");
    }

    private void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
