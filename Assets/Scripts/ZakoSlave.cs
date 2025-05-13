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
                Vector3 forwardPos = transform.position + transform.forward;
                moveDirection = (forwardPos - transform.position).normalized;
                isFree = true;
                return;
            }

            Vector3 targetPos = leader.TransformPoint(offsetFromLeader);
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(targetPos - transform.position);
        }
        else
        {
            Vector3 nextPos = transform.position + moveDirection;
            transform.position = Vector3.MoveTowards(transform.position, nextPos, followSpeed * Time.deltaTime * 0.2f);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

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