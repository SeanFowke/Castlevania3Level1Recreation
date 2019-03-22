using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody2D E_Rb;
    private SpriteRenderer E_Sr;
    private Player E_Pl;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void E_Initialise()
    {
        E_Pl = GameObject.Find("Player").GetComponent<Player>();
        E_Rb = GetComponent<Rigidbody2D>();
        E_Sr = GetComponent<SpriteRenderer>();
    }

    public virtual void E_MoveHorizontal(string dir, float speed)
    {
        if (E_Pl.P_UpgradedWhipTimer == false)
        {
            if (dir == "Right")
            {
                E_Rb.velocity = new Vector2(speed, E_Rb.velocity.y);
            }
            else if (dir == "Left")
            {
                E_Rb.velocity = new Vector2(-speed, E_Rb.velocity.y);
            }
        }
    }

    public abstract void E_Hunchback();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("knife"))
        {
            Destroy(gameObject);
        }
        if (col.CompareTag("Whip"))
        {
            Destroy(gameObject);
        }
    }
}
