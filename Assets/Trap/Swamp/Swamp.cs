using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Swamp : MonoBehaviour
{
    public Sprite poisonBuffSprite;
    public Sprite slowDownBuffSprite;
    public float damageTime = 1.5f;
    private float t;
    private const float slowBuffDuration = 1.7f;
    private const float slowMoveScale = 0.5f;
    private const float slowJumpScale = 0.5f;
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            BuffManager buffMgr = other.GetComponent<BuffManager>();
            t += Time.fixedDeltaTime;
            if (t>= damageTime)
            {
                playerHealth.TakeDamage(new DamageInfo(15f));
                t = 0;
                if (buffMgr != null)
                {
                    SlowDownBuff slowBuff = new SlowDownBuff(slowBuffDuration, slowMoveScale, slowJumpScale);
                    slowBuff.buffSprite = slowDownBuffSprite;
                    buffMgr.AddBuff(slowBuff);
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            t = 0;
            BuffManager buffMgr = other.GetComponent<BuffManager>();
            if (buffMgr != null)
            {
                PoisonBuff poison = new PoisonBuff(3.2f, 20, 1.5f);
                poison.buffSprite = poisonBuffSprite;
                buffMgr.AddBuff(poison);
            }
        }
    }
}