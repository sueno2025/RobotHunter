using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ZakoController : MonoBehaviour
{
    public float speed = 3f;
    public Transform player;
    public float steerStrength = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //inspectorからplayerの登録をするように変更する
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 moveDirection = Vector3.Lerp(transform.forward, directionToPlayer, steerStrength * Time.deltaTime).normalized;
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        if (player != null && distanceZ > 10f)
        {
            //Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 forwardishDirection = Vector3.Lerp(transform.forward, directionToPlayer, steerStrength * Time.deltaTime).normalized;

            // transform.position += direction * speed * Time.deltaTime;
            //Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.LookRotation(forwardishDirection);
            //transform.Translate(Vector3.forward * speed * Time.deltaTime * 0.5f);
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        else
        {
            //transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        if (player == null) return;

        // プレイヤーに向かって移動（前進）
        transform.position += transform.forward * speed * Time.deltaTime;

        // プレイヤーより z が 30 以上離れたら消す（画面奥へ進んで）
        if (transform.position.z > player.position.z + 30f)
        {
            Debug.Log($"{name} を削除します！");
            Destroy(gameObject);
        }

    }
    // void OnBecameInvisible()
    // {
    //     Destroy(gameObject);
    // }
}
