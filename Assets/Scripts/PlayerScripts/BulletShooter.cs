using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 50f;
    public float fireRate = 0.05f; // 発射間隔（秒）

    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    Rigidbody rb = bullet.GetComponent<Rigidbody>();

    // カメラの向いている方向に飛ばす
    //Vector3 shootDirection = Camera.main.transform.forward;

    //rb.velocity = shootDirection * bulletSpeed;

    //05/07追記
    // カメラの向きを取得
    Vector3 camForward = Camera.main.transform.forward;

    // Y成分を除去して「水平成分だけ」にする
    camForward.y = 0;
    camForward = camForward.normalized;

    // 水平に飛ばす
    rb.velocity = camForward * bulletSpeed;

    Destroy(bullet, 3f);
    Destroy(bullet, 3f);
    }

    

}

