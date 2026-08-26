using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCoin : MonoBehaviour,IInteractable
{
    public CoinType coinType;

    public Sprite bronzeCoinSprite;
    public Sprite silverCoinSprite;
    public Sprite goldCoinSprite;

    public int bronzeCoinValue =1;
    public int sliverCoinValue =5;
    public int goldCoinValue =20;

    public SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        RefreshCoinSprite();
    }
    private void OnValidate()
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        RefreshCoinSprite();
    }
    void RefreshCoinSprite()
    {
        if (sr == null) return;
        sr.sprite = coinType switch
        {
            CoinType.BronzeCoin => bronzeCoinSprite,
            CoinType.SilverCoin => silverCoinSprite,
            CoinType.GoldCoin => goldCoinSprite,
            _ => null
        };
    }
    public int GetCoinValue()
    {
        return coinType switch
        {
            CoinType.BronzeCoin => bronzeCoinValue,
            CoinType.SilverCoin => sliverCoinValue,
            CoinType.GoldCoin => goldCoinValue,
            _=> 0
        };
    }

    public string GetTipText()
    {
        return coinType switch
        {
            CoinType.BronzeCoin => "Press [F] To Pick Up Bronze Coin",
            CoinType.SilverCoin => "Press [F] To Pick Up Silver Coin",
            CoinType.GoldCoin => "Press [F] To Pick Up Gold Coin",
            _ => ""
        };
    }

    public void Interact()
    {
        int coinValue = GetCoinValue();
        CoinManager.instance.AddCoin(coinValue);
        Destroy(gameObject);
    }
}

public enum CoinType
{
    BronzeCoin,
    SilverCoin,
    GoldCoin,
}
