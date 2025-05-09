using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    //インスペクターから登録
    public GameObject hitEffectPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            UnityEngine.Vector3 spawnPos = transform.position + UnityEngine.Vector3.up * 0.5f;
            GameObject effect = Instantiate(hitEffectPrefab,spawnPos,UnityEngine.Quaternion.identity);
            Destroy(effect,1f);
            
            SoundManager.Instance.PlayExplosionSE();
            
            Destroy(other.gameObject);  // 敵を消す
            Destroy(gameObject);        // 弾を消す
        }
    }
}
