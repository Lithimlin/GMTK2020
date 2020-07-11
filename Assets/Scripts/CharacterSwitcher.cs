using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public List<GameObject> players;
    public float cooldown = 5f;

    [HideInInspector]
    private int activePlayer = 0;
    private float time;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && Time.time > time)
        {
            activePlayer = (activePlayer + 1) % players.Count;
            time = Time.time + cooldown;
        }
    }

    public GameObject getActivePlayer()
    {
        return players[activePlayer];
    }
}
