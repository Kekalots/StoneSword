using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : EnemyBaseAttack
{
    protected override void TriggerAttack()
    {
        isAttacking = true;
        contactTimer = 0;
        DealDamageToPlayer();
        enemyAnim.SetTrigger("SlimeAttack");
    }

    public void AttackAnimationFinished()
    {
        isAttacking = false;
        cdTimer = attackCoolDown;
    }
}
