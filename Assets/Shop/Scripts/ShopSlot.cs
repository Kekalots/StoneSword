using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;

    public int index;
    public ShopUI shopUI;

    public void SetData(Sprite icon, string displayName, int slotIndex)
    {
        index = slotIndex;
        iconImage.enabled = true;
        iconImage.sprite = icon;
        nameText.text = displayName;
    }

    public void OnShopSlotClick()
    {
        shopUI?.OnSlotClicked(index);
    }
}