using UnityEngine;

public class ZakoSlave : MonoBehaviour
{
    public Transform leader;
    public Vector3 offsetFromLeader;
    public float followSpeed = 5f;
    public Transform player;
    private bool isFree = false;
    private Vector3 moveDirection;

    void Update()
    {
        if (!isFree)
        {
            if (leader == null)
            {
                // 直進方向を固定（滑らかに向いていた方向で）
                Vector3 forwardPos = transform.position + transform.forward;
                moveDirection = (forwardPos - transform.position).normalized;
                isFree = true;
                return;
            }

            // フォーメーション維持
            Vector3 targetPos = leader.TransformPoint(offsetFromLeader);
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(targetPos - transform.position);
        }
        else
        {
            // スムーズな直進（MoveTowardsで等速）
            Vector3 nextPos = transform.position + moveDirection;
            transform.position = Vector3.MoveTowards(transform.position, nextPos, followSpeed * Time.deltaTime * 0.2f);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
         // プレイヤーより z が 90 以上離れたら消す（画面奥へ進んで）
        if (transform.position.z > player.position.z + 90f)
        {
            Debug.Log($"{name} を削除します！");
            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}