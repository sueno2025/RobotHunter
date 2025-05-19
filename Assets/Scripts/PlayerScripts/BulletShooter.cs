using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 50f;
    public float fireRate = 0.05f; // 発射間隔（秒）
    private float minFireRate = 0.01f; // 05/19追記: fireRateの最小値

    private float nextFireTime = 0f;
    CharacterMovement player;

    void Start()
    {
        player = GetComponent<CharacterMovement>(); 
    }

    void LateUpdate()
    {
        if (player != null && player.IsDead)
        {
            return;
        }

        if (Time.time >= nextFireTime)
        {   
            // 05/08追記（効果音）微調整必要
            SoundManager.Instance.PlayShootSE();
            
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // カメラの向きを取得
        Vector3 camForward = Camera.main.transform.forward;

        // Y成分を除去して「水平成分だけ」にする
        camForward.y = 0;
        camForward = camForward.normalized;

        // 水平に飛ばす
        rb.velocity = camForward * bulletSpeed;

        Destroy(bullet, 3f);
    }

    // 05/19追記: bulletSpeedを増加させる公開メソッド
    public void IncreaseBulletSpeed(float amount)
    {
        bulletSpeed += amount;
        Debug.Log($"Bullet speed increased to: {bulletSpeed}");
    }

    // 05/19追記: fireRateを減少させる公開メソッド（発射頻度アップ）
    public void IncreaseFireRate(float amount)
    {
        fireRate = Mathf.Max(fireRate - amount, minFireRate);
        Debug.Log($"Fire rate decreased to: {fireRate}");
    }
}