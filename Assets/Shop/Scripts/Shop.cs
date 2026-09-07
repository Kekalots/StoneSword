using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("固定商品（不会随机、不会补货）")]
    public List<SwordSO> weaponList = new List<SwordSO>();
    public int healAmount = 20;
    public int healCount = 1;
    public int healPrice = 50;

    [Header("商店界面")]
    public ShopUI shopUI;

    private List<InventoryItem> goods = new List<InventoryItem>();

    private int visitTimes = 0;

    public void OpenShop()
    {
        if (shopUI == null)
        {
            Debug.LogWarning("[Shop] 未绑定 shopUI，商店无法打开");
            return;
        }

        if (visitTimes == 0)
        {
            ResetGoods();
            visitTimes++;
        }

        shopUI.RefreshGoods(goods);
        shopUI.SetPanelOpen(true);
        shopUI.ShowTip("");
    }

    public void CloseShop()
    {
        if (shopUI == null) return;
        shopUI.SetPanelOpen(false);
    }

    public bool TryBuy(int goodsIndex)
    {
        if (goodsIndex < 0 || goodsIndex >= goods.Count) return false;
        InventoryItem item = goods[goodsIndex];
        int price = GetItemPrice(item);

        CoinManager cm = CoinManager.instance;
        if (cm == null)
        {
            return false;
        }
        if (!cm.Spend(price))
        {
            shopUI.ShowTip($"Not enough coins! Need {price}, you have {cm.money}.");
            return false;
        }

        InventoryItemManager inv = InventoryItemManager.Instance;
        if (inv != null)
        {
            bool added;
            if (item.kind == InventoryItemKind.Weapon)
                added = inv.AddWeapon(item.swordData);
            else
                added = inv.AddHealPotion(item.healValue, item.stackCount);

            if (!added)
            {
                cm.AddCoin(price);
                shopUI.ShowTip("Inventory full! Purchase failed. Coins refunded.");
                Debug.Log("[Shop] 背包已满，已退款");
                return false;
            }
        }

        goods.RemoveAt(goodsIndex);
        shopUI.RefreshGoods(goods);
        shopUI.ShowTip($"Purchased {GetItemName(item)}! {price} coins spent. {goods.Count} left.");
        return true;
    }

    private int GetItemPrice(InventoryItem item)
    {
        if (item.kind == InventoryItemKind.Weapon && item.swordData != null)
        {
            return item.swordData.price;
        }
        return healPrice;
    }

    private string GetItemName(InventoryItem item)
    {
        if (item.kind == InventoryItemKind.Weapon && item.swordData != null)
        {
            return item.swordData.swordName;
        }
        return "HealPotion";
    }

    public int GetVisitTimes() => visitTimes;

    public List<string> GetRemainingNames()
    {
        List<string> names = new List<string>();
        foreach (InventoryItem item in goods)
        {
            names.Add(GetItemName(item));
        }
        return names;
    }

    public void RestoreState(int visits, List<string> remaining)
    {
        visitTimes = visits;
        goods.Clear();
        if (remaining == null) return;
        foreach (string name in remaining)
        {
            if (name == "HealPotion")
            {
                goods.Add(new InventoryItem(healAmount, healCount));
            }
            else
            {
                SwordSO sword = FindSwordByName(name);
                if (sword != null) goods.Add(new InventoryItem(sword));
            }
        }
    }

    private SwordSO FindSwordByName(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;
        foreach (SwordSO sword in weaponList)
        {
            if (sword != null && sword.swordName == name) return sword;
        }
        return null;
    }

    private void ResetGoods()
    {
        goods.Clear();
        foreach (SwordSO sword in weaponList)
        {
            if (sword != null) goods.Add(new InventoryItem(sword));
        }
        goods.Add(new InventoryItem(healAmount, healCount));
    }
}