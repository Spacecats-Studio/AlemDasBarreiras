using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBulletBehavior : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;
    [SerializeField] float speed = 5f;
    

    void Update()
    {
        transform.Translate(Vector3.forward  * Time.deltaTime * speed);
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy")){ // ignore self
            return;       
        } 
        if(other.gameObject.CompareTag("Player")){
            // other.gameObject.GetComponent<IDamagable>().DealDamage(1);
            Instantiate(explosion, transform.position, Quaternion.identity);
            // Play the explosion animation
            explosion.Emit(1);
            Destroy(this.gameObject);
        } else {
            Instantiate(explosion, transform.position, Quaternion.identity);
            // Play the explosion animation
            explosion.Emit(1);
            Destroy(this.gameObject);
        }

        

    }
}
