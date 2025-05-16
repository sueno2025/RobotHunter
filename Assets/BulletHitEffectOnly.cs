using UnityEngine;

public class BulletHitEffectOnly : MonoBehaviour
{
    public GameObject hitEffectPrefab; // ヒット時に再生するエフェクト

    void OnTriggerEnter(Collider other)
    {
        // 💥 ヒットエフェクトだけ再生
        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f); // 2秒後に自動削除（エフェクトのDurationに合わせて調整）
        }

        Destroy(gameObject); // 弾も消す
    }
}
