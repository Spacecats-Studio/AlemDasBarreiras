using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    [SerializeField] Animator animator;
    Controller_Robot controller_Robot;
    public bool isOnHackingArea;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("IsIdle");
        isOnHackingArea = false;
        controller_Robot = GetComponent<Controller_Robot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller_Robot.isOnControl){
            if(!isOnHackingArea){    
                if(Input.GetAxisRaw("Horizontal") != 0  || Input.GetAxisRaw("Vertical") != 0){
                    animator.SetTrigger("IsWalking");
                } else {
                    animator.SetTrigger("IsIdle");
                }
            }
        }    
        if(isOnHackingArea){
            animator.SetTrigger("isHacking");
            
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("HackingArea")){
            isOnHackingArea = true;
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("HackingArea")){
            isOnHackingArea = false;
        }
    }



}
