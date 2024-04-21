using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingAreaBehavior : MonoBehaviour
{

    TowerComponent towerComponent;
    [SerializeField] float hackingDamage;

    // Start is called before the first frame update
    void Start()
    {
        towerComponent = transform.parent.gameObject.GetComponent<TowerComponent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if(!towerComponent.questionSet){    
            if(other.gameObject.name == "Player_Robot"){
                towerComponent.resistence -= hackingDamage * Time.deltaTime;
            }
        }
        
    }
}
