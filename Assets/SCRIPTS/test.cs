using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class test : MonoBehaviour
{
    public RectTransform uiObject; // Referência ao objeto de UI que será animado
    public Transform pointA; // Ponto de partida
    public Transform pointB; // Ponto de chegada
    public Ease easeType; // Ponto de chegada

    void Start()
    {
        // Definir a posição inicial do objeto de UI no ponto A
        uiObject.position = pointA.position;

        // Iniciar a animação utilizando o DoTween
        uiObject.DOMove(pointB.position, 0.3f).SetEase(easeType); // Movendo o objeto para o ponto B

        // Também podemos animar a opacidade do objeto para que ele pareça estar aparecendo gradualmente
        uiObject.GetComponent<CanvasGroup>().DOFade(1, 1f).SetDelay(0.5f); // Definindo a opacidade para 1 (totalmente visível)
    }
}