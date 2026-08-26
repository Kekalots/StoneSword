using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public float damage;
    public bool isCrit;

    public DamageInfo(float attackDamage, float critRate, float critDamage)
    {
        isCrit = Random.value < critRate;
        if (isCrit)
        {
            damage = attackDamage * (1f + critDamage);
        }
        else
        {
            damage = attackDamage;
        }
    }

    public DamageInfo(float trapDamage)
    {
        damage = trapDamage;
        isCrit = false;
    }
}

