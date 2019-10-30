using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GooseAI : MonoBehaviour
{
    public GameObject player;
    public Seeker seeker;
    public Rigidbody2D rb;

    public float acceptableDistanceToPlayer = 3;

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
    public State currentState = State.Roaming;
    bool canAttack;

    public AudioSource aS;
    public AudioClip honk;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
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
        

        switch (currentState)
        {
            case State.Roaming:
                if (path == null)
                {
                    CreateIdleDestination();                    
                }
                if (canAttack)
                    if ((transform.position - player.transform.position).sqrMagnitude < acceptableDistanceToPlayer * acceptableDistanceToPlayer)
                        AttackCheck();
                break;
            case State.Attacking:
                if (path == null)
                    CreateAttackDestination();
                break;
            case State.Scaring:
                break;
        }
    }

    public void CreateIdleDestination()
    {
        Vector2 newPos = rb.position;
        for (int i = 0; i < 1000; i++)
        {
            newPos = rb.position + Random.insideUnitCircle * 4;

            if (PathNotNearPlayer(newPos))
                break;
        }
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, newPos, OnPathCompleteIdle);
            canAttack = true;
        }
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

    void CreateAttackDestination()
    {
        Vector2 newPos = player.transform.position;
        if (seeker.IsDone())
            seeker.StartPath(rb.position, newPos, OnPathCompleteAttack);
    }

    void OnPathCompleteAttack(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void AttackCheck()
    {
        aS.pitch = 1;
        aS.pitch += Random.Range(-0.2f, 0.2f);
        aS.PlayOneShot(honk);
        
        if (Random.Range(0, 10) == 0)
        {
            currentState = State.Attacking;
        }
        else
        {
            path = null;
            canAttack = false;
        }
    }

    bool PathNotNearPlayer(Vector3 destination)
    {
        if (Vector3.Distance(destination, player.transform.position) < acceptableDistanceToPlayer)
            return false;
        else
            return true;
    }

    public void IdleMovement()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        rb.SetRotation(0);
    }
}
