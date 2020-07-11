using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Vector2> waypoints;
    public int speed;
    public float waitTime;
    public bool turnOnly;

    private int currentPoint;
    private bool wait;
    private float nextTime;

    void Start()
    {
        if (!turnOnly)
        {
            waypoints.Insert(0, transform.position);
        }
        currentPoint = 1;
        wait = false;
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
            Vector2 pos = transform.position;
            Vector2 faceDir = target - pos;
            float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.Equals(target))
            {
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
            Vector2 target = waypoints[currentPoint] - (Vector2)transform.position;
            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.RotateTowards(rot, targetRot, speed * 10 * Time.deltaTime);
            if (transform.rotation == targetRot)
            {
                wait = true;
                nextTime = Time.time + waitTime;
                currentPoint = (currentPoint + 1) % waypoints.Count;
            }
        }
    }
}
