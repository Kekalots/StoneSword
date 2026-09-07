using UnityEngine;

public static class PauseManager
{
    private static int pauseCount;

    public static bool IsPaused => pauseCount > 0;

    public static void RequestPause()
    {
        pauseCount++;
        Time.timeScale = 0f;
    }

    public static void ReleasePause()
    {
        pauseCount = Mathf.Max(0, pauseCount - 1);
        if (pauseCount == 0)
        {
            Time.timeScale = 1f;
        }
    }

    public static void ResetPause()
    {
        pauseCount = 0;
        Time.timeScale = 1f;
    }
}