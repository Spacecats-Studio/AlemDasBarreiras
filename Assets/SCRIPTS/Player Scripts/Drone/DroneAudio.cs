using UnityEngine;

public class DroneAudio : MonoBehaviour
{
    public AudioSource audioSource; // Referência ao AudioSource
    private Transform mainCameraTransform; // Referência ao Transform da câmera principal
    public float maxDistance = 10f; // Distância máxima em que o áudio será ouvido
    public float minVolume = 0.1f; // Volume mínimo quando a câmera estiver na distância máxima

    void Start()
    {
        // Encontra a câmera principal na cena
        mainCameraTransform = Camera.main.transform;

        // Se não foi atribuído um AudioSource, tenta encontrar um na mesma GameObject
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Calcula a distância entre este objeto e a câmera principal
        float distanceToCamera = Vector3.Distance(transform.position, mainCameraTransform.position);

        // Calcula o volume com base na distância
        float volume = Mathf.Clamp(1f - (distanceToCamera / maxDistance), minVolume, 1f);

        // Define o volume do áudio
        audioSource.volume = volume;
    }
}
