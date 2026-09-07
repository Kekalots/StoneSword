using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName ="NewCharacter",menuName ="CharacterInfo/BasicInfo",order =1)]
public class CharacterInfoSO : ScriptableObject
{
    public float maxHP=100;
    public float defense=10;
    public float baseAttackDamage = 10f;
    public float baseCritRate = 0.2f;
    public float baseCritDamage = 0.5f;
}