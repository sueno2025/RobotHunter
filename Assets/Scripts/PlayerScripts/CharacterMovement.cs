using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5.0f; // 移動速度（Inspectorで調整可能）
    //05/06追記
    CharacterController cc;
    public float gravity = -9.81f; // 重力加速度
    private float verticalVelocity = 0f; // 落下速度保持

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
        // 常にRun_Shooterをアクティブ
        animator.SetBool("isShooting", true);

        // A/Dキーまたは左右矢印キーの入力を検出
        float moveInput = Input.GetAxisRaw("Horizontal"); // -1 (左), 0 (なし), 1 (右)
        bool isMoving = moveInput != 0;
        animator.SetBool("isMoving", isMoving);

        // キャラクターの移動
        // if (isMoving)
        // {
        //     transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);
        // }


        // 重力処理  05/06追記
        if (cc.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // 接地している時の吸着力
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        // 移動方向のベクトル作成
        Vector3 move = new Vector3(-moveInput * moveSpeed, verticalVelocity, 0);

        // CharacterControllerで移動
        cc.Move(move * Time.deltaTime);
    }
}