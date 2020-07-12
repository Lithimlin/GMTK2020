using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour
{
    public List<GameObject> players;
    public float cooldown = 5f;
    public Camera sceneCam;
    public float zoomStrengh;
    public float zoomSpeed;

    [Header("Fill Bar")]
    public Canvas cooldownBar;
    public Image bar;
    public Text text;

    [HideInInspector]
    public bool firstSwitch = true;

    private int activePlayer = 0;
    private float time;
    float oldSize;
    bool zoomPhase;

    void Start()
    {
        oldSize = sceneCam.orthographicSize;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && Time.time > time)
        {
            ZoomOut();
            activePlayer = (activePlayer + 1) % players.Count;
            time = Time.time + cooldown;
            firstSwitch = false;
        }

    }

    private void LateUpdate()
    {
        //Zoom
        if (!zoomPhase)
        {
            if (oldSize < sceneCam.orthographicSize)
            {
                sceneCam.orthographicSize -= 1 * Time.deltaTime * zoomSpeed;
            }
            else
            {
                sceneCam.orthographicSize = oldSize;
            }
        }else
        {
            if (oldSize + zoomStrengh > sceneCam.orthographicSize)
            {
                sceneCam.orthographicSize += 1 * Time.deltaTime * zoomSpeed;
            }
            else
            {
                zoomPhase = false;
            }
        }
        //Switch and Bar
        cooldownBar.transform.position = GetActivePlayer().transform.position + Vector3.up * .8f + Vector3.right * .05f + Vector3.forward * 2f;
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

    public void ZoomOut()
    {
        zoomPhase = true;
        oldSize = sceneCam.orthographicSize;
    }
}
