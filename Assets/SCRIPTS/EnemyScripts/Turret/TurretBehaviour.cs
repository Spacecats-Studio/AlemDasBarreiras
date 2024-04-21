using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour, IDamagaeble
{
    [Header("Basic Info")]
    [SerializeField] float currentLife;
    [SerializeField] float maxLife;


    [Header("Turret Stats")] // melhorar nome
    [SerializeField] Transform TurretHead;
    [SerializeField] GameObject target; // O alvo para o qual a torreta deve girar
    [SerializeField] float rotationSpeed = 5f; // Velocidade de rotação da torreta
    [SerializeField] float shootCoolDown = 1f; // Tempo de espera entre os disparos
    [SerializeField] GameObject bullet; // Prefab da bala
    [SerializeField] Transform bulletPoint; // Ponto de origem do disparo
    [SerializeField] float maxRange = 10f; // Tempo de espera entre os disparos
    private bool canShoot = true; // Flag para controlar se a torreta pode disparar

    [Header("Components")]
    [SerializeField] ParticleSystem explosionParticles;
    AudioSource shootSound;


    void Start()
    {
        shootSound = GetComponent<AudioSource>();
        target = GameObject.Find("Player_Drone");
        currentLife = maxLife;
    }

    void Update()
    {
        if (target != null)
        {
            if(Vector3.Distance(transform.position, target.transform.position) <= maxRange){    
                // checar se o player está em distancia de tiro
                // Calcula a direção do alvo
                Vector3 targetDirection = target.transform.position - transform.position;
                // Calcula a rotação necessária para mirar no alvo
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                // Aplica o Slerp para suavizar a rotação em todas as três dimensões
                TurretHead.rotation = Quaternion.Slerp(TurretHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Verifica se a torreta pode disparar e se a tecla de disparo foi pressionada
                if (canShoot)
                {

                    Shoot(); // Dispara a torreta
                    shootSound.Play();
                }
                } else {
                    return;
                }
            }
    }

    void Shoot()
    {
        // Instancia uma nova bala na posição do ponto de origem do disparo
        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);

        // Inicia a contagem do cooldown
        StartCoroutine(BeginShootCooldown());
    }

    IEnumerator BeginShootCooldown()
    {
        canShoot = false; // A torreta não pode atirar durante o cooldown
        yield return new WaitForSeconds(shootCoolDown); // Aguarda o tempo de cooldown
        canShoot = true; // A torreta pode atirar novamente após o cooldown
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
