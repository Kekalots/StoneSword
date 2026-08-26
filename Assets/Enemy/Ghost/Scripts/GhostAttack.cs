using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : EnemyBaseAttack
{
    private void Start()
    {
        contactRequiredTime = 0.3f;
        attackCoolDown = 0.5f;
    }
    protected override void TriggerAttack()
    {
        isAttacking = true;
        contactTimer = 0;
        DealDamageToPlayer();
        enemyAnim.SetTrigger("GhostAttack");
    }

    public void AttackAnimationFinished()
    {
        isAttacking = false;
        cdTimer = attackCoolDown;
    }
}
