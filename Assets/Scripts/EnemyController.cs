using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Vector2> waypoints;
    public int moveSpeed;

    int currentPoint;

    void Start()
    {
        waypoints.Insert(0, transform.position);
        currentPoint = 1;
    }

    void FixedUpdate()
    {
        Patrol();
        Debug.Log(transform.position);
    }

    private void Patrol()
    {
        Vector2 target = waypoints[currentPoint];
        transform.LookAt(target);
        transform.position = Vector2.MoveTowards(transform.position, target, .2f);
        if (transform.position.Equals(target))
        {
            currentPoint = (currentPoint + 1) % waypoints.Count;
        }
    }
}
