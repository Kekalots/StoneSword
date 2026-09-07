using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;
    public static GameData pendingData;

    [Header("武器注册表（读档按名字反查）")]
    public List<SwordSO> allWeapons = new List<SwordSO>();

    [Header("玩家引用（不拖会自动找）")]
    public PlayerMove playerMove;
    public PlayerHealth playerHealth;
    public PlayerNow playerNow;
    public CoinManager coinManager;
    public InventoryItemManager inventory;
    public WeaponNow weaponNow;
    public Shop shop;

    private const string SAVE_KEY = "StoneSword_Save";

    private void Awake()
    {
        Instance = this;

        if (playerMove == null) playerMove = GetComponent<PlayerMove>();
        if (playerHealth == null) playerHealth = GetComponent<PlayerHealth>();
        if (playerNow == null) playerNow = GetComponent<PlayerNow>();
        if (coinManager == null) coinManager = FindObjectOfType<CoinManager>();
        if (inventory == null) inventory = GetComponent<InventoryItemManager>();
        if (weaponNow == null) weaponNow = GetComponentInChildren<WeaponNow>();
        if (shop == null) shop = FindObjectOfType<Shop>();
    }

    private void Start()
    {
        if (pendingData != null)
        {
            bool fromAutoSave = !pendingData.restorePosition;
            ApplyData(pendingData);
            pendingData = null;

            if (fromAutoSave)
            {
                GameData current = CollectData(true);
                PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(current));
                Debug.Log("[SaveLoad] 存档已更新到当前场景：" + current.sceneName);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !PauseManager.IsPaused)
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        GameData data = CollectData(true);
        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(data));
        Debug.Log($"[SaveLoad] 已存档：{data.sceneName} HP:{data.hp} 金币:{data.coins} 背包:{data.items.Count}件");
    }

    public void AutoSave()
    {
        GameData data = CollectData(false);
        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(data));
        pendingData = data;
        Debug.Log($"[SaveLoad] 自动存档：{data.sceneName} → 切场景");
    }

    public static GameData ReadData()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY)) return null;
        return JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(SAVE_KEY));
    }

    public static bool HasSave() => PlayerPrefs.HasKey(SAVE_KEY);

    public static void DeleteSave() => PlayerPrefs.DeleteKey(SAVE_KEY);

    private GameData CollectData(bool restorePos)
    {
        GameData data = new GameData();
        data.sceneName = SceneManager.GetActiveScene().name;
        data.restorePosition = restorePos;
        if (playerMove != null)
        {
            data.posX = playerMove.transform.position.x;
            data.posY = playerMove.transform.position.y;
        }
        if (playerHealth != null) data.hp = playerHealth.currentHP;
        if (playerNow != null) data.defense = playerNow.defense;
        if (coinManager != null) data.coins = coinManager.money;
        if (weaponNow != null && weaponNow.currentSword != null) data.currentWeapon = weaponNow.currentSword.swordName;

        if (inventory != null)
        {
            foreach (InventoryItem item in inventory.itemList)
            {
                SaveItem si = new SaveItem();
                if (item.kind == InventoryItemKind.Weapon && item.swordData != null)
                {
                    si.itemName = item.swordData.swordName;
                    si.stackCount = 1;
                }
                else
                {
                    si.itemName = "HealPotion";
                    si.healValue = item.healValue;
                    si.stackCount = item.stackCount;
                }
                data.items.Add(si);
            }
        }

        if (shop != null)
        {
            data.shopVisitTimes = shop.GetVisitTimes();
            data.shopRemaining = shop.GetRemainingNames();
        }

        foreach (GroundCoin coin in FindObjectsOfType<GroundCoin>())
        {
            data.existingCoins.Add($"{data.sceneName}|{coin.transform.position.x:F2},{coin.transform.position.y:F2}");
        }
        foreach (OpenChest chest in FindObjectsOfType<OpenChest>())
        {
            if (chest.IsOpened) data.openedChests.Add($"{data.sceneName}|{chest.transform.position.x:F2},{chest.transform.position.y:F2}");
        }
        return data;
    }

    public void ApplyData(GameData data)
    {
        if (data == null) return;

        if (data.restorePosition && playerMove != null)
        {
            playerMove.transform.position = new Vector3(data.posX, data.posY, playerMove.transform.position.z);
        }

        if (playerHealth != null) playerHealth.currentHP = data.hp;

        if (playerNow != null) playerNow.defense = data.defense;

        if (coinManager != null) coinManager.SetMoney(data.coins);

        if (weaponNow != null)
        {
            SwordSO sword = FindSwordByName(data.currentWeapon);
            if (sword != null) weaponNow.EquipWeapon(sword);
        }

        if (inventory != null)
        {
            inventory.itemList.Clear();
            foreach (SaveItem si in data.items)
            {
                if (si.itemName == "HealPotion")
                {
                    inventory.AddHealPotion(si.healValue, si.stackCount);
                }
                else
                {
                    SwordSO sword = FindSwordByName(si.itemName);
                    if (sword != null) inventory.AddWeapon(sword);
                }
            }
        }

        if (shop != null) shop.RestoreState(data.shopVisitTimes, data.shopRemaining);

        string scene = SceneManager.GetActiveScene().name;
        if (data.sceneName == scene)
        {
            foreach (GroundCoin coin in FindObjectsOfType<GroundCoin>())
            {
                string id = $"{scene}|{coin.transform.position.x:F2},{coin.transform.position.y:F2}";
                if (!data.existingCoins.Contains(id))
                {
                    Destroy(coin.gameObject);
                }
            }

            foreach (OpenChest chest in FindObjectsOfType<OpenChest>())
            {
                string id = $"{scene}|{chest.transform.position.x:F2},{chest.transform.position.y:F2}";
                if (data.openedChests.Contains(id))
                {
                    chest.SetOpened();
                }
            }
        }

        Debug.Log($"[SaveLoad] 存档已应用：{data.sceneName} HP:{data.hp} 金币:{data.coins} 背包:{data.items.Count}件");
    }

    private SwordSO FindSwordByName(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;
        foreach (SwordSO sword in allWeapons)
        {
            if (sword != null && sword.swordName == name) return sword;
        }
        return null;
    }
}