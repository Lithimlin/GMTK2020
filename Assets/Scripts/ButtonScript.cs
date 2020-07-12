using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public LayerMask targetMask;
    public int radius;
    SpriteRenderer myRender;
    public Sprite pressedSprite;
    public List<Transform> playerTargets = new List<Transform>();
    public bool isButton;
    public GameObject target;
    public LevelManager manager;
    private void Start()
    {
        myRender = GetComponent<SpriteRenderer>();
        StartCoroutine("FindTargetsWithDelay", .2F);
    }



    private void CheckButton()
    {
        Collider2D[] targetInViewRadius = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
        foreach (Collider2D collider in targetInViewRadius)
        {
            Transform target = collider.GetComponent<Collider2D>().transform;
            playerTargets.Add(target);
       }
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            CheckButton();
            if (playerTargets.Count > 0)
            {
                if (isButton)
                {
                    myRender.sprite = pressedSprite;
                    Destroy(target.gameObject);
                }
                else
                {
                    manager.cases.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
