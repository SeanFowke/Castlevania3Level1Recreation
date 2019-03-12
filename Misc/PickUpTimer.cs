using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTimer : MonoBehaviour
{
    [SerializeField] float PT_DestroyTime;
    

    
    void Update()
    {
        Destroy(gameObject, PT_DestroyTime);
    }
}
