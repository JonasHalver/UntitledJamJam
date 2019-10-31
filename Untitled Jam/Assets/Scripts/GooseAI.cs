using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GooseAI : MonoBehaviour
{
    public GameObject player;
    public Seeker seeker;
    public Rigidbody2D rb;

    [Range(1,10)]
    public float playerRadius = 3;
    public static float acceptableDistanceToPlayer = 3;

    public float moveSpeed = 2, moveSpeedMod = 1;
    public float maxWalkDistance = 10;

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

    bool fleeing;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        maxWalkDistance = (acceptableDistanceToPlayer * 2) - 0.1f;
    }

    private void FixedUpdate()
    {
        acceptableDistanceToPlayer = playerRadius;
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
                    if (Random.Range(0, 10) == 0)
                        currentState = State.Scaring;
                    else
                        CreateIdleDestination();                    
                }
                if (canAttack)
                {
                    if ((transform.position - player.transform.position).sqrMagnitude < acceptableDistanceToPlayer * acceptableDistanceToPlayer)
                        AttackCheck();
                }
                else if (!fleeing)
                    if ((transform.position - player.transform.position).sqrMagnitude < acceptableDistanceToPlayer * acceptableDistanceToPlayer)
                    {
                        path = null;
                        Flee();
                        StartCoroutine(FleeDelay());
                    }

                break;
            case State.Attacking:
                if (path == null)
                    CreateAttackDestination();
                break;
            case State.Scaring:
                if (path == null)
                    CreateScareDestination();
                if (canAttack)
                    if ((transform.position - player.transform.position).sqrMagnitude < acceptableDistanceToPlayer * acceptableDistanceToPlayer)
                        AttackCheck();
                break;
        }

        if ((transform.position - player.transform.position).sqrMagnitude < acceptableDistanceToPlayer)
            moveSpeedMod = 3;
        else
            moveSpeedMod = 1;
    }

    public void CreateIdleDestination()
    {
        Vector2 newPos = rb.position;
        for (int i = 0; i < 1000; i++)
        {
            newPos = rb.position + Random.insideUnitCircle * maxWalkDistance;

            if (PathNotNearPlayer(newPos) && PathNotFarFromPlayer(newPos))
                break;
        }
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, newPos, OnPathCompleteIdle);
            canAttack = true;
        }
    }

    IEnumerator FleeDelay()
    {
        fleeing = true;
        yield return new WaitForSecondsRealtime(0.5f);
        fleeing = false;
    }

    public void Flee()
    {
        Vector2 newPos = Vector2.zero;

        newPos = ((rb.position - (Vector2)player.transform.position).normalized * acceptableDistanceToPlayer * 2) + Random.insideUnitCircle * 3;

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, newPos, OnPathCompleteAttack);
            currentState = State.Roaming;
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
    
    void CreateScareDestination()
    {
        Vector2 newPos = (rb.position - (Vector2)player.transform.position).normalized * (acceptableDistanceToPlayer - 1) + Random.insideUnitCircle * 2;
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
            path = null;
            currentState = State.Attacking;
            
            canAttack = false;
        }
        else
        {
            path = null;
            canAttack = false;
            Flee();
        }
    }

    bool PathNotNearPlayer(Vector3 destination)
    {
        if ((destination - player.transform.position).sqrMagnitude < acceptableDistanceToPlayer * acceptableDistanceToPlayer)
            return false;
        else
            return true;
    }

    bool PathNotFarFromPlayer(Vector3 destination)
    {
        if ((destination - player.transform.position).sqrMagnitude < (acceptableDistanceToPlayer * acceptableDistanceToPlayer)* 4)
            return true;
        else
            return false;
    }

    public void IdleMovement()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * moveSpeedMod * Time.fixedDeltaTime);
        rb.SetRotation(0);
    }
}
