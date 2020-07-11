using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Vector2> waypoints;
    public int moveSpeed;
    public float waitTime;

    private int currentPoint;
    private bool wait;
    private float nextTime;

    void Start()
    {
        waypoints.Insert(0, transform.position);
        currentPoint = 1;
        wait = false;
    }

    void Update()
    {
        Patrol();
        Debug.Log(transform.position);
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
            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.back);
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            if (transform.position.Equals(target))
            {
                wait = true;
                nextTime = Time.time + waitTime;
                currentPoint = (currentPoint + 1) % waypoints.Count;
            }
        }
    }
}
