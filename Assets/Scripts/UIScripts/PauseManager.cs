using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // ポーズ時に表示するUI（任意）

    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(false);
        }
    }
}

