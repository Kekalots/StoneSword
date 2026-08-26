using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewSword",menuName="Weapon/Swords",order=2)]
public class SwordSO : ScriptableObject
{
    public string swordName;
    public int swordAttackDamage;
    public float swordCritRate; 
    public float swordCritDamage;
    public Sprite swordSprite;
}
