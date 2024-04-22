using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PavileBehavior : MonoBehaviour, IDamagaeble
{

    [Header("Basic Info")]
    [SerializeField] float currentLife;
    [SerializeField] float maxLife;

    bool canMove;
    [SerializeField] float pavileViewDistance;
    [SerializeField] float pavileExplosionDistance;
    [SerializeField] ParticleSystem explosionParticles;

    [SerializeField] float damageRadius = 5f;
    [SerializeField] int damageAmount = 1;

    GameObject playerObject;
    NavMeshAgent navMeshAgent;
    private bool canShoot;
    
    void Start()
    {
        currentLife = maxLife;
        canMove = true;
        playerObject = GameObject.Find("Player_Robot");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer(){
        if(canMove){
            if(Vector3.Distance(transform.position, playerObject.transform.position) < pavileViewDistance){
                print("player detect");
                navMeshAgent.SetDestination(playerObject.transform.position);
                if(Vector3.Distance(transform.position, playerObject.transform.position) < pavileExplosionDistance){
                    Explode();
                    Destroy(this.gameObject);
                    canMove = false;
                }
            }
            else{
                return;
            }
        }

            
    }

    void Explode(){

        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        // Play the explosion animation
        explosionParticles.Emit(1);

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        // Loop through all the colliders
        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to an enemy
            if (collider.CompareTag("Player"))
            {
                // Calculate the distance between the collider and the explosion point
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                
                // Deal damage to the enemy
                // collider.GetComponent<IDamagable>().DealDamage(damageAmount);
            }
        }

        Destroy(this.gameObject);
    }


    public void DealDamage(){
        currentLife -= 1;
        if(currentLife <= 0) Die();
    }

    void Die(){
        Destroy(this.gameObject);
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        explosionParticles.Emit(1);
    }


}
