using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
