using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform dirTransform;
    public CharacterSwitcher characterManager;
    public GameObject partical;

    Rigidbody2D playerBody;
    public int moveSpeed = 6;
    public float idleSpeedFactor = .8f;
    public float slowSpeedFactor = .5f;
    public bool fl1pPass = false;
    private bool bounce = true;
    private bool startParticle;
    private bool slowMovement = false;

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
            if (!startParticle)
            {
                startParticle = true;
                partical.SetActive(true);
                StartCoroutine(StopParticle());
            }
            UpdateFacing();
            Move();
        } 
        else
        {
            if (startParticle)
            {
                startParticle = false;
            }
                if (!characterManager.firstSwitch)
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
        if(slowMovement)
        {
            playerBody.velocity *= slowSpeedFactor;
        }
    }

    private void UpdateFacing()
    {
        Vector2 faceDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;

        dirTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!bounce || characterManager.GetActivePlayer() == gameObject)
        {
            return;
        }

        if ((collision.gameObject.layer == 9 || collision.gameObject.layer == 8) 
            && ((CompareTag("Br4hms") && !collision.gameObject.CompareTag("Box")) || !CompareTag("Br4hms")))
        {
            dirTransform.right *= -1;
            bounce = false;
        }
        else if (CompareTag("Br4hms") && collision.gameObject.CompareTag("Box"))
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < .1f)
            {
                dirTransform.right *= -1;
                bounce = false;
            }
            slowMovement = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bounce = true;
        if (CompareTag("Br4hms") && collision.gameObject.CompareTag("Box"))
        {
            slowMovement = false;
        }
    }

    IEnumerator StopParticle()
    {
        yield return new WaitForSeconds(3);
        partical.SetActive(false);
    }
}
