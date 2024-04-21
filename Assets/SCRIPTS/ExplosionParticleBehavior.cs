using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleBehavior : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 0.5f);
    }
}
