using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class UIManager2 : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public GameObject titleButton;
    public CanvasGroup gameOverCanvasGroup;
    public CanvasGroup gameClearCanvasGroup;
    public CanvasGroup titleButtonCanvasGroup;



    void Start()
    {


        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        titleButton.SetActive(false);

        gameOverCanvasGroup = gameOverPanel.GetComponentInParent<CanvasGroup>();
        gameClearCanvasGroup = gameClearPanel.GetComponentInParent<CanvasGroup>();
        titleButtonCanvasGroup = titleButton.GetComponentInParent<CanvasGroup>();
        gameOverCanvasGroup.alpha = 0f;
        gameClearCanvasGroup.alpha = 0f;
        titleButtonCanvasGroup.alpha = 0f;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        gameClearPanel.SetActive(false);
        titleButton.SetActive(true);
        gameOverCanvasGroup.DOFade(1f, 3f);
        titleButtonCanvasGroup.DOFade(1f, 3f);


    }

    public void ShowGameClear()
    {
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(true);
        titleButton.SetActive(true);
        gameClearCanvasGroup.DOFade(1f, 3f);
        titleButtonCanvasGroup.DOFade(1f, 3f);
    }
    public void ReturnToMainMenu()
    {
        //05/18追記
        SoundManager.Instance.StopBGM();  // ← ここで止める！
        SceneManager.LoadScene("Title");
    }
}
