using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    public Shop shop;
    public GameObject panelRoot;
    public GameObject slotPrefab;
    public Transform content;
    public Sprite potionSprite;

    [Header("与对话一致：屏蔽玩家交互与提示")]
    public PlayerInteract playerInteract;
    public GameObject interactTipText;

    [Header("购买提示")]
    public TMP_Text shopTipText;

    public bool IsShopOpen => isOpen;

    private List<ShopSlot> spawnedSlots = new List<ShopSlot>();
    private CanvasGroup panelCanvasGroup;
    private bool isOpen = false;

    private void Awake()
    {
        Instance = this;
        if (shop == null) shop = FindObjectOfType<Shop>();

        if (playerInteract == null) playerInteract = FindObjectOfType<PlayerInteract>();
        if (interactTipText == null)
        {
            GameObject go = GameObject.Find("InteractTipText");
            if (go != null) interactTipText = go;
        }
        if (potionSprite == null && slotPrefab != null)
        {
            ItemSlot src = slotPrefab.GetComponent<ItemSlot>();
            if (src != null) potionSprite = src.potionSprite;
        }
        if (panelRoot != null)
        {
            panelCanvasGroup = panelRoot.GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        SetPanelOpen(false);
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.F))
        {
            if (shop != null) shop.CloseShop();
        }
    }

    public void SetPanelOpen(bool open)
    {
        isOpen = open;

        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = open ? 1f : 0f;
            panelCanvasGroup.interactable = open;
            panelCanvasGroup.blocksRaycasts = open;
        }
        else if (panelRoot != null)
        {
            if (panelRoot != gameObject) panelRoot.SetActive(open);
        }

        if (open)
        {
            PauseManager.RequestPause();
            if (playerInteract != null) playerInteract.enabled = false;
            if (interactTipText != null) interactTipText.SetActive(false);
        }
        else
        {
            PauseManager.ReleasePause();
            if (playerInteract != null) playerInteract.enabled = true;
            if (interactTipText != null) interactTipText.SetActive(true);
        }
    }

    public void RefreshGoods(List<InventoryItem> goods)
    {
        while (spawnedSlots.Count < goods.Count)
            spawnedSlots.Add(SpawnSlot());

        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            if (i < goods.Count)
            {
                InventoryItem item = goods[i];
                Sprite icon = potionSprite;
                string displayName = "HealPotion";
                if (item.kind == InventoryItemKind.Weapon && item.swordData != null)
                {
                    icon = item.swordData.swordSprite;
                    displayName = item.swordData.swordName;
                }

                spawnedSlots[i].gameObject.SetActive(true);
                spawnedSlots[i].SetData(icon, displayName, i);
            }
            else
            {
                spawnedSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnSlotClicked(int index)
    {
        if (shop != null) shop.TryBuy(index);
    }

    public void ShowTip(string message)
    {
        if (shopTipText != null) shopTipText.text = message;
    }

    private ShopSlot SpawnSlot()
    {
        GameObject go = Instantiate(slotPrefab, content);
        ShopSlot slot = go.GetComponent<ShopSlot>();
        if (slot == null) slot = go.AddComponent<ShopSlot>();
        slot.shopUI = this;

        Button btn = go.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            ShopSlot captured = slot;
            btn.onClick.AddListener(() => captured.OnShopSlotClick());
        }
        return slot;
    }
}