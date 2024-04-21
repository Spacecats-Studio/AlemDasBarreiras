using System.Collections;
using UnityEngine;

public class DroneShooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] AudioClip shootSound;
    PickPlayer pickPlayer;
    private bool canShoot;


    void Start()
    {
        canShoot = true;
        pickPlayer = GetComponent<PickPlayer>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && canShoot && GameManager.Instance.Current_Character.name == "Player_Drone")
        {
            if (!pickPlayer.RobotOnDrone)
            {
                ShootProjectile();
                GameManager.Instance.PlaySound(shootSound);
            }
        }
    }

    void ShootProjectile()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition -= new Vector3(0, 2, 0);
        // Convert the mouse position to a world position on the plane where the projectile will move
        mousePosition.z = Camera.main.transform.position.y; // Set the z position to the plane's position
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Instantiate projectile at player's position
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Calculate the direction to shoot
        Vector3 shootDirection = (targetPosition - transform.position).normalized;

        // Get the rigidbody component of the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Set the velocity of the rigidbody to shoot the projectile
            rb.velocity = shootDirection * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found on projectile prefab.");
        }
    }


    IEnumerator BeginShootCooldown(){
        canShoot = false;
        yield return new WaitForSeconds(1);
        canShoot = true;
    }
}
