using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public AudioSource footstepSound; // Assign the footstep sound to this in the Unity Inspector
    public float walkingSpeed = 5f; // Adjust this according to your character's walking speed
    public float raycastDistance = 0.1f; // Distance to cast the ray downwards

    
    

    private void Update()
    {
        if(GameManager.Instance.Current_Character.name == "Player_Robot"){
            // Check if character is grounded
            if (IsGrounded())
            {
                // Check if character is walking
                if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
                {
                    if (!footstepSound.isPlaying) // Check if the sound is not already playing
                    {
                        footstepSound.Play(); // Start playing the footstep sound
                    }
                }
                else
                {
                    footstepSound.Stop(); // Stop playing the sound if not walking
                }
            }
            else
            {
                footstepSound.Stop(); // Stop playing the sound if not grounded
            }

        } else {
            footstepSound.Stop(); // Stop playing the sound if current character is not the robot lol 
        }
        
    }

    private bool IsGrounded()
    {
        // Cast a ray downwards to check if grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, raycastDistance))
        {
            // Check if the object hit has the tag "Ground"
            if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }
}
