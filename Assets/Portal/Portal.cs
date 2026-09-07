using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    public string targetScene = "Level_2";
    public string GetTipText()
    {
        return "Press [F] To Enter Next Level";
    }

    public void Interact()
    {
        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.AutoSave();
        }
        else
        {
            Debug.LogWarning("[Portal] 找不到 SaveLoadManager，切换后将丢失数据");
        }

        SceneManager.LoadScene(targetScene);
    }
}