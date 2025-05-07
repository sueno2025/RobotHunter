using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public float speed = 2f;
    public GameObject hitEffect;

    void Update()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found!");
            return;
        }

        // カメラの向きを取得
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();

        // カメラの向きの逆（＝画面手前方向）に進む
        Vector3 moveDirection = -forward;

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);
        }
    }
}