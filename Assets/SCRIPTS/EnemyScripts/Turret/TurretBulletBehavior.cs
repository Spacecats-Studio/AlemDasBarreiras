using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class TurretBulletBehavior : MonoBehaviour
{

    [SerializeField] float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.forward  * Time.deltaTime * speed);
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Turret")){
            return;       
        } 
        if(other.gameObject.CompareTag("Player")){
            // other.gameObject.GetComponent<IDamagable>().DealDamage(3);
            
            Destroy(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}
