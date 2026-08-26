using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItemKind
{
    Weapon,
    HealPotion
}

public class InventoryItem
{
    public InventoryItemKind kind;
    public SwordSO swordData;
    public int healValue;
    public int stackCount;

    public InventoryItem(SwordSO sword)
    {
        kind = InventoryItemKind.Weapon;
        swordData = sword;
        stackCount = 1;
    }

    public InventoryItem(int healAmount, int count = 1)
    {
        kind = InventoryItemKind.HealPotion;
        healValue = healAmount;
        stackCount = count;
    }
}
