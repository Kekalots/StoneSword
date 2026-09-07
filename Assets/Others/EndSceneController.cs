using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    public void OnQuitToMenu()
    {
        SaveLoadManager.DeleteSave();
        SaveLoadManager.pendingData = null;
        SceneManager.LoadScene("MainMenu");
    }
}