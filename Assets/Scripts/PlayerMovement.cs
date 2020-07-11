using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform dirTransform;
    public CharacterSwitcher characterManager;
    public bool inControl;

    Rigidbody2D playerBody;
    public int moveSpeed = 6;
    public float idleSpeedFactor = .8f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterManager.getActivePlayer() == gameObject)
        {
            UpdateFacing();
            Move();
        } 
        else
        {
            MoveFacing();
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
}
