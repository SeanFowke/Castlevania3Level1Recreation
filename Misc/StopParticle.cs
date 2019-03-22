using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParticle : MonoBehaviour
{
    ParticleSystem SP;
    void Start()
    {
        SP = gameObject.GetComponent<ParticleSystem>();
        SP.Stop();
    }
}
