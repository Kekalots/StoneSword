using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Sprite poisonBuffImage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth playerHealth=other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(new DamageInfo(20f));
            BuffManager buffMgr = other.gameObject.GetComponent<BuffManager>();
            if (buffMgr != null)
            {
                SlowDownBuff slowBuff = new SlowDownBuff(1f, 0.5f, 0.5f);
                slowBuff.buffSprite = poisonBuffImage;
                buffMgr.AddBuff(slowBuff);
            }
        }
    }
}