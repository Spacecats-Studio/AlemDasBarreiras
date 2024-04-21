using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YallAnimationController : MonoBehaviour
{
    private float xMovInput;
    private float zMovInput;
    public float tiltSpeed = 5f; // velocidade de inclinação do drone

    Controller_Drone controller_Drone;
    

    void Start(){
        controller_Drone = GetComponent<Controller_Drone>();
    }


    // Update is called once per frame
    void Update()
    {
        HandleInputForYall();
        yallDrone(); // chama a função para inclinar o drone
    }

    void yallDrone()
    {
        if(controller_Drone.isOnControl){
            // Calcula a inclinação do drone com base nos inputs do jogador
            Quaternion targetRotation = Quaternion.Euler(zMovInput * tiltSpeed, transform.localRotation.y, -xMovInput * tiltSpeed);
            // Suaviza a transição da rotação atual para a rotação desejada
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);    
        }
    }

    void HandleInputForYall()
    {
        xMovInput = Input.GetAxisRaw("Horizontal");
        zMovInput = Input.GetAxisRaw("Vertical");
    }
}
