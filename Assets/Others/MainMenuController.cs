using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("规则界面（主菜单内的面板，默认隐藏）")]
    public GameObject rulesPanel;

    private void Start()
    {
        if (rulesPanel != null) rulesPanel.SetActive(false);
    }

    public void OnNewGame()
    {
        SaveLoadManager.DeleteSave();
        SaveLoadManager.pendingData = null;
        SceneManager.LoadScene("Level_1");
    }

    public void OnLoadGame()
    {
        GameData data = SaveLoadManager.ReadData();
        if (data == null)
        {
            Debug.Log("[MainMenu] 没有存档");
            return;
        }

        SaveLoadManager.pendingData = data;
        SceneManager.LoadScene(data.sceneName);
    }

    public void OnOpenRules()
    {
        if (rulesPanel != null) rulesPanel.SetActive(true);
    }

    public void OnCloseRules()
    {
        if (rulesPanel != null) rulesPanel.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}