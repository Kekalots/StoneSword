using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWeapon : MonoBehaviour,IInteractable
{
    public SwordSO swordData;
    public SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        RefreshDisplay();
    }
    private void OnValidate()
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        RefreshDisplay();
    }
    public void RefreshDisplay()
    {
        sr.sprite = swordData.swordSprite;
    }

    public void Interact()
    {
        InventoryItemManager.Instance.AddWeapon(swordData);
        Destroy(gameObject);
    }
    public string GetTipText()
    {
        return $"Press [F] to Pick Up {swordData.swordName}";
    }
}
