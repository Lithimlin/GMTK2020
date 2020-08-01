using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform dirTransform;
    public CharacterSwitcher characterManager;
    public bool inControl;

    Rigidbody2D playerBody;
    public int moveSpeed = 6;
    public float idleSpeedFactor = .8f;
    public bool fl1pPass = false;
    private float flipCooldown = .5f;
    private float flipTime = 0f;
    private bool bounce = true;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();

        if (fl1pPass)
        {
            GameObject[] passableObjects = GameObject.FindGameObjectsWithTag("Fl1pPass");
            Collider2D playerCollider = GetComponent<Collider2D>();
            foreach (GameObject obj in passableObjects)
            {
                Collider2D target = obj.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(playerCollider, target, true);
            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterManager.GetActivePlayer() == gameObject)
        {
            UpdateFacing();
            Move();
        } 
        else
        {
            if(!characterManager.firstSwitch)
            {
                MoveFacing();
            }
        }
    }

    private void Move()
    {
        playerBody.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);
    }

    private void MoveFacing()
    {
        playerBody.velocity = dirTransform.right * moveSpeed * idleSpeedFactor;
    }

    private void UpdateFacing()
    {
        Vector2 faceDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;

        dirTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!bounce)
        {
            return;
        }
        flipTime -= Time.deltaTime;
        if (characterManager.GetActivePlayer() == gameObject && flipTime <= 0)
        {
            return;
        }
        if (collision.gameObject.layer == 9 && (tag != "Br4hms" || collision.gameObject.tag != "Box"))
        {
            dirTransform.right *= -1;
            //flipTime = flipCooldown;
            bounce = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bounce = true;
    }
}
