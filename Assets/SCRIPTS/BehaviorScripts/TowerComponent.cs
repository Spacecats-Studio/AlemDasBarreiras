using UnityEngine;
using UnityEngine.UI;

public class TowerComponent : MonoBehaviour
{
    // Variáveis públicas acessíveis no Inspector
    public float resistence;
    public Slider resistenceBarSlider;
    public float maxResistenceValue = 100f;
    [SerializeField] GameObject sliderObject;
    [SerializeField] int questionIndex;
    [SerializeField] GameObject Question;


    [SerializeField] GameObject GreenEnergy;
    [SerializeField] GameObject RedEnergy;
    

    // Variável para controlar se a pergunta já foi definida
    public bool questionSet = false;
    public bool hacked = false;

    




    // Chamado uma vez no início do script
    void Start()
    {
        questionSet = false;
        // Configura o valor inicial do Slider
        UpdateSliderValue(maxResistenceValue);
    }

    // Chamado a cada frame
    void Update()
    {
        UpdateSliderValue(maxResistenceValue); // Atualiza a UI do Slider de resistencia da torres
        

        CheckResistance();
    }

    // Verifica a resistência e realiza ações com base nela
    void CheckResistance()
    {

        // Se o objeto do slider existir, atualize o valor do Slider
        if (sliderObject)
        {
            // Se a resistência for menor ou igual a 20 e a pergunta ainda não foi definida
            if (resistence <= 20 && !questionSet)
            {
                // Define a pergunta e marca que a ação foi realizada
                GameManager.Instance.SetQuestion(questionIndex);
                questionSet = true;
            }


            // Se a resistência for menor ou igual a 0, desative o objeto do slider
            if (resistence <= 0)
            {
                sliderObject.SetActive(false);
            }            
        }


    }

    // Atualiza o valor do Slider com base na resistência atual
    void UpdateSliderValue(float maxResistenceValue)
    {
        // Se o objeto do slider existir, atualize o valor do Slider
        if (sliderObject)
        {
            // Calcula o valor relativo do Slider com base na resistência e no valor máximo de resistência
            float sliderValue = resistence / maxResistenceValue;
            resistenceBarSlider.value = sliderValue;
        }
    }


    public void CorrectAnswer(){
            // avisa o gameManager
            Destroy(Question); // retira o pop up da tela
            hacked = true;
            resistence = 0;
            Destroy(sliderObject);
            GameManager.Instance.RestoreTime(); // volta o tempo ao normal
            GameManager.Instance.TowerHacked(); // avisa o GameManager que uma torre foi hackeada
            GameObject go = Instantiate(RedEnergy, GreenEnergy.transform.position, Quaternion.identity);
            go.transform.localScale = new Vector3(2.105425f,2.105425f,2.105425f);
            
            Destroy(GreenEnergy);
            
        }
    

    public void IncorrectAnswer(){
        ResetTower();
        GameManager.Instance.RestoreTime(); // volta o tempo ao normal
    }

    // Chamada para resetar as propriedades da torre
    public void ResetTower(){
        questionSet = false;
        resistence = maxResistenceValue;
        Question.transform.position -= new Vector3(0,1000,0);
    }


    // resposta incorreta - Resetar torre
    // colocar pergunta embaixo da tela de novo

}
