using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public TMP_Text tipText;
    public IInteractable currentTarget;

    private void Awake()
    {
        currentTarget = null;
        if (tipText == null)
        {
            GameObject go = GameObject.Find("InteractTipText");
            if (go != null) tipText = go.GetComponent<TMP_Text>();
        }
        if (tipText != null) tipText.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interact = other.GetComponent<IInteractable>();
        if (interact != null)
        {
            currentTarget = interact;
            if (tipText != null)
            {
                tipText.text = currentTarget.GetTipText();
                tipText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == null) return;

        IInteractable interact = other.GetComponent<IInteractable>();
        if (interact != null)
        {
            currentTarget = interact;
            if (tipText != null)
            {
                tipText.text = currentTarget.GetTipText();
                tipText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null) return;

        IInteractable interact = other.GetComponent<IInteractable>();
        if (interact == currentTarget)
        {
            currentTarget = null;
            if (tipText != null) tipText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(currentTarget!=null&&Input.GetKeyDown(KeyCode.F))
        {
            currentTarget.Interact();
            currentTarget=null;
            if (tipText != null) tipText.gameObject.SetActive(false);
        }
    }
}