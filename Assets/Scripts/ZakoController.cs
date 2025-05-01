using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoController : MonoBehaviour
{
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
       Application.targetFrameRate = 60; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,0,speed*0.1f));
    }
        void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
