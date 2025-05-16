using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRobotController : MonoBehaviour
{
    Animator animator;
    public int hp = 200;
    public Transform player;
    public GameObject EnemyBulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;
    public float fireRate = 0.5f;
    public float dashForce = 1400f;
    public Rigidbody rb;
    Vector3 originalPosition;
    bool isReturning = false;
    bool isDashing;
    int hitCount = 0;
    bool isDamaged = false;
    bool isInvincible = true;
    bool isDead;
    public GameObject hitEffect;
    public GameObject RockEffect;


    float nextFireTime = 0f;

    enum BossState { Idle, Shooting, Waiting, Charging, Returning, Damaged }
    BossState currentState = BossState.Idle;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb.isKinematic = true;
        FirstMove();
        StartCoroutine(BossRoutine());
    }
    void Update()
    {
        if (isDead || player == null || isDamaged) return;


        Vector3 direction = player.position - transform.position;

        if (direction.sqrMagnitude < 0.3f) return;

        // ボスの見た目が22度ズレてるなら、Y+22度分回転させる
        if (!isDashing && !isReturning)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation * Quaternion.Euler(0, 22f, 0);
        }
        if (!isReturning && transform.position.z >= 75f)
        {
            StartCoroutine(ReturnToOrigine(1.5f));
        }
        //テスト
        // if (Input.GetKeyDown(KeyCode.X)) ShootAttack();
        // if (Input.GetKeyDown(KeyCode.Z)) MoveAttack();


    }


    //初期位置までの移動
    void FirstMove()
    {
        SoundManager.Instance.PlayRoboWalkLoop();
        Vector3 targetPosition = new Vector3(0, 2, 40);
        StartCoroutine(FirstMoveRoutine(transform.position, targetPosition, 3f));
    }
    //突進攻撃
    void MoveAttack()
    {
        SoundManager.Instance.PlayRoboWalkLoop();
        if (isDead || player == null || isReturning || isDamaged) return;
        rb.isKinematic = false; // 物理ON
        isDashing = true;
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // 水平方向だけ突進したい場合

        rb.AddForce(direction * dashForce);

    }
    //銃攻撃
    void ShootAttack()
    {
        if (player == null || Time.time < nextFireTime || isDamaged) return;
        FireBullet();
        nextFireTime = Time.time + fireRate;
    }
    // 強制的に撃つ（cooldown無視）
    void ForceShootAttack()
    {
        if (isDead || player == null || isDamaged || isReturning) return;
        FireBullet();
    }
    void FireBullet()
    {
        SoundManager.Instance.PlayRoboShoot();
        animator.SetTrigger("Shoot");
        GameObject bullet = Instantiate(EnemyBulletPrefab, firePoint.position, Quaternion.identity);
        // bullet.transform.localScale = new Vector3(2f, 2f, 2f);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.useGravity = false;
        Vector3 targetPos = player.position;
        targetPos.y = firePoint.position.y; // 高さを揃える
        Vector3 direction = (player.position - firePoint.position).normalized;
        rb.velocity = direction * bulletSpeed;
        Destroy(bullet, 3f);
    }

    //死亡時の挙動
    void Die()
    {
        SoundManager.Instance.PlayRoboClash();
        Instantiate(RockEffect, transform.position, Quaternion.identity);
        if (isDead) return;
        isDead = true;
        StopAllCoroutines();
        animator.SetBool("IsDead", true);
        animator.Update(0f); // ← 即反映させる

        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        rb.useGravity = true;
        //GameManagerに通知
        GameManager.Instance.OnBossDefeated();

        // 05/15追記 - ボス撃破でスコア200加算
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(200);
        }

        Destroy(gameObject, 3f);
    }

    IEnumerator FirstMoveRoutine(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos; // 最後ピタッと合わせる
        originalPosition = transform.position;
        isInvincible = false;
        SoundManager.Instance.StopRoboWalk();
    }
    IEnumerator ReturnToOrigine(float duration)
    {

        isReturning = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        Vector3 start = transform.position;
        Vector3 end = originalPosition;
        // float duration = 1.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(start, end, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
        isReturning = false;
        isDashing = false;
        SoundManager.Instance.StopRoboWalk();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {

            if (isInvincible) return;
            if (hp <= 0) return;
            hp--;
            if (hp == 0)
            {
                Die();
            }
            SoundManager.Instance.PlayExplosionSE();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            hitCount++;
            Debug.Log("HP=" + hp);
            if (hitCount >= 40 && !isDamaged)
            {
                StartCoroutine(DamagedRoutine());
            }
        }
    }
    IEnumerator DamagedRoutine()
    {

        isDamaged = true;

        //攻撃キャンセル
        isDashing = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        animator.SetTrigger("Damaged");

        yield return new WaitForSeconds(0.5f);

        if (!isReturning)
        {
            yield return StartCoroutine(ReturnToOrigine(0.5f));
        }
        hitCount = 0;
        isDamaged = false;
    }

    IEnumerator BossRoutine()
    {
        yield return new WaitForSeconds(3f); // 初期演出など

        while (true)
        {
            // 被弾で中断されたら再開まで待機
            while (isDamaged) yield return null;

            // currentState = BossState.Shooting;
            // for (int i = 0; i < 3; i++)
            // {
            //     ShootAttack();
            //     yield return new WaitForSeconds(0.1f);
            // }

            // currentState = BossState.Waiting;
            // yield return new WaitForSeconds(2f);

            currentState = BossState.Shooting;
            for (int i = 0; i < 3; i++)
            {
                ForceShootAttack();
                yield return new WaitForSeconds(0.8f);
            }

            currentState = BossState.Waiting;
            yield return new WaitForSeconds(2f);

            currentState = BossState.Charging;
            MoveAttack();

            // Zが75超えたら戻る（Updateで検知）
            yield return new WaitUntil(() => !isReturning);

            currentState = BossState.Waiting;
            yield return new WaitForSeconds(2f);
        }
    }


}
