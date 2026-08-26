using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNow : MonoBehaviour
{
    public static WeaponNow instance;
    public SwordSO defaultWeapon;
    public  SpriteRenderer swordSR;
    public SwordSO currentSword {  get; private set; }
    private void Awake()
    {
        instance = this;
        EquipWeapon(defaultWeapon);
    }
    public void EquipWeapon(SwordSO newSword)
    {
        currentSword = newSword;
        RefreshDisplay(newSword);
        
    }
    public void RefreshDisplay(SwordSO newSword)
    {
        swordSR.sprite = newSword.swordSprite;
    }
    public SwordSO GetCurrentWeaponSwordSo()
    {
        return currentSword;
    }
}
