using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;

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
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, coll.bounds.size * overlap, transform.eulerAngles.z);
        if ((hits.Length > 0) && hits.Any(e => e.tag == "Br4hms")) 
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
