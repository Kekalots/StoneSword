using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSlimeAttack : EnemyBaseAttack
{
    public Sprite poisonBuffSprite;
    protected override void TriggerAttack()
    {
        isAttacking = true;
        contactTimer = 0;
        DealDamageToPlayer();
        enemyAnim.SetTrigger("PoisonSlimeAttack");
        BuffManager buffMgr = playerObj.GetComponent<BuffManager>();
        if (buffMgr != null)
        {
            PoisonBuff poison = new PoisonBuff(3.2f, 20, 1.5f);
            poison.buffSprite = poisonBuffSprite;
            buffMgr.AddBuff(poison);
        }
    }

    public void AttackAnimationFinished()
    {
        isAttacking = false;
        cdTimer = attackCoolDown;
    }
}
