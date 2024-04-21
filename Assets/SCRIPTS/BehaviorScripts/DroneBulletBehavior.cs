using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBulletBehavior : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] float damageRadius = 5f;
    [SerializeField] int damageAmount = 1;

    void Start()
    {
        Destroy(gameObject, 4f);
    }

    void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    void Explode()
    {
        // Instantiate the explosion particles at the collision position
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        // Play the explosion animation
        explosionParticles.Emit(1);

        // Find all colliders within the damage radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);

        // Loop through all the colliders
        foreach (Collider collider in colliders)
        {
            IDamagaeble damagaeble = collider.GetComponent<IDamagaeble>();
            // Check if the collider belongs to an enemy
            if (damagaeble != null)
            {
                // Calculate the distance between the collider and the explosion point
                // float distance = Vector3.Distance(transform.position, collider.transform.position);
                // Calculate damage based on distance from the explosion point
                // int calculatedDamage = Mathf.RoundToInt((1 - (distance / damageRadius)) * damageAmount);
                // ! DEPRECATEAD: Since our Interface doesn't actually contays a way to pass a damage vallue, this calculation is not 
                // ! needed anymore
                // Deal damage to the enemy
                damagaeble.DealDamage();

                
            }
        }

        // Destroy the bullet
        Destroy(gameObject);
    }
}
