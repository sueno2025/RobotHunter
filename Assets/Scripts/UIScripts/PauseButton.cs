using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    void Update()
    {
        // タップやクリックで再開（ポーズ中のみ）
        if (isPaused && Input.GetMouseButtonDown(0))
        {
            ResumeGame();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pausePanel != null) pausePanel.SetActive(true);
        //05/18追記
        SoundManager.Instance.StopBGM();  // ★ BGMを止める
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null) pausePanel.SetActive(false);
        //05/18追記
        SoundManager.Instance.playBGM(SoundManager.Instance.bgm); // ★ BGM再開（任意）
    }

}
