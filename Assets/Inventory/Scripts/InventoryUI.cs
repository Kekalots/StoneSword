using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject panelRoot;
    public GameObject slotPrefab;
    public Transform content;
    public GameObject interactTipText;
    public PlayerInteract playerInteract;

    private List<ItemSlot> spawnedSlots = new List<ItemSlot>();
    private CanvasGroup panelCanvasGroup;
    private bool isOpen = false;

    private void Awake()
    {
        if (panelRoot != null)
        {
            panelCanvasGroup = panelRoot.GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        SetPanelOpen(false);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TogglePanel();
        }

        if (isOpen)
        {
            if (playerInteract != null)
            {
                playerInteract.enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                InventoryItemManager.Instance.OnPressQ();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                InventoryItemManager.Instance.DestroySelectedWeapon();
            }
        }
        else
        {
            if (playerInteract != null)
            {
                playerInteract.enabled = true;
            }
        }
    }

    public void TogglePanel()
    {
        SetPanelOpen(!isOpen);
    }

    private void SetPanelOpen(bool open)
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
            if (panelRoot != gameObject)
            {
                panelRoot.SetActive(open);
            }
        }

        if (!open)
        {
            Time.timeScale = 1f;
            if (InventoryItemManager.Instance != null)
            {
                InventoryItemManager.Instance.ClearSelected();
            }
            if (interactTipText != null)
            {
                interactTipText.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 0;
            if (interactTipText != null)
            {
                interactTipText.SetActive(false);
            }
            if (InventoryItemManager.Instance != null)
            {
                RefreshAllUi(InventoryItemManager.Instance.itemList);
            }
        }
    }

    public void RefreshAllUi(List<InventoryItem> items)
    {
        if (spawnedSlots.Count == 0)
        {
            SpawnAllSlots();
        }

        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            if (i < items.Count)
            {
                spawnedSlots[i].SetData(items[i], i);
            }
            else
            {
                spawnedSlots[i].SetEmpty();
            }
        }
    }

    private void SpawnAllSlots()
    {
        var manager = InventoryItemManager.Instance;
        int count = manager != null ? manager.maxSlotCount : 18;

        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(slotPrefab, content);
            ItemSlot slot = go.GetComponent<ItemSlot>();
            if (slot != null)
            {
                slot.SetEmpty();
                spawnedSlots.Add(slot);
            }
        }
    }
}
