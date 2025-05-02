using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5.0f; // 移動速度（Inspectorで調整可能）

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animatorが見つかりません！");
        }
    }

    void Update()
    {
        // 常にRun_Shooterをアクティブ
        animator.SetBool("isShooting", true);

        // A/Dキーまたは左右矢印キーの入力を検出
        float moveInput = Input.GetAxisRaw("Horizontal"); // -1 (左), 0 (なし), 1 (右)
        bool isMoving = moveInput != 0;
        animator.SetBool("isMoving", isMoving);

        // キャラクターの移動
        if (isMoving)
        {
            transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);
        }
    }
}