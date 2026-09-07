using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemManager : MonoBehaviour
{
    public static InventoryItemManager Instance { get; private set;}

    public int maxSlotCount = 18;
    public List<InventoryItem> itemList = new List<InventoryItem>();
    public int selectedSlotIndex = -1;

    public InventoryUI inventoryUI;
    public ItemTipUI itemTipUI;
    private PlayerHealth playerHealth;
    void Awake()
    {
        Instance = this;
        playerHealth = GetComponent<PlayerHealth>();
        if (inventoryUI == null) inventoryUI = FindObjectOfType<InventoryUI>();
        if (itemTipUI == null) itemTipUI = FindObjectOfType<ItemTipUI>();
    }

    private void Start()
    {
        if (inventoryUI != null) inventoryUI.RefreshAllUi(itemList);
        ClearSelected();
    }

    public void SelectSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= itemList.Count)
        {
            ClearSelected();
            return;
        }
        selectedSlotIndex = slotIndex;
        if (itemTipUI != null) itemTipUI.ShowTip(itemList[slotIndex]);
    }

    public void ClearSelected()
    {
        selectedSlotIndex = -1;
        if (itemTipUI != null) itemTipUI.HideTip();
    }

    public bool AddWeapon(SwordSO swordSo)
    {
        if (itemList.Count >= maxSlotCount)
        {
            return false;
        }
        itemList.Add(new InventoryItem(swordSo));
        if (inventoryUI != null) inventoryUI.RefreshAllUi(itemList);
        return true;
    }

    public bool AddHealPotion(int healAmount, int count = 1)
    {
        foreach (var item in itemList)
        {
            if (item.kind == InventoryItemKind.HealPotion && item.healValue == healAmount)
            {
                item.stackCount += count;
                if (inventoryUI != null) inventoryUI.RefreshAllUi(itemList);
                return true;
            }
        }

        if (itemList.Count >= maxSlotCount)
        {
            return false;
        }
        itemList.Add(new InventoryItem(healAmount, count));
        if (inventoryUI != null) inventoryUI.RefreshAllUi(itemList);
        return true;
    }

    public  void OnPressQ()
    {
        if (selectedSlotIndex == -1) return;

        var selectedItem = itemList[selectedSlotIndex];
        if (selectedItem.kind == InventoryItemKind.HealPotion)
        {
            playerHealth.Heal(selectedItem.healValue);
            selectedItem.stackCount--;
            if (selectedItem.stackCount <= 0)
            {
                itemList.RemoveAt(selectedSlotIndex);
                ClearSelected();
            }
            if (inventoryUI != null) inventoryUI.RefreshAllUi(itemList);
        }

        else if (selectedItem.kind == InventoryItemKind.Weapon)
        {
            SwordSO currentEquipped = WeaponNow.instance.GetCurrentWeaponSwordSo();
            if (currentEquipped != null)
            {
                AddWeapon(currentEquipped);
            }
            WeaponNow.instance.EquipWeapon(selectedItem.swordData);
            itemList.RemoveAt(selectedSlotIndex);
            ClearSelected();
            if (inventoryUI != null) inventoryUI.RefreshAllUi(itemList);
        }
    }

    public void DestroySelectedWeapon()
    {
        if (selectedSlotIndex == -1) return;
        var item = itemList[selectedSlotIndex];
        if (item.kind != InventoryItemKind.Weapon) return;

        itemList.RemoveAt(selectedSlotIndex);
        ClearSelected();
        if (inventoryUI != null) inventoryUI.RefreshAllUi(itemList);
    }
}