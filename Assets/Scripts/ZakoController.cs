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
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        if (player != null && distanceZ > 10f)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 forwardishDirection = Vector3.Lerp(transform.forward, directionToPlayer, steerStrength * Time.deltaTime).normalized;

            // transform.position += direction * speed * Time.deltaTime;
            //Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.LookRotation(forwardishDirection);
            transform.Translate(Vector3.forward * speed * Time.deltaTime * 0.5f);
        }
        else
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
