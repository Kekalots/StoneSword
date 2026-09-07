using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public GameObject pausePanel;
    public PlayerInteract playerInteract;
    public GameObject interactTipText;

    public bool IsPaused => isPaused;

    private bool isPaused = false;
    private CanvasGroup panelCanvasGroup;

    private void Awake()
    {
        Instance = this;

        if (playerInteract == null) playerInteract = FindObjectOfType<PlayerInteract>();
        if (interactTipText == null)
        {
            GameObject go = GameObject.Find("InteractTipText");
            if (go != null) interactTipText = go;
        }
        if (pausePanel != null)
        {
            panelCanvasGroup = pausePanel.GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        SetPause(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (isPaused)
        {
            SetPause(false);
        }
        else if (!PauseManager.IsPaused)
        {
            SetPause(true);
        }
    }

    public void TogglePause()
    {
        SetPause(!isPaused);
    }

    public void SetPause(bool pause)
    {
        isPaused = pause;

        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = pause ? 0.6f : 0f;
            panelCanvasGroup.interactable = pause;
            panelCanvasGroup.blocksRaycasts = pause;
        }
        else if (pausePanel != null)
        {
            if (pausePanel != gameObject) pausePanel.SetActive(pause);
        }

        if (pause)
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

    public void OnBackToMenu()
    {
        PauseManager.ResetPause();
        SceneManager.LoadScene("MainMenu");
    }
}