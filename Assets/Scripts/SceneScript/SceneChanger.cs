using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public void Start()
    {
        highScoreText.text = $"High Score : {PlayerPrefs.GetInt("HighScore")}";
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("OK");
    }
    public void GoToTitle()
    {
      
        SceneManager.LoadScene("Title");
    }
}
