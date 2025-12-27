using UnityEngine;

public class EnemyWalkerShooter : MonoBehaviour
{
    public Transform player;                  // プレイヤーへの参照
    public float moveSpeed = 2f;              // 移動速度
    public float stopDistance = 10f;          // この距離まで近づいたら停止して攻撃
    public float fireInterval = 1f;           // 発射間隔
    public GameObject bulletPrefab;           // 弾のプレハブ
    public Transform firePoint;               // 発射位置
    public float bulletSpeed = 20f;           // 弾速

    private float fireTimer = 0f;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // 一定距離まで移動
        if (distance > stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // 射撃処理
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireInterval)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }
            Destroy(bullet, 3f);
        }
    }
}
