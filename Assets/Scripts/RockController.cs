using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RockController : MonoBehaviour
{
    public float speed = 2f;
    public GameObject hitEffect;
    public GameObject rockEffect;
    public TextMeshProUGUI hpText;
    int hp;

    void Start()
    {
        hp = Random.Range(20, 41);
       
        UpdateHPText();
    
    }
    void LateUpdate()
    {
        if (hpText != null && hpText.transform.parent != null)
        {
            hpText.transform.parent.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }

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

        //回転処理
        transform.Rotate(Vector3.forward * 360 * Time.deltaTime);

        //破壊処理
        if (hp <= 0)
        {
            Instantiate(rockEffect,transform.position,Quaternion.identity);
            SoundManager.Instance.playRockExplosion();
            Destroy(gameObject);
        }
        if (transform.position.z > 90f) // 画面より十分奥
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Bullet"))
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);
            SoundManager.Instance.PlayExplosionSE();
            hp -= 1;
            Debug.Log($"HP:{hp}");
            UpdateHPText();
        }
    }
    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = $"{hp}";
            Debug.Log($"HP表示更新: {hpText.text}");
        }
    }
    //消えない
    // void OnBecameInvisible()
    // {
    //     Destroy(gameObject);

    // }
}