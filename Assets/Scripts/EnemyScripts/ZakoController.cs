using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoController : MonoBehaviour
{
    public float speed = 3f;
    public float steerStrength = 0.1f;

    private Transform player;

    void Start()
    {
        Application.targetFrameRate = 60;

        // タグでプレイヤーを探す
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("[Zako] プレイヤーが見つかりません！");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceZ = player.position.z - transform.position.z;

        if (distanceZ > 10f)
        {
            Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
            Vector3 directionToPlayer = (targetPos - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, steerStrength * Time.deltaTime);
            // *2.5fは回転分の速度補正
            transform.position += transform.forward * speed * Time.deltaTime * 2.5f;

            
        }
        else
        {

            transform.position += transform.forward * (speed / 0.7f) * Time.deltaTime;
        }
        if (transform.position.z > player.position.z + 30f)
        {
            Destroy(gameObject);
        }
    }
}