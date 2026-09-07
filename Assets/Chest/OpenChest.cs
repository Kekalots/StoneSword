using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour,IInteractable
{
    public ChestType chestType;

    [Header("宝箱图片")]
    public Sprite normalClosed;
    public Sprite normalOpened;

    public Sprite goldenClosed;
    public Sprite goldenOpened;

    public Sprite cursedClosed;
    public Sprite cursedOpened;

    [Header("掉落预制体")]
    public List<GameObject> coinPrefabs;
    public List<GameObject> weaponPrefabs;
    public List<GameObject> potionPrefabs;

    [Header("生成偏移")]
    public Vector3 chestPosition;
    public Vector2 spawnOffset = new Vector2(1, -0.4f);

    [Header("普通宝箱配置")]
    public int normalMinCoin = 1;
    public int normalMaxCoin = 2;
    public float normalWeaponChance = 0.3f;
    public float normalPotionChance = 0.3f;

    [Header("金色宝箱配置")]
    public int goldenMinCoin = 5;
    public int goldenMaxCoin = 6;
    public float goldenWeaponChance = 1f;
    public float goldenPotionChance = 1f;

    [Header("诅咒宝箱配置")]
    public int cursedMinCoin =3;
    public int cursedMaxCoin = 4;
    public float cursedWeaponChance = 0.7f;
    public float cursedPotionChance = 0.7f;

    public Sprite buffSprite1;
    public Sprite buffSprite2;

    private bool isOpened;
    private SpriteRenderer sr;

    public BoxCollider2D cld;

    public bool IsOpened => isOpened;

    public void SetOpened()
    {
        isOpened = true;
        RefreshChestSprite();
        if (cld != null) cld.enabled = false;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        isOpened = false;
        RefreshChestSprite();
        chestPosition=transform.position;
        cld=GetComponent<BoxCollider2D>();
    }

    private void OnValidate()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        RefreshChestSprite();
    }

    void RefreshChestSprite()
    {
        if (sr == null) return;
        if (isOpened)
        {
            sr.sprite = chestType switch
            {
                ChestType.Normal => normalOpened,
                ChestType.Golden => goldenOpened,
                ChestType.Cursed => cursedOpened,
                _ => null
            };
        }
        else
        {
            sr.sprite = chestType switch
            {
                ChestType.Normal => normalClosed,
                ChestType.Golden => goldenClosed,
                ChestType.Cursed => cursedClosed,
                _ => null
            };
        }
    }

    public void Interact()
    {
        if (isOpened) return;

        isOpened = true;
        RefreshChestSprite();

        int minCoin;
        int maxCoin;
        float weaponChance;
        float potionChance;

        switch (chestType)
        {
            case ChestType.Normal:
                minCoin = normalMinCoin;
                maxCoin = normalMaxCoin;
                weaponChance = normalWeaponChance;
                potionChance= normalPotionChance;
                break;
            case ChestType.Golden:
                minCoin = goldenMinCoin;
                maxCoin = goldenMaxCoin;
                weaponChance = goldenWeaponChance;
                potionChance= goldenPotionChance;
                break;
            case ChestType.Cursed:
                minCoin = cursedMinCoin;
                maxCoin = cursedMaxCoin;
                weaponChance = cursedWeaponChance;
                potionChance= cursedPotionChance;
                ApplyRandomDebuffToPlayer();
                break;
            default:
                minCoin = 0;
                maxCoin = 0;
                weaponChance = 0;
                potionChance = 0;
                break;
        }

        int coinNum = Random.Range(minCoin, maxCoin + 1);
        for (int i = 0; i < coinNum; i++)
        {
            GameObject randomCoin = coinPrefabs[Random.Range(0, coinPrefabs.Count)];
            Vector2 spawnPos = (Vector2)chestPosition + spawnOffset + new Vector2(Random.Range(-2, 2), 0);
            Instantiate(randomCoin, spawnPos, Quaternion.identity);
        }

        if (Random.value < weaponChance && weaponPrefabs.Count > 0)
        {
            GameObject randomWeapon = weaponPrefabs[Random.Range(0, weaponPrefabs.Count)];
            Vector2 weaponSpawn = (Vector2)chestPosition + spawnOffset+ new Vector2(Random.Range(-2, 2), 0.4f); ;
            Instantiate(randomWeapon, weaponSpawn, Quaternion.identity);
        }

        if (Random.value < potionChance && potionPrefabs.Count > 0)
        {
            GameObject randomPotion = potionPrefabs[Random.Range(0, potionPrefabs.Count)];
            Vector2 potionSpawn = (Vector2)chestPosition + spawnOffset + new Vector2(Random.Range(-2, 2), 0); ;
            Instantiate(randomPotion, potionSpawn, Quaternion.identity);
        }

        cld.enabled = false;
    }

    void ApplyRandomDebuffToPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null) return;

        BuffManager buffMgr = playerObj.GetComponent<BuffManager>();
        if (buffMgr == null) return;

        BuffBase poisonous = new PoisonBuff(2.2f,15,0.7f);
        poisonous.buffSprite = buffSprite1;
        BuffBase cannotJump = new SlowDownBuff(5f,1f,0);
        cannotJump.buffSprite = buffSprite2;
        BuffBase hardtoMove = new SlowDownBuff(5f, 0.2f, 1f);
        hardtoMove.buffSprite = buffSprite2;

        List<BuffBase> candidateDebuffs = new List<BuffBase>
        {
            poisonous,
            cannotJump,
            hardtoMove
        };

        if (candidateDebuffs.Count == 0) return;

        BuffBase selectedDebuff = candidateDebuffs[Random.Range(0, candidateDebuffs.Count)];
        buffMgr.AddBuff(selectedDebuff);
    }

    public string GetTipText()
    {
        if (isOpened)
        {
            return "";
        }
        return chestType switch
        {
            ChestType.Normal => "Press [F] To Open Normal Chest",
            ChestType.Golden => "Press [F] To Open Golden Chest",
            ChestType.Cursed => "Press [F] To Open Cursed Chest",
            _ => ""
        };
    }
}

public enum ChestType
{
    Normal,
    Golden,
    Cursed
}