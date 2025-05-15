using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    public GameObject hitEffectPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log($"Trigger entered: {other.name}");
            Vector3 spawnPos = transform.position + Vector3.up * 0.5f;
            GameObject effect = Instantiate(hitEffectPrefab,spawnPos,Quaternion.identity);
            Destroy(effect,1f);
            
        }
    }
}
