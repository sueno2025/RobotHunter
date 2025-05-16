using UnityEngine;

public class DualShooter : MonoBehaviour
{
    [Header("弾の設定")]
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public float bulletSpeed = 20f;

    [Header("発射タイミング")]
    public float fireInterval = 0.3f;

    [Header("ターゲットと射程距離")]
    public Transform target;
    public float shootDistance = 10f;
    public float stopDistance = 2f;

    [Header("移動速度")]
    public float moveSpeed = 2f;

    [Header("横移動（プレイヤー位置に合わせる）")]
    public float lateralMoveSpeed = 2f;

    private float timer = 0f;
    private int currentIndex = 0;

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, lateralMoveSpeed * Time.deltaTime);
        }

        if (distance <= shootDistance)
        {
            timer += Time.deltaTime;
            if (timer >= fireInterval)
            {
                Fire();
                timer = 0f;
            }
        }
    }

    void Fire()
    {
        if (bulletPrefab == null || firePoints.Length == 0 || target == null) return;

        Transform firePoint = firePoints[currentIndex];
        Vector3 targetCenter = target.position + Vector3.up * 1.0f;
        Vector3 direction = (targetCenter - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        Vector3 spawnPos = firePoint.position + firePoint.up * 0.1f;

        GameObject bullet = Instantiate(bulletPrefab, spawnPos, rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = direction * bulletSpeed;
        }

        Destroy(bullet, 10f);

        // 🔁 次の砲台に切り替え（交互に撃つ）
        currentIndex = (currentIndex + 1) % firePoints.Length;
    }
}
