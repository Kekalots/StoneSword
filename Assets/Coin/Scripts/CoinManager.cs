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
    }

    public void AddCoin(int value)
    {
        money += value;
        RefreshCoinUI();
    }

    void RefreshCoinUI()
    {
        moneyText.text = $": {money}";
    }
}
