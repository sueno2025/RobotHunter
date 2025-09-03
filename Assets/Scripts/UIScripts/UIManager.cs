using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject pauseButton;

    [Header("GameOver UI")]
    public CanvasGroup gameOverCanvasGroup;
    public CanvasGroup returnToMenuCanvasGroup;

    void Start()
    {
        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 0f;
            gameOverCanvasGroup.gameObject.SetActive(false);
            gameOverCanvasGroup.interactable = false;
            gameOverCanvasGroup.blocksRaycasts = false;
        }

        if (returnToMenuCanvasGroup != null)
        {
            returnToMenuCanvasGroup.alpha = 0f;
            returnToMenuCanvasGroup.gameObject.SetActive(false);
            returnToMenuCanvasGroup.interactable = false;
            returnToMenuCanvasGroup.blocksRaycasts = false;
        }
    }

    public void ShowGameOverUI()
    {
        if (pauseButton != null)
            pauseButton.SetActive(false);

        if (gameOverCanvasGroup != null)
        {
            StartCoroutine(FadeInCanvasGroup(gameOverCanvasGroup, 1f, 0.8f));
        }

        if (returnToMenuCanvasGroup != null)
        {
            StartCoroutine(FadeInCanvasGroup(returnToMenuCanvasGroup, 1f, 1.8f));
        }
    }

    IEnumerator FadeInCanvasGroup(CanvasGroup cg, float duration, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        cg.gameObject.SetActive(true);
        cg.alpha = 0f;
        float time = 0f;

        while (time < duration)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Title");
    }
}