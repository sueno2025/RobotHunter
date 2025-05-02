using UnityEngine;

public class DualShooter : MonoBehaviour
{
    [Header("弾の設定")]
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public float bulletSpeed = 20f;     // 弾の速さ（publicで調整可能）
    
    [Header("発射タイミング")]
    public float fireInterval = 0.3f;   // 連射間隔（publicで調整可能）

    private float timer = 0f;
    private int currentIndex = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            Fire();
            timer = 0f;
        }
    }

    void Fire()
    {
        if (bulletPrefab == null || firePoints.Length == 0) return;

        Transform firePoint = firePoints[currentIndex];
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        Destroy(bullet, 3f);

        currentIndex = (currentIndex + 1) % firePoints.Length;
    }
}
