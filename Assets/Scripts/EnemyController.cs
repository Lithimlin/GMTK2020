using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Vector2> walkPoints;
    public int moveSpeed;

    Rigidbody2D myBody;
    int currentPoint;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        walkPoints[0] = transform.position;
        currentPoint = 1;
    }

    void Update()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPos = walkPoints[currentPoint] - currentPos;
        myBody.velocity = ((targetPos).normalized * moveSpeed);
    }
}
