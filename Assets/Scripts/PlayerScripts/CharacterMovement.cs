using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5.0f; // 移動速度（Inspectorで調整可能）

    //05/06追記
    CharacterController cc;
    public float gravity = -9.81f; // 重力加速度
    private float verticalVelocity = 0f; // 落下速度保持

    //05/08追記
    bool isDead;
    public bool IsDead => isDead;

    //=== 2025/05/13 追記: タッチUI操作用 ===
    private float uiInput = 0f;
    //====================================

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animatorが見つかりません！");
        }

        //05/06追記
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //05/08 追記　死んだときの挙動
        if (isDead)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        // 常にRun_Shooterをアクティブ
        animator.SetBool("isShooting", true);

        //=== 2025/05/13 変更: UI入力があれば優先 ===
        float keyInput = Input.GetAxisRaw("Horizontal");
        float moveInput = uiInput != 0 ? uiInput : keyInput; // タッチUI対応
        //================================================

        bool isMoving = moveInput != 0;
        animator.SetBool("isMoving", isMoving);

        // 重力処理  05/06追記
        if (cc.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 move = new Vector3(-moveInput * moveSpeed, verticalVelocity, 0);
        cc.Move(move * Time.deltaTime);
    }

    //05/08追記
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Rock"))
        {
            if (isDead) return;

            isDead = true;
            SoundManager.Instance.playDeathByRock();
            animator.SetBool("isDead", true);
        }
    }

    //=== 2025/05/13 追記: タッチUI用の操作関数 ===
    public void MoveLeft() => uiInput = -1f;
    public void MoveRight() => uiInput = 1f;
    public void StopMove() => uiInput = 0f;
    //==========================================
}
