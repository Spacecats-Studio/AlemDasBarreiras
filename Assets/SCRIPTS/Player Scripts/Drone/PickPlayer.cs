using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickPlayer : MonoBehaviour
{

    public bool RobotOnDrone; 
    public GameObject playerRobot; 

    void Awake(){
        
    }

    void Update(){    
        CarryPlayer();
        if(Input.GetKeyDown(KeyCode.P)){
            PickPlayerUp();
            
        }
    }


    void PickPlayerUp(){
        if(!RobotOnDrone){            
            RaycastHit hit;            
            if (Physics.Raycast(transform.position, -transform.up, out hit, 1f, 3)){
                RobotOnDrone = true;    
            }
        } else {
            RobotOnDrone = false;
        }


    }

    void CarryPlayer(){
        if(RobotOnDrone){
            if(playerRobot){
                playerRobot.transform.position = transform.position - new Vector3(0,0.3f,0);
                playerRobot.transform.rotation = transform.rotation;
            }
            
        }
    }

}
