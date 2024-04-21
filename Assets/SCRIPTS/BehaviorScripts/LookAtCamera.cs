using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Calculate the direction from the canvas to the camera
            Vector3 lookDir = mainCamera.transform.position - transform.position;

            // Ensure the canvas faces the camera but stays upright
            transform.rotation = Quaternion.LookRotation(-lookDir, Vector3.up);
        }
        else
        {
            Debug.LogWarning("Main camera not found in the scene!");
        }
    }
}
