using UnityEngine;
using UnityEngine.SceneManagement;

//死亡界面：玩家死亡时弹出（独立 Canvas，仿 End 场景）
//按钮 Back to Menu：恢复 timeScale 后回主菜单（存档保留，可读档回到死亡前位置）
public class DeathUI : MonoBehaviour
{
    public static DeathUI Instance;

    public GameObject deathPanel;
    public PlayerInteract playerInteract;
    public GameObject interactTipText;

    public bool IsDeathShowing => isShowing;

    private bool isShowing = false;
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
        if (deathPanel != null)
        {
            panelCanvasGroup = deathPanel.GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        SetShowing(false);
    }

    //玩家死亡时由 PlayerHealth.OnDeath 调用
    public void ShowDeath()
    {
        SetShowing(true);
    }

    private void SetShowing(bool show)
    {
        isShowing = show;

        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = show ? 1f : 0f;
            panelCanvasGroup.interactable = show;
            panelCanvasGroup.blocksRaycasts = show;
        }
        else if (deathPanel != null)
        {
            if (deathPanel != gameObject) deathPanel.SetActive(show);
        }

        //暂停 + 屏蔽交互 + 隐藏提示
        if (show)
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