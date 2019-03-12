using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D PJ_Rb;
    [SerializeField] float PJ_height;
    [SerializeField] float PJ_speed;
    [SerializeField] float PJ_DeathTime;
    private string PZ_Dir;
    void Awake()
    {
        PJ_Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Destroy(gameObject, PJ_DeathTime);
    }

    public void Initialise(string dir)
    {
        PZ_Dir = dir;
    }

    public void PJ_Fire()
    {
        if (PZ_Dir == "Right")
        {
            PJ_Rb.velocity = new Vector2(PJ_speed, PJ_height);
        }
        else if (PZ_Dir == "Left")
        {
            PJ_Rb.velocity = new Vector2(-PJ_speed, PJ_height);
        }  
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && PZ_Dir == "Right")
        {
            PJ_Rb.velocity = new Vector2(PJ_speed, PJ_height);
        }
        else if (col.gameObject.CompareTag("Ground") && PZ_Dir == "Left")
        {
            PJ_Rb.velocity = new Vector2(-PJ_speed, PJ_height);
        }
    }
}
