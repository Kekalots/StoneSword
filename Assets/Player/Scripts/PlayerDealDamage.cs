using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDealDamage : MonoBehaviour
{
    public PlayerNow playerNow;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            DamageInfo info = new DamageInfo(playerNow.attackDamage, playerNow.critRate, playerNow.critDamage);
            damagable.TakeDamage(info);
        }
    }
}
