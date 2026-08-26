using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseAttack : MonoBehaviour
{
    public CharacterInfoSO enemySO;
    public float contactRequiredTime = 1f;
    public float attackCoolDown = 1f;

    public Animator enemyAnim;
    public Collider2D attackHitbox;
    public Collider2D detectRangeCollider;

    protected EnemyBaseMove enemyMove;
    protected Rigidbody2D rb;
    protected GameObject playerObj;

    protected bool playerInDetectRange = false;
    protected bool isAttacking;
    protected float contactTimer;
    protected float cdTimer;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMove = GetComponent<EnemyBaseMove>();
        if (attackHitbox != null)
        {
            attackHitbox.enabled = false;
        }
    }

    protected virtual void Update()
    {
        if (cdTimer > 0)
            cdTimer -= Time.deltaTime;

        if (playerInDetectRange || isAttacking)
        {
            enemyMove.shouldStopPatrol = true;
        }
        else
        {
            enemyMove.shouldStopPatrol = false;
        }

        if (playerInDetectRange && !isAttacking && cdTimer <= 0)
        {
            contactTimer += Time.deltaTime;
            if (contactTimer >= contactRequiredTime)
            {
                TriggerAttack();
            }
        }
        else
        {
            contactTimer = 0;
        }
    }

    protected abstract void TriggerAttack();

    public void OnPlayerEnterDetect(GameObject player)
    {
        playerInDetectRange = true;
        playerObj = player;
    }

    public void OnPlayerExitDetect()
    {
        playerInDetectRange = false;
        contactTimer = 0;
    }

    public void DealDamageToPlayer()
    {
        if (playerObj == null || enemySO == null) return;
        if (playerObj.TryGetComponent(out IDamagable player))
        {
            DamageInfo dmg = new DamageInfo(enemySO.baseAttackDamage, enemySO.baseCritRate, enemySO.baseCritDamage);
            player.TakeDamage(dmg);
        }
    }
}

