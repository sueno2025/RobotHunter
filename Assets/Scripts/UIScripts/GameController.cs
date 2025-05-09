using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameController nejiko;
    public TextMeshProUGUI scoreText;

    // Update is called once per frame
    public void Update()
    {
        int score=CalcScore();
        scoreText.text=$"Score : {score}m";
            if(PlayerPrefs.GetInt("HighScore")<score)
            {
                PlayerPrefs.SetInt("HighScore",score);
            }
            Invoke("ReturnToTitle",2.0f);
    }

    int CalcScore()
    {
        return (int)nejiko.transform.position.z;
    }

    void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Debug.Log("ポーズしました");
        }
        else
        {
            Debug.Log("ポーズから復帰しました");
        }
    }
}