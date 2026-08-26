
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBase
{
    public BuffType buffType;
    public Sprite buffSprite;
    public BuffImageUI buffImageUI;
    public float totalDuration;
    public float currentTimer;
    public bool isFinished => currentTimer <= 0;

    public abstract void OnApply(PlayerHealth playerHealth, PlayerMove playerMove);

    public abstract void OnUpdate(float deltaTime, PlayerHealth playerHealth, PlayerMove playerMove);

    public abstract void OnRemove(PlayerHealth playerHealth, PlayerMove playerMove);

    public virtual void TickTimer(float deltaTime)
    {
        currentTimer -= deltaTime;
        if (currentTimer < 0) currentTimer=0;
    }
}

public enum BuffType
{
    Poison,
    SlowDown,
}

