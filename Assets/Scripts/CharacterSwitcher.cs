using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour
{
    public List<GameObject> players;
    public float cooldown = 5f;

    [Header("Fill Bar")]
    public Canvas cooldownBar;
    public Image bar;
    public Text text;

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

    private void LateUpdate()
    {
        cooldownBar.transform.position = GetActivePlayer().transform.position + Vector3.up * .8f + Vector3.right * .05f;
        int timeLeft = Mathf.RoundToInt(time - Time.time);
        if (timeLeft > 0)
        {
            text.text = timeLeft.ToString() + "s";
            bar.fillAmount = timeLeft / cooldown;
        } 
        else
        {
            text.text = "Ready";
            bar.fillAmount = 0;
        }
    }

    public GameObject GetActivePlayer()
    {
        return players[activePlayer];
    }
}
