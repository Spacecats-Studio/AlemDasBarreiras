using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class GruntBehavior : MonoBehaviour
{
    
    [SerializeField] GameObject gruntBullet;
    [SerializeField] GameObject GunPoint;
    [SerializeField] float grunchViewDistance;
    [SerializeField] float grunchShootDistance;
    [SerializeField] AudioClip laserSound;

    GameObject playerObject;
    NavMeshAgent navMeshAgent;
    private bool canShoot;

    void Start()
    {
        canShoot = true;
        playerObject = GameObject.Find("Player_Robot");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
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
}
