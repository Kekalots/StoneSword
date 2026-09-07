using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBuff : BuffBase
{
    public int tickDamage;
    public float tickInterval;
    private float tickTimer;

    public PoisonBuff(float duration, int dmg, float tickGap)
    {
        totalDuration = duration;
        currentTimer = duration;
        tickDamage = dmg;
        tickInterval = tickGap;
        tickTimer = tickGap;
        buffType= BuffType.Poison;
    }

    public override void OnApply(PlayerHealth playerHealth, PlayerMove playerMove)
    {
    }

    public override void OnUpdate(float deltaTime, PlayerHealth playerHealth, PlayerMove playerMove)
    {
        tickTimer -= deltaTime;
        if (tickTimer <= 0)
        {
            playerHealth.TakeDamage(new DamageInfo(tickDamage));
            tickTimer = tickInterval;
        }
        TickTimer(deltaTime);
    }

    public override void OnRemove(PlayerHealth playerHealth, PlayerMove playerMove)
    {
    }
}