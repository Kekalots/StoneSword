using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTipUI : MonoBehaviour
{
    public GameObject tipPanel;

    public TMP_Text nameText;
    public TMP_Text descText;

    private void Awake()
    {
        tipPanel.SetActive(false);
    }

    public void ShowTip(InventoryItem item)
    {
        if (item == null)
        {
            HideTip();
            return;
        }
        tipPanel.SetActive(true);

        if (item.kind == InventoryItemKind.Weapon)
        {
            nameText.text = item.swordData.swordName;
            descText.text = $"AttackDamage: {item.swordData.swordAttackDamage}  CritRate: {item.swordData.swordCritRate}  CritDamage: {item.swordData.swordCritDamage}";
        }
        else if (item.kind == InventoryItemKind.HealPotion)
        {
            nameText.text = "HealPotion";
            descText.text = $"Restores {item.healValue} Current HP";
        }
    }

    public void HideTip()
    {
        tipPanel.SetActive(false);
    }
}
