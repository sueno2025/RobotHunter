using UnityEngine;
public class Enemy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // スコア加算
            ScoreManager.Instance.AddScore(100);

            // 敵と弾を消す
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
