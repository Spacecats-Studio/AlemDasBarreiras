using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class menuManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    public void StartGame(){
        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence(){
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);

    }
}
