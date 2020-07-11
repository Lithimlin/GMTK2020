using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public List<GameObject> players;
    public float time;

    [HideInInspector]
    public int activePlayer = 0;

    void Start()
    {
        StartCoroutine(SwitchPlayer());
    }

    void Update()
    {

    }

    public GameObject getActivePlayer()
    {
        return players[activePlayer];
    }

    IEnumerator SwitchPlayer()
    {
        while(true)
        {
            yield return new WaitForSeconds(time);
            activePlayer = (activePlayer + 1) % players.Count;
        }
    }
}
