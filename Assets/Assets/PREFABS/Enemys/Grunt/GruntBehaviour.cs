using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class GruntBehaviour : MonoBehaviour, IDamagaeble
{
    

    [Header("Turret Stats")]
    [SerializeField] float currentLife;
    [SerializeField] float maxLife;
    [SerializeField] float grunchViewDistance;
    [SerializeField] float grunchShootDistance;
    private bool canShoot;

    [Header("Components")]
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] GameObject gruntBullet;
    [SerializeField] GameObject GunPoint;
    [SerializeField] AudioClip laserSound;
    GameObject playerObject;
    NavMeshAgent navMeshAgent;
    

    void Start()
    {
        canShoot = true;
        playerObject = GameObject.Find("Player_Robot");
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentLife = maxLife;
    }

    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer(){
        if(playerObject == null){
            return;
        }
        if(Vector3.Distance(transform.position, playerObject.transform.position) < grunchViewDistance){
            print("player detect");
            navMeshAgent.SetDestination(playerObject.transform.position);
            if(Vector3.Distance(transform.position, playerObject.transform.position) < grunchShootDistance && canShoot){
                Shoot();
                GameManager.Instance.PlaySound(laserSound);
            }
            
        }
        else{
            return;
        }
    }

    void Shoot(){
        StartCoroutine(BeginShootCooldown());
        // Instancia uma nova bala na posição do ponto de origem do disparo
        Instantiate(gruntBullet, GunPoint.transform.position, GunPoint.transform.rotation);
    }
    IEnumerator BeginShootCooldown(){
        canShoot = false;
        yield return new WaitForSeconds(2);
        canShoot = true;
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
