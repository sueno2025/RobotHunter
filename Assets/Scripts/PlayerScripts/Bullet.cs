using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 05/13追記 - 敵との衝突でスコア加算とオブジェクト破壊
        if (other.CompareTag("Enemy"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(100); // スコアを100加算
            }
            Destroy(other.gameObject); // 敵（ZakoSlave）を破壊
            Destroy(gameObject); // 弾を破壊
        }
    }
}