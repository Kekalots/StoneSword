using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamagable
{
    public PlayerNow playernow;
    public float currentHP;
    public Animator anim;
    public CameraFollow cameraFollow;
    private void Awake()
    {
        playernow = GetComponent<PlayerNow>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHP = playernow.maxHP;
    }
    public void TakeDamage(DamageInfo damageInfo)
    {
        currentHP=currentHP-((damageInfo.damage-playernow.defense)<=0?0: damageInfo.damage - playernow.defense);
        anim.SetTrigger("TakeDamage");
        cameraFollow.Shake();
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath();
        }
    }
    public void OnDeath()
    {

    }
    public void Heal(int healValue)
    {
        currentHP = Mathf.Clamp(currentHP + healValue, 0, playernow.maxHP);
    }
}
