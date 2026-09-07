using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TMP_Text contentText, speakerNameText;
    [SerializeField] Image speakerAvatarImage;

    public PlayerInteract playerInteract;
    public GameObject interactTipText;

    private int currentSentence;
    public ConversationSO currentConv;

    public bool IsInDialogue => currentConv != null;

    public UnityEvent onDialogueEnd;

    void Awake()
    {
        Instance = this;

        if (playerInteract == null) playerInteract = FindObjectOfType<PlayerInteract>();
        if (interactTipText == null)
        {
            GameObject go = GameObject.Find("InteractTipText");
            if (go != null) interactTipText = go;
        }
    }

    void Start()
    {
        canvasGroup.alpha = 0;
    }

    public void OnClick()
    {
        if (currentConv == null) return;

        currentSentence++;
        if (currentSentence >= currentConv.sentences.Count)
        {
            EndDialogue();
            currentSentence = 0;
            return;
        }

        UpdateUIInfo();
    }

    public void StartDialogue(ConversationSO conversation)
    {
        if (conversation == null || conversation.sentences.Count == 0) return;

        PauseManager.RequestPause();
        if (playerInteract != null) playerInteract.enabled = false;
        if (interactTipText != null) interactTipText.SetActive(false);

        currentConv = conversation;
        currentSentence = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        UpdateUIInfo();
    }

    public void EndDialogue()
    {
        PauseManager.ReleasePause();
        if (playerInteract != null) playerInteract.enabled = true;
        if (interactTipText != null) interactTipText.SetActive(true);

        currentConv = null;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        StartCoroutine(FadeDialogue(1, 0));

        onDialogueEnd?.Invoke();
    }

    private void UpdateUIInfo()
    {
        if (currentConv == null) return;
        contentText.text = currentConv.sentences[currentSentence].content;
        speakerNameText.text = currentConv.sentences[currentSentence].speakerName;
        speakerAvatarImage.sprite = currentConv.sentences[currentSentence].speakerAvatar;

        if (canvasGroup.alpha < 1f)
        {
            StartCoroutine(FadeDialogue(0, 1));
        }
    }

    IEnumerator FadeDialogue(float a, float b)
    {
        float t = 0, duration = 0.5f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(a, b, 1.0f * t / duration);
            yield return null;
        }
        canvasGroup.alpha = b;
    }
}