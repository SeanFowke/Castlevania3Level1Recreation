using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : Enemy
{
    [SerializeField] float M_Speed;
    [SerializeField] string Dir;
    private Player M_Pl;
    [SerializeField] float M_CheckDistance;
    void Start()
    {
        M_Pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        E_Initialise();
        float check = M_Pl.transform.position.x - transform.position.x;
        if (check <= M_CheckDistance && check >= -M_CheckDistance)
        {
            E_MoveHorizontal(Dir, M_Speed);
        }
    }
    public override void E_Hunchback()
    {

    }
}
