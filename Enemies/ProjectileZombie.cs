using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileZombie : Enemy
{
    [SerializeField] float PZ_ThrowTimerInitial;
    [SerializeField] GameObject PZ_ProjectileObject;
    private Animator PZ_Anim;
    private Projectile PZ_Projectile;
    private float PZ_ThrowTimer;

    void Start()
    {
        E_Initialise();
        PZ_ThrowTimer = PZ_ThrowTimerInitial;
        PZ_Anim = gameObject.GetComponent<Animator>();
        PZ_Anim.SetBool("IsFiring", false);
    }

    // Update is called once per frame
    void Update()
    {
        PZ_ThrowProjectileAnim();
    }

    void PZ_ThrowProjectileAnim()
    {
        PZ_ThrowTimer -= Time.deltaTime;
        if (PZ_ThrowTimer <= 0)
        {
            PZ_Anim.SetBool("IsFiring", true);
        }
    }

    void PZ_ThrowProjectile()
    {
        PZ_ThrowTimer = PZ_ThrowTimerInitial;
        Instantiate(PZ_ProjectileObject, new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        PZ_Projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        PZ_Projectile.Initialise("Left");
        PZ_Projectile.PJ_Fire();
        PZ_Anim.SetBool("IsFiring", false);
    }

    public override void E_Hunchback()
    {
        
    }
}
