using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.LeftArrow)){
        transform.Translate(new Vector3(1,0,0));
       }
        if(Input.GetKey(KeyCode.RightArrow)){
        transform.Translate(new Vector3(-1,0,0));
       } 
    }
}
