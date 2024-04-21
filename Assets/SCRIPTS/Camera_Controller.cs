using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform target; // O objeto que a câmera seguirá
    public Vector3 positionOffset; // Deslocamento (offset) da câmera em relação ao alvo
    public Vector3 lookOffset; // Deslocamento (offset) da câmera em relação ao alvo
    

    [SerializeField] float _currentFollowSpeed = 5f;
    
    void Update()
    {
        if (target == null)
        {
            return;
        }

        // Calcule a posição alvo ajustada com o offset
        Vector3 targetPosition = target.position + positionOffset;

        // Interpole suavemente a posição atual da câmera em direção à posição do alvo ajustada
        transform.position = Vector3.Slerp(transform.position, targetPosition, _currentFollowSpeed * Time.deltaTime);

        // Calcule a direção do alvo a partir da posição atual da câmera
        Vector3 lookDirection = (target.position - transform.position).normalized - lookOffset;

        // Calcule a rotação desejada para a câmera
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        // Interpole suavemente a rotação atual da câmera em direção à rotação desejada
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _currentFollowSpeed * Time.deltaTime);
    }
}
