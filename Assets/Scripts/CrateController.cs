using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;
    public CameraScript sceneCam;

    public float overlap;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x < -0.3F || rb.velocity.x > 0.3F || rb.velocity.y < -0.3F || rb.velocity.y > 0.3F)
        {
            sceneCam.Shake();
        }else
        {
            sceneCam.StopShake();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Br4hms"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Br4hms"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
