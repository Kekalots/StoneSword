using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamagable
{
    public CharacterInfoSO enemyInfo;
    public float currentHP;

    void Start()
    {
        currentHP = enemyInfo.maxHP;
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        currentHP =currentHP- ((damageInfo.damage - enemyInfo.defense) <= 0 ? 0 : damageInfo.damage - enemyInfo.defense);
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath();
        }
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }
}

