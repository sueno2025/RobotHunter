using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("HP設定")]
    public int maxHealth = 100;
    public int defense = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(damage - defense, 0);
        currentHealth -= actualDamage;

        Debug.Log($"💥 Mechが{actualDamage}ダメージを受けた（防御力:{defense}） 残りHP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 Mechが破壊された！");
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Debug.Log("🎯 プレイヤーの弾が命中！");
            TakeDamage(20);
            Destroy(other.gameObject);
        }
    }
}
