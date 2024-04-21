using UnityEngine;

public class HackingSound : MonoBehaviour
{
    public AudioSource triggerSound; // Assign the trigger sound to this in the Unity Inspector

    void Start()
    {
        triggerSound = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if(other.gameObject.name == "Player_Robot"){
            // Start playing the trigger sound
            if (!triggerSound.isPlaying)
            {
                triggerSound.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player_Robot"){
            triggerSound.Stop();
            
        }
    }
}