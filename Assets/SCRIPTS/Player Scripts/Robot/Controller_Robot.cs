using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Controller_Robot : MonoBehaviour
{
    
    
    public bool isOnControl;
    [SerializeField] float _speed;
    [SerializeField] float rotationSpeed = 5f;

    float xMovInput;
    float zMovInput;
    Vector3 previousPosition;
    Rigidbody rb;
    

    void Start()
    {
        Physics.gravity = new Vector3(0, -3.0F, 0);
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    void Update()
    {
        if(isOnControl){    
            HandleInput();
            MoveCharacter();
            RotateCharacter();
        }
    }

    void HandleInput(){
        xMovInput = Input.GetAxis("Horizontal");
        zMovInput = Input.GetAxis("Vertical");
    }

    void MoveCharacter(){
        transform.position += new Vector3(xMovInput, 0, zMovInput) * Time.deltaTime * _speed;
    }

    void RotateCharacter(){
        // Verifica se há alguma entrada de movimento do jogador
        if (AnyInputHappening())
        {
            // Calcula a direção do movimento do personagem
            Vector3 direction = (transform.position - previousPosition).normalized;

            // Se a direção do movimento não for zero, ou seja, o personagem está se movendo
            if (direction != Vector3.zero)
            {
                // Calcula a rotação desejada usando LookRotation
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Suaviza a rotação atual em direção à rotação desejada usando a interpolação de quaternions
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
    }

    // Atualiza a posição anterior para o próximo quadro
    previousPosition = transform.position;
    }

    bool AnyInputHappening(){
        if(xMovInput != 0 ||  zMovInput != 0){
            return true;
        } else {
            return false;
        }
    }
} 
