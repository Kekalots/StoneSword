using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamagable
{
    public PlayerNow playernow;
    public float currentHP;
    public Animator anim;
    public CameraFollow cameraFollow;
    private bool isDead = false;
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
        if (isDead) return;   //死亡后不再受击，避免重复触发抖动/死亡流程
        currentHP=currentHP-((damageInfo.damage-playernow.defense)<=0?0: damageInfo.damage - playernow.defense);
        anim.SetTrigger("TakeDamage");
        cameraFollow.Shake();
        if (SoundManager.Instance != null) SoundManager.Instance.PlayHurt();
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath();
        }
    }
    public void OnDeath()
    {
        if (isDead) return;
        isDead = true;
        if (DeathUI.Instance != null)
        {
            DeathUI.Instance.ShowDeath();
        }
        else
        {
            Debug.LogWarning("[PlayerHealth] 找不到 DeathUI，玩家死亡但无界面");
        }
    }
    public void Heal(int healValue)
    {
        currentHP = Mathf.Clamp(currentHP + healValue, 0, playernow.maxHP);
    }
}