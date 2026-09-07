using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : MonoBehaviour, IInteractable
{
    public ConversationSO conversation;

    public bool openShopAfterDialogue = true;
    private Shop shop;

    private void Start()
    {
        shop=GetComponent<Shop>();
    }
    public string GetTipText()
    {
        return "Press [F] To Talk";
    }

    public void Interact()
    {
        if (conversation == null) return;

        DialogueManager dm = DialogueManager.Instance;
        if (dm == null) return;

        if (openShopAfterDialogue && shop != null)
        {
            dm.onDialogueEnd.RemoveAllListeners();
            dm.onDialogueEnd.AddListener(shop.OpenShop);
        }
        else
        {
            dm.onDialogueEnd.RemoveAllListeners();
        }

        dm.StartDialogue(conversation);
    }
}