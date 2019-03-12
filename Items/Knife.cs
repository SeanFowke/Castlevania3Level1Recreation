using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

    private Player K_Pl;
    private Rigidbody2D K_Rb;
    private SpriteRenderer K_Sr;
    public float K_Speed;
    private bool K_Right;
    public float K_Timer;
	void Start ()
    {
        K_Pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        K_Rb = GetComponent<Rigidbody2D>();
        K_Sr = GetComponent<SpriteRenderer>();
        K_Right = K_Pl.P_IsRight;
	}
	
	// Update is called once per frame
	void Update ()
    {
        K_Move();

    }

    public void K_Move()
    {
        if (K_Right==true)
        {
            K_Rb.velocity = new Vector2(K_Speed, 0);
            K_Sr.flipX = false;
            K_Timer -= Time.deltaTime;
        }
        else
        {
            K_Rb.velocity = new Vector2(-K_Speed, 0);
            K_Sr.flipX = true;
            K_Timer -= Time.deltaTime;
        }
        if (K_Timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
