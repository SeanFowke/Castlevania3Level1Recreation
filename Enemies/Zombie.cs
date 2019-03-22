using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public Player Z_Pl;
    [SerializeField] float Z_CheckDistance;
    void Start()
    {
        E_Initialise();
        Z_Pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        Z_Move();
        
    }

    void Z_Move()
    {
        float check = Z_Pl.transform.position.x - transform.position.x;
        if (check <= Z_CheckDistance && check >= -Z_CheckDistance)
        {

            E_MoveHorizontal("Left", 0.5f);
        }

    }
    public override void E_Hunchback()
    {

    }
}
