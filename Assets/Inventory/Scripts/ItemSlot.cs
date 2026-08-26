using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text stackText;
    public Sprite potionSprite;

    private int slotIndex;

    public void SetData(InventoryItem item, int index)
    {
        slotIndex = index;
        iconImage.enabled = true;

        if (item.kind == InventoryItemKind.Weapon)
        {
            iconImage.sprite = item.swordData.swordSprite;
            stackText.text = "1";
        }
        else if (item.kind == InventoryItemKind.HealPotion)
        {
            iconImage.sprite = potionSprite;
            stackText.text = item.stackCount > 1 ? item.stackCount.ToString() : "1";
        }
    }

    public void SetEmpty()
    {
        iconImage.enabled = false;
        stackText.text = "";
    }

    public void OnSlotClick()
    {
        InventoryItemManager.Instance.SelectSlot(slotIndex);
    }
}
