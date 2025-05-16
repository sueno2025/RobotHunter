using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // 05/15追記 - 弾のダメージ値

    void OnTriggerEnter(Collider other)
    {
        // 05/13追記 - ZakoSlaveとの衝突でスコア100加算
        if (other.CompareTag("Enemy") && other.GetComponent<ZakoSlave>() != null)
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(100);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
