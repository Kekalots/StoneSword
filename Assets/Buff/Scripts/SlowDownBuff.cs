using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownBuff : BuffBase
{
    public float moveSpeedScale;
    public float jumpForceScale;

    private float originMoveSpeed;
    private float originJumpForce;

    public SlowDownBuff(float duration, float moveScale, float jumpScale)
    {
        totalDuration = duration;
        currentTimer = duration;
        moveSpeedScale = moveScale;
        jumpForceScale = jumpScale;
        buffType = BuffType.SlowDown;
    }

    public override void OnApply(PlayerHealth playerHealth, PlayerMove playerMove)
    {
        originMoveSpeed = playerMove.speed;
        originJumpForce = playerMove.jumpForce;

        playerMove.speed *= moveSpeedScale;
        playerMove.jumpForce *= jumpForceScale;
    }

    public override void OnUpdate(float deltaTime, PlayerHealth playerHealth, PlayerMove playerMove)
    {
        TickTimer(deltaTime);
    }

    public override void OnRemove(PlayerHealth playerHealth, PlayerMove playerMove)
    {
        playerMove.speed = originMoveSpeed;
        playerMove.jumpForce = originJumpForce;
    }
}