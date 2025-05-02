using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    bool isMoveRight;
    bool isMoveLeft;
    bool isShooting;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.LeftArrow)){
        transform.Translate(new Vector3(1,0,0));
        animator.SetTrigger("moveRight");
       }
        if(Input.GetKey(KeyCode.RightArrow)){
        transform.Translate(new Vector3(-1,0,0));
        isMoveRight = true;
       } 
       isMoveLeft=false;
       isMoveRight = false ;
    }
}
