using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseMove : MonoBehaviour
{
    public bool shouldStopPatrol = false;


    protected Rigidbody2D rb;
    protected GameObject playerGo;
    protected Transform playerTrans;

    protected Vector3 originLocalScale;
    protected Vector2 startPos;

    protected int moveDir = 1;
    public float moveSpeed;
    public float patrolRange;
    public bool doPatrol = true;
    
    public float visionRadius;
    public LayerMask obstacleLayer=1<<6;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerGo = GameObject.FindGameObjectWithTag("Player");
        if (playerGo != null)
        {
            playerTrans = playerGo.transform;
        }
        originLocalScale = transform.localScale;
        startPos = transform.position;
    }

    protected virtual void FixedUpdate()
    {
        if (shouldStopPatrol)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        if (doPatrol)
        {
            PatrolMove();
        }
    }

    protected void PatrolMove()
    {
        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
        float offsetX = transform.position.x - startPos.x;

        if (moveDir == 1)
        {
            if (offsetX >= patrolRange)
                moveDir = -1;
        }
        else
        {
            if (offsetX <= -patrolRange)
                moveDir = 1;
        }

        transform.localScale = new Vector3(originLocalScale.x * moveDir, originLocalScale.y, originLocalScale.z);
    }

    protected bool CanSeePlayer()
    {
        if (playerTrans == null) return false;

        Vector2 start = transform.position;
        Vector2 end = playerTrans.position;
        RaycastHit2D ray = Physics2D.Raycast(start, end - start, visionRadius, obstacleLayer);
        if (ray.collider != null) return false;

        float distance = Mathf.Abs(playerTrans.position.x - transform.position.x);
        return distance <= visionRadius;
    }
}
