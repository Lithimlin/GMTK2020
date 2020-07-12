using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> cases = new List<GameObject>();

    public SceneFader sceneFader;
    public string nextLevel;

    private bool dead = false;

    public AudioClip sound;

    AudioSource myAudio;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        foreach (GameObject casse in cases)
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
            if (!dead)
            {
                print("win");
                sceneFader.FadeTo(nextLevel);
                dead = true;
            }
        }
    }

    public void Death()
    {
        if (!dead)
        {
            Debug.Log("Death...");
            myAudio.PlayOneShot(sound);
            sceneFader.FadeTo(SceneManager.GetActiveScene().name);
            dead = true;
        }
    }
}
