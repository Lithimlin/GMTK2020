using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> cases = new List<GameObject>();

    private void Start()
    {
        foreach(GameObject casse in cases)
        {
            casse.GetComponent<ButtonScript>().manager = this;
        }
    }

    private void Update()
    {
        if(cases.Count == 0)
        {
            //WIN
            print("win");
        }
    }
}
