using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] Player S_Pl;
    private SpriteRenderer S_Sr;
    [SerializeField] float S_Speed;
    [SerializeField] float S_LockTimerInitial;
    [SerializeField] float S_CheckDistance;
    private bool S_Lock;
    private float S_LockTimer;
    private string S_Dir;
    void Start()
    {
        E_Initialise();
        S_Sr = gameObject.GetComponent<SpriteRenderer>();
        S_LockTimer = S_LockTimerInitial;
        S_Lock = false;
        S_Pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSkeleton();
    }

    void MoveSkeleton()
    {
        RaycastHit2D hit = Physics2D.Linecast(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(transform.position.x, transform.position.y-2),13);
        bool debug = hit;
        float check = S_Pl.transform.position.x - transform.position.x;
        if(check <= S_CheckDistance && check >= -S_CheckDistance)
        {
            if (check < 0 && S_Lock == false)
            {
                S_Dir = "Left";
                E_MoveHorizontal(S_Dir, S_Speed);
                S_Sr.flipX = false;
            }
            else if (check > 0 && S_Lock == false)
            {
                S_Dir = "Right";
                E_MoveHorizontal(S_Dir, S_Speed);
                S_Sr.flipX = true;
            }
            if (check < 0.1 && check > -0.1)
            {
                S_Lock = true;
            }

            if (S_Lock == true)
            {
                S_LockTimer -= Time.deltaTime;
                S_Dir = "Right";
                E_MoveHorizontal(S_Dir, S_Speed);
                S_Sr.flipX = true;
            }
            if (S_LockTimer <= 0)
            {
                S_LockTimer = S_LockTimerInitial;
                S_Lock = false;
            }

            /*if (hit == false)
            {
                
                S_Lock = true;
            }*/

        }
        
    }
    public override void E_Hunchback()
    {

    }
}
