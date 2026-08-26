using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNow : MonoBehaviour
{
    public CharacterInfoSO playerInfoSO;
    public WeaponNow weaponNow;

    public float maxHP;
    public float defense;
    private void Awake()
    {
        maxHP = playerInfoSO.maxHP;
        defense= playerInfoSO.defense;
        weaponNow=GetComponentInChildren<WeaponNow>();
    }

    public float attackDamage
    {
        get
        {
            float damage = playerInfoSO.baseAttackDamage + weaponNow.currentSword.swordAttackDamage;
            return damage;
        }
    }
    public float critRate
    {
        get
        {
            float critrate = playerInfoSO.baseCritRate + weaponNow.currentSword.swordCritRate;
            return critrate;
        }
    }
    public float critDamage
    {
        get
        {
            float critdamage = playerInfoSO.baseCritDamage + weaponNow.currentSword.swordCritDamage;
            return critdamage;
        }
    }
}

