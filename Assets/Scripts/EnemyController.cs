using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform dirTransform;
    public List<Vector2> waypoints;
    public int speed;
    public float waitTime;
    public bool turnOnly;
    public bool fl1pPass = false;

    Rigidbody2D enemyBody;
    private int currentPoint;
    private bool wait;
    private float nextTime;


    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        if (!turnOnly)
        {
            waypoints.Insert(0, transform.position);
        }
        currentPoint = 0;
        wait = false;
        Vector2 target = waypoints[currentPoint];
        Vector2 pos = dirTransform.position;
        Vector2 faceDir = target - pos;
        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
        dirTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (fl1pPass)
        {
            GameObject[] passableObjects = GameObject.FindGameObjectsWithTag("Fl1pPass");
            Collider2D playerCollider = GetComponent<Collider2D>();
            foreach (GameObject obj in passableObjects)
            {
                Collider2D pass = obj.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(playerCollider, pass, true);
            }

        }

    }

    void Update()
    {
        if (turnOnly)
        {
            Turn();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (wait)
        {
            if (Time.time > nextTime)
            {
                wait = false;
            }
        }
        else
        {
            Vector2 target = waypoints[currentPoint];
            Vector2 pos = dirTransform.position;
            Vector2 faceDir = target - pos;
            float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
            dirTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            enemyBody.velocity = dirTransform.right * speed;
            if (((Vector2) dirTransform.position - target).magnitude < .1f)
            {
                enemyBody.velocity = Vector2.zero;
                wait = true;
                nextTime = Time.time + waitTime;
                currentPoint = (currentPoint + 1) % waypoints.Count;
            }
        }
    }

    private void Turn()
    {
        if (wait)
        {
            if (Time.time > nextTime)
            {
                wait = false;
            }
        }
        else
        {
            Vector2 target = waypoints[currentPoint] - (Vector2)dirTransform.position;
            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
            Quaternion rot = dirTransform.rotation;
            dirTransform.rotation = Quaternion.RotateTowards(rot, targetRot, speed * 10 * Time.deltaTime);
            if (dirTransform.rotation == targetRot)
            {
                wait = true;
                nextTime = Time.time + waitTime;
                currentPoint = (currentPoint + 1) % waypoints.Count;
            }
        }
    }
}
