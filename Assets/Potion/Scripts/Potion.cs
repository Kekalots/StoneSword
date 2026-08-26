using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class Potion : MonoBehaviour,IInteractable
{
    [Header("“©ÀÆ¿‡–Õ")]
    public PotionType potionType;

    public int healValue=20;

    public float defenseAddValue=5;

    public Sprite healPotionSprite;
    public Sprite defensePotionSprite;
    public SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        RefreshPotionSprite();
    }
    private void OnValidate()
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        RefreshPotionSprite();
    }
    void RefreshPotionSprite()
    {
        if (sr == null) return;
        sr.sprite = potionType switch
        {
            PotionType.Heal => healPotionSprite,
            PotionType.Defense => defensePotionSprite,
            _ => null
        };
    }
    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        switch (potionType)
        {
            case PotionType.Heal:
                {
                    InventoryItemManager.Instance.AddHealPotion(healValue);
                    Destroy(gameObject);
                    break;
                }
            case PotionType.Defense:
                {
                    DoPotionEffect(player);
                    Destroy(gameObject);
                    break;
                }
            default: break;
        }
    }

    public string GetTipText()
    {
        return potionType switch
        {
            PotionType.Heal => "Press [F] To Pick Up Heal Potion",
            PotionType.Defense => "Press [F] To Pick Up Defense Potion",
            _ => ""
        };
    }

    private void DoPotionEffect(GameObject player)
    {
        switch (potionType)
        {
            case PotionType.Heal:
                {
                    PlayerHealth ph = player.GetComponent<PlayerHealth>();
                    if (ph != null)
                    {
                        ph.Heal(healValue);
                    }
                    break;
                }
            case PotionType.Defense:
                {
                    PlayerNow playerNow = player.GetComponent<PlayerNow>();
                    if (playerNow != null)
                    {
                        playerNow.defense += defenseAddValue;
                    }
                    break;
                }
            default:break;
        }
    }
}

public enum PotionType
{
    Heal,
    Defense
}
