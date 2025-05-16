using UnityEngine;
using TMPro; // 05/13追記 - TextMeshPro用

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int score = 0;
    public TextMeshProUGUI scoreText; // 05/13追記 - スコア表示用UI

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 05/13追記 - 初期スコアをUIに反映
        scoreText.text = "SCORE: " + score;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
        scoreText.text = "SCORE: " + score; // 05/13追記 - スコアをUIに反映
    }
}
