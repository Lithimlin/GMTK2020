using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    Rigidbody2D body;
    public Transform dirTransform;
    private Animator myAnime;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        myAnime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        myAnime.SetInteger("Dir", getDirection());
        myAnime.SetBool("Move", IsMoving());
    }

    private bool IsMoving()
    {
        return body.velocity.magnitude > 0;
    }

    private int getDirection()
    {
        if (Vector2.Angle(dirTransform.right, Vector2.down) <= 45 / 2)
        {
            return 0;
        }
        else if (Vector2.Angle(dirTransform.right, Vector2.down + Vector2.right) < 45 / 2)
        {
            return 1;
        }
        else if (Vector2.Angle(dirTransform.right, Vector2.right) <= 45 / 2)
        {
            return 2;
        }
        else if (Vector2.Angle(dirTransform.right, Vector2.up + Vector2.right) < 45 / 2)
        {
            return 3;
        }
        else if (Vector2.Angle(dirTransform.right, Vector2.up) <= 45 / 2)
        {
            return 4;
        }
        else if (Vector2.Angle(dirTransform.right, Vector2.up + Vector2.left) < 45 / 2)
        {
            return 5;
        }
        else if (Vector2.Angle(dirTransform.right, Vector2.left) <= 45 / 2)
        {
            return 6;
        }
        else if (Vector2.Angle(dirTransform.right, Vector2.down + Vector2.left) < 45 / 2)
        {
            return 7;
        }
        return -1;
    }
}
