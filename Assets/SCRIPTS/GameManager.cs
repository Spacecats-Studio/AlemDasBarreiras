using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Header("UI ELEMENTS")]
    [SerializeField] RectTransform QuestionScreen1;
    [SerializeField] RectTransform QuestionScreen2;
    [SerializeField] RectTransform QuestionScreen3;
    [SerializeField] RectTransform QuestionScreen4;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Ease easeType;
    [SerializeField] float animationDuration;

    

    [Header("LOGIC")]
    public bool isQuestionHappening;
    
    
    
    [Header("Characters")]
    [SerializeField] public GameObject Current_Character;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject Player_drone;
    [SerializeField] GameObject Player_robot;
    Controller_Robot controller_Robot;
    Controller_Drone controller_Drone;
    [SerializeField] PickPlayer pickPlayer;
    [Header("Characters Camera Offset and position")]
    [SerializeField] Vector3 drone_LookOffset;
    [SerializeField] Vector3 drone_PositionOffset;
    [SerializeField] Vector3 robot_LookOfsset;
    [SerializeField] Vector3 robot_PositionOfsset;

    [Header("Towers")]
    [SerializeField] TowerComponent tower1;
    [SerializeField] TowerComponent tower2;
    [SerializeField] TowerComponent tower3;
    [SerializeField] TowerComponent tower4;


    
    // Private Variables
    Camera _camera;
    Camera_Controller camera_controller;
    int towers_hacked = 0;
    AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
        _camera = Camera.main; // get main camera used on scene
        audioSource = GetComponent<AudioSource>(); // Gets audio source component
        camera_controller = _camera.GetComponent<Camera_Controller>(); // get camera controller
        controller_Robot = GameObject.FindAnyObjectByType<Controller_Robot>();
        controller_Drone = GameObject.FindAnyObjectByType<Controller_Drone>();
        
        isQuestionHappening = false;    

        // SET UP INITIAL CHARACTER
        Current_Character = Player_drone;
        camera_controller.positionOffset = drone_PositionOffset;
        camera_controller.lookOffset = drone_LookOffset;

        
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            ChangeCharacter();
        }
        
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(1);
        }
        
    }

    public void ChangeCharacter(){
        // Alterna entre Drone e Robô
        if (Current_Character == Player_robot) {
            camera_controller.target = Player_drone.transform;
            camera_controller.positionOffset = drone_PositionOffset;
            camera_controller.lookOffset = drone_LookOffset;

            Current_Character = Player_drone;
            controller_Robot.isOnControl = false;
            controller_Drone.isOnControl = true;
        } 
        // Se o jogador atual for o drone, alterna para o robô
        else {
            playerAnimator.SetTrigger("IsIdle");
            // checar se o robo não está atracado no drone
            if(!pickPlayer.RobotOnDrone){
                camera_controller.target = Player_robot.transform;
                camera_controller.positionOffset = robot_PositionOfsset;
                camera_controller.lookOffset = robot_LookOfsset;

                Current_Character = Player_robot;
                controller_Robot.isOnControl = true;
                controller_Drone.isOnControl = false;
            }
            
        }
    }

    // TOWER FUNCTIONALITY 
    // ativa objeto de ui da pergunta
    // quando a pergunta for respondida corretamente 
    public void SetQuestion(int index){
        switch(index){
            case 1:
                if(Current_Character != Player_robot) ChangeCharacter(); // Se o personagem ativo for o drone muda
                PopUpQuestionScreen(QuestionScreen1);
                Time.timeScale = 0.2f;
            break;
            case 2:
                if(Current_Character != Player_robot) ChangeCharacter();
                PopUpQuestionScreen(QuestionScreen2);
                Time.timeScale = 0.2f;

            break;
            case 3:
                if(Current_Character != Player_robot) ChangeCharacter();
                PopUpQuestionScreen(QuestionScreen3);
                Time.timeScale = 0.2f;

            break;
            case 4:
                if(Current_Character != Player_robot) ChangeCharacter();
                PopUpQuestionScreen(QuestionScreen4);
                Time.timeScale = 0.2f;

            break;
        }
    }

    void PopUpQuestionScreen(RectTransform uiObject){
        // Definir a posição inicial do objeto de UI no ponto A
        uiObject.position = pointA.position;

        // Iniciar a animação utilizando o DoTween
        uiObject.DOMove(pointB.position, animationDuration).SetEase(easeType); // Movendo o objeto para o ponto B
        
    }

    public void TowerHacked(){
        towers_hacked += 1;
        CheckAllTowersHacked();
    }

    public void CheckAllTowersHacked(){
        if(towers_hacked >= 4){
            GameWon();
        }
    }

    public void RestoreTime(){
        Time.timeScale = 1f;
    }

    void GameWon(){
        SceneManager.LoadScene(1);
    }

    public void GameOver(){
        // u failed in save the world
    }

    public void PlaySound(AudioClip sound){
        audioSource.clip = sound;
        audioSource.Play();

    }

}