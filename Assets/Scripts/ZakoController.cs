using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoController : MonoBehaviour
{
    public float speed = 3f;
    public Transform player;
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
        float distanceZ = Mathf.Abs(player.position.z -transform.position.z);
        if (player != null && distanceZ > 10)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
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
