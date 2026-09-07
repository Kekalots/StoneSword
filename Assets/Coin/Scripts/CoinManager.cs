using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public TMP_Text moneyText;
    public int money;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (moneyText == null)
        {
            GameObject go = GameObject.Find("MoneyText");
            if (go != null) moneyText = go.GetComponent<TMP_Text>();
        }
    }

    public void AddCoin(int value)
    {
        money += value;
        RefreshCoinUI();
    }

    public bool Spend(int value)
    {
        if (money < value) return false;
        money -= value;
        RefreshCoinUI();
        return true;
    }

    public void SetMoney(int value)
    {
        money = value;
        RefreshCoinUI();
    }

    void RefreshCoinUI()
    {
        moneyText.text = $": {money}";
    }
}