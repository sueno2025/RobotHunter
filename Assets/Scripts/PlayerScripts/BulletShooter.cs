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
    Vector3 shootDirection = Camera.main.transform.forward;

    rb.velocity = shootDirection * bulletSpeed;
    Destroy(bullet, 3f);
    }

}

