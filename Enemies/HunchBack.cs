using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunchBack : Enemy
{
    private Player HB_Pl;
    [SerializeField] float HB_CheckDistance;
    [SerializeField] float HB_Speed;
    [SerializeField] float HB_MaxLungeDistanceX;
    private SpriteRenderer HB_Sr;
    private bool HB_IsGrounded;
    void Start()
    {
        E_Initialise();
        HB_Pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        HB_Sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveHunchback();
    }

    public void MoveHunchback()
    {
        float check = HB_Pl.transform.position.x - transform.position.x;
        float LungeDistanceX = HB_Pl.transform.position.x - transform.position.x;
        if (LungeDistanceX >= HB_MaxLungeDistanceX) { LungeDistanceX = HB_MaxLungeDistanceX; }
        LungeDistanceX *= Random.Range(0.5f, 1);
        float HB_LungeY = Random.Range(2, 6.5f);
        
        if (check <= HB_CheckDistance && check >= -HB_CheckDistance)
        {
            if (check < 0)
            {
                if (HB_IsGrounded == true)
                {
                    E_Rb.velocity = new Vector2(LungeDistanceX, HB_LungeY);
                    HB_IsGrounded = false;
                    HB_Sr.flipX = false;
                }
            }
             if (check > 0)
            {
                if (HB_IsGrounded == true)
                {
                    E_Rb.velocity = new Vector2(LungeDistanceX, HB_LungeY);
                    HB_IsGrounded = false;
                    Debug.Log("Hello");
                    HB_Sr.flipX = true;
                }
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Platform"))
        {
            HB_IsGrounded = true;
            
        }
    }
}
