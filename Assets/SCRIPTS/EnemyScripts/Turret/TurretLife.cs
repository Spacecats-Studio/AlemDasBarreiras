using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLife : MonoBehaviour
{
    public float currentLife;
    public float maxLife;

    void Start(){
        currentLife = maxLife;
    }

    public void DealDamage(int quantity){
        currentLife -= quantity;
        if(currentLife <= 0) Die();
    }

    public void RecoverLife(int quantity){
        if(currentLife + quantity > maxLife) currentLife = maxLife;
        else currentLife += quantity;
    }

    void Die(){
        Destroy(this.gameObject);
        // tocar sistema de particulas (explosao) na posiçao da torreta
        
    }
}
