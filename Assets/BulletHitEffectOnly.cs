using UnityEngine;

public class BulletHitEffectOnly : MonoBehaviour
{
    public GameObject hitEffectPrefab;

    void OnTriggerEnter(Collider other)
    {
        // プレイヤーに命中したときだけ反応（T_Poseに "Player" タグが付いている前提）
        if (other.CompareTag("Player"))
        {
            // ヒットエフェクト再生
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }

            Destroy(gameObject); // 弾を消す
        }
    }
}