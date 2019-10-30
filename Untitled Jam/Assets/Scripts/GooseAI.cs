using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GooseAI : MonoBehaviour
{
    public Seeker seeker;
    public Rigidbody2D rb;

    public float moveSpeed = 2;

    Vector2 lastPos = new Vector2();

    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath;
    float nextWayPointDistance = .5f;
    Vector2 direction;
    bool pause;
    float prevMoveSpeed;

    public enum State { Roaming, Scaring, Attacking }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (path != null)
        {
            IdleMovement();
            if (currentWayPoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                path = null;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
            if (distance < nextWayPointDistance)
            {
                currentWayPoint++;
            }
        }
        else
        {
            CreateIdleDestination();
        }

    }

    public void CreateIdleDestination()
    {
        Vector2 newPos = Random.insideUnitCircle * 8;
        if (seeker.IsDone())
            seeker.StartPath(rb.position, newPos, OnPathCompleteIdle);
    }

    void OnPathCompleteIdle(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
        else
        {
            path = null;
            currentWayPoint = 0;
        }
    }

    public void IdleMovement()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        rb.SetRotation(0);
    }
}
