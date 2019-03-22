using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    //private static Player instance;
    // player variables
    public float P_Speed;
    [SerializeField] float P_StairSpeed;
    private Vector2 P_Horizontal;
    private bool P_IsJumping;
    private Rigidbody2D P_Rb;
    private Animator P_Anim;
    public bool P_IsRight;
    private bool P_IsStopped;
    public float JumpX;
    public float JumpY;
    public float MaxJumpY;
    private bool P_IsGrounded;
    private bool P_IsAttack;
    public bool P_IsOnStair;
    [SerializeField] float P_FallSpeed;
    private bool P_StairRight;
    private Vector3 P_Start;
    private ParticleSystem P_Ps;
    private ParticleSystem P_Ps2;
    private bool P_IsPause;
    //UI
    [SerializeField] Slider HealthBar;
    [SerializeField] Text HeartText;
    [SerializeField] Text LiveText;
    [SerializeField] Text PauseText;

    // inventory
    public bool P_Knife;
    public bool P_Cross;
    public bool P_Watch;
    public bool P_Axe;
    public bool P_HolyWater;
    public bool P_UpgradedWhip;
    public bool P_UpgradedWhipTimer;
    public float P_PowerUpTimerInitial;
    private float P_PowerUpTimer;
    public float P_AttackTimerInitial;
    private float P_AttackTimer;
    private float P_HeartCounter;
    // health
    public float P_TotalHealth;
    public float P_CurrentHealth;
    private bool P_IsUsingSubWeapon;
    private bool P_HasBeenHit;
    private bool P_IsDead;
    [SerializeField] float P_DeathTimerInitial;
    private float P_DeathTimer;
    [SerializeField] float P_Lives;
    // items
    public GameObject Knife;
    public float WeaponTimerInitial;
    private float WeaponTimer;
    private void Awake()
    {
       
    }
    void Start()
    {
        P_Rb = gameObject.GetComponent<Rigidbody2D>();
        P_Anim = gameObject.GetComponent<Animator>();
        P_IsJumping = false;
        P_CurrentHealth = P_TotalHealth;
        P_IsUsingSubWeapon = false;
        P_UpgradedWhipTimer = false;
        P_PowerUpTimer = P_PowerUpTimerInitial;
        WeaponTimer = WeaponTimerInitial;
        P_Knife = false;
        P_Cross = false;
        P_Watch = false;
        P_Axe = false;
        P_HolyWater = false;
        P_UpgradedWhip = false;
        P_HasBeenHit = false;
        P_IsDead = false;
        P_IsGrounded = true;
        P_IsAttack = false;
        P_AttackTimer = P_AttackTimerInitial;
        P_IsOnStair = false;
        P_StairRight = true;
        P_HeartCounter = 0;
        P_DeathTimer = P_DeathTimerInitial;
        P_Start = gameObject.transform.position;
        HealthBar = GameObject.Find("Health").GetComponent<Slider>();
        HeartText = GameObject.Find("Heart").GetComponent<Text>();
        LiveText = GameObject.Find("Lives").GetComponent<Text>();
        PauseText = GameObject.Find("Paused").GetComponent<Text>();
        P_Ps = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
        P_Ps2 = GameObject.Find("Particle System (1)").GetComponent<ParticleSystem>();
        P_IsPause = false;
    }

    void Update()
    {
        P_CheckDeath();
        P_HandleJump();
        P_HandleAttack();
        P_HandleMainAttack();
        P_HandleHorizontalMovement();
        P_HandleUI();
        P_Pause();

    }

    #region Movement
    public void P_HandleHorizontalMovement()
    {
        if(P_IsOnStair == true)
        {
            if (P_StairRight == true)
            {
                P_Horizontal.x = Input.GetAxis("Vertical");
                P_Horizontal.x *= P_StairSpeed;
                P_Rb.velocity = new Vector2(P_Horizontal.x, P_Rb.velocity.y);
            }
            else
            {
                P_Horizontal.x = -Input.GetAxis("Vertical");
                P_Horizontal.x *= P_StairSpeed;
                P_Rb.velocity = new Vector2(P_Horizontal.x, P_Rb.velocity.y);
            }
            if (P_IsOnStair == true && P_Horizontal.x == 0)
            {
                P_Rb.velocity = new Vector2(0, 0);
                P_Rb.isKinematic = true;

            }
            else if (P_IsOnStair == true && P_Horizontal.x != 0 || (P_IsOnStair == true && P_Horizontal.x == 0 && P_IsGrounded == true))
            {
                P_Rb.isKinematic = false;

            }

            if (P_Horizontal.x < 0 && P_IsOnStair == true)
            {
                P_Anim.SetBool("OnStair", false);
                P_Anim.SetBool("OnStairIdle", false);
                P_Anim.SetBool("OnStairIdleLeft", false);
                P_Anim.SetBool("OnStairLeft", true);
            }
            else if (P_Horizontal.x > 0 && P_IsOnStair == true)
            {
                P_Anim.SetBool("OnStair", true);
                P_Anim.SetBool("OnStairIdle", false);
                P_Anim.SetBool("OnStairLeft", false);
                P_Anim.SetBool("OnStairIdleLeft", false);
            }
            if (P_Horizontal.x == 0 && P_IsOnStair == true && P_IsRight == true)
            {
                P_Anim.SetBool("OnStairIdle", true);
                P_Anim.SetBool("OnStairLeft", false);
                P_Anim.SetBool("OnStair", false);
                P_Anim.SetBool("OnStairIdleLeft", false);
            }
            else if (P_Horizontal.x == 0 && P_IsOnStair == true && P_IsRight == false)
            {
                P_Anim.SetBool("OnStairIdle", false);
                P_Anim.SetBool("OnStairLeft", false);
                P_Anim.SetBool("OnStair", false);
                P_Anim.SetBool("OnStairIdleLeft", true);
            }
            
               
            
        }
        else if (P_IsJumping == false && P_IsUsingSubWeapon == false && P_UpgradedWhipTimer == false && P_HasBeenHit == false)
        {
            P_Horizontal.x = Input.GetAxis("Horizontal");
            P_Horizontal.x *= P_Speed;
            P_Rb.velocity = new Vector2(P_Horizontal.x, P_Rb.velocity.y);
            if (P_IsDead == true)
            {
                P_Rb.velocity = new Vector2(0, P_Rb.velocity.y);
            }
            if (P_IsAttack == true)
            {
                P_Rb.velocity = new Vector2(0, 0);
            }
            

        }
        if (P_Horizontal.x < 0 && P_IsJumping == false && P_UpgradedWhipTimer == false)
        {
            P_Anim.SetBool("IsStill", false);
            P_Anim.SetBool("WalkingRight", false);
            P_Anim.SetBool("WalkingLeft", true);
            P_IsRight = false;
        }
        if (P_Horizontal.x > 0 && P_IsJumping == false && P_UpgradedWhipTimer == false)
        {
            P_Anim.SetBool("IsStill", false);
            P_Anim.SetBool("WalkingRight", true);
            P_Anim.SetBool("WalkingLeft", false);
            P_IsRight = true;
        }
        if (P_Horizontal.x == 0 && P_IsJumping == false && P_UpgradedWhipTimer == false)
        {
            P_Anim.SetBool("IsStill", true);
            P_Anim.SetBool("WalkingRight", false);
            P_Anim.SetBool("WalkingLeft", false);
        }
    }

    
    public void P_HandleJump()
    {
        if (Input.GetButtonDown("Jump") && P_IsJumping == false && P_IsRight == true && Input.GetAxis("Horizontal") > 0 && P_UpgradedWhipTimer == false && P_IsDead == false && P_IsOnStair == false)
        {
            P_Rb.velocity = new Vector2(JumpX, JumpY);
            P_IsJumping = true;
            P_Anim.SetBool("IsJumpingRight", true);
            P_Anim.SetBool("IsJumpingLeft", false);
            //P_IsGrounded = false;
        }
        else if (Input.GetButtonDown("Jump") && P_IsJumping == false && P_IsRight == false && Input.GetAxis("Horizontal") < 0 && P_UpgradedWhipTimer == false && P_IsDead == false && P_IsOnStair == false)
        {
            P_Rb.velocity = new Vector2(-JumpX, JumpY);
            P_IsJumping = true;
            P_Anim.SetBool("IsJumpingRight", false);
            P_Anim.SetBool("IsJumpingLeft", true);
            //P_IsGrounded = false;
        }
        else if (Input.GetButtonDown("Jump") && Input.GetAxis("Horizontal") == 0 && P_IsJumping == false && P_UpgradedWhipTimer == false && P_IsDead == false && P_IsOnStair == false)
        {
            P_Rb.velocity = new Vector2(0, JumpY);
            P_IsJumping = true;
            P_Anim.SetBool("IsJumpingRight", true);
            P_Anim.SetBool("IsJumpingLeft", true);
            //P_IsGrounded = false;
        }
        

    }
    #endregion
    #region Attack/Health
    public void P_HandleAttack()
    {

        if (Input.GetButtonDown("Fire2") && P_Knife == true && P_IsRight == true && P_Anim.GetBool("IsStill") == true && P_HeartCounter > 0)
        {
            WeaponTimer = WeaponTimerInitial;
            P_IsUsingSubWeapon = true;
            Instantiate(Knife, new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.2f), Quaternion.identity);
            P_Anim.SetBool("UsingSubWeaponRight", true);
            P_Anim.SetBool("UsingSubWeaponLeft", false);
            P_Knife = false;
            P_HeartCounter -= 1;
        }
        if (Input.GetButtonDown("Fire2") && P_Knife == true && P_IsRight == false  && P_Anim.GetBool("IsStill") == true && P_HeartCounter > 0)
        {
            WeaponTimer = WeaponTimerInitial;
            P_IsUsingSubWeapon = true;
            Instantiate(Knife, new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 0.2f), Quaternion.identity);
            P_Anim.SetBool("UsingSubWeaponRight", false);
            P_Anim.SetBool("UsingSubWeaponLeft", true);
            P_Knife = false;
            P_HeartCounter -= 1;
        }
        if (P_IsUsingSubWeapon == true)
        {
            WeaponTimer -= Time.deltaTime;
        }
        if (WeaponTimer <= 0)
        {
            P_IsUsingSubWeapon = false;
            P_Anim.SetBool("UsingSubWeaponRight", false);
            P_Anim.SetBool("UsingSubWeaponLeft", false);
            P_Knife = true;
        }
        if (P_UpgradedWhipTimer == true)
        {
            P_PowerUpTimer -= Time.deltaTime;
            P_Rb.velocity = new Vector2(0f,P_Rb.velocity.y);
        }
        if (P_PowerUpTimer < 0)
        {
            P_PowerUpTimer = P_PowerUpTimerInitial;
            P_UpgradedWhipTimer = false;
            P_Anim.SetBool("WhipPickUpRight", false);
            P_Anim.SetBool("WhipPickupLeft", false);
        }
    }

    public void P_HandleHealth()
    {
        if (P_CurrentHealth > 0 && P_HasBeenHit == false && P_IsRight == true)
        {
            P_Anim.SetBool("HitRight", true);
            P_Anim.SetBool("HitLeft", false);
            P_CurrentHealth--;
            P_Rb.velocity = new Vector2(-3,4);
            P_HasBeenHit = true;
        }
        if (P_CurrentHealth > 0 && P_HasBeenHit == false && P_IsRight == false)
        {
            P_Anim.SetBool("HitRight", false);
            P_Anim.SetBool("HitLeft", true);
            P_CurrentHealth--;
            P_Rb.velocity = new Vector2(3, 4);
            P_HasBeenHit = true;
            
        }
        if (P_CurrentHealth <= 0)
        {
            P_IsDead = true;
            P_Anim.SetBool("Death", true);
        }
        

    }

    public void P_CheckDeath()
    {
        if (P_IsDead == true)
        {
            P_DeathTimer -= Time.deltaTime;
        }
        if (P_DeathTimer <= 0 && P_Lives > 0)
        {
            P_Lives--;
            P_IsDead = false;
            P_CurrentHealth = P_TotalHealth;
            gameObject.transform.position = P_Start;
            Debug.Log(P_Lives);
            P_DeathTimer = P_DeathTimerInitial;
            P_Anim.SetBool("Death", false);
            P_HeartCounter = 0;
            P_UpgradedWhip = false;
            P_Knife = false;
        }
        if (P_DeathTimer <= 0 && P_Lives <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
    }

    public void P_HandleMainAttack()
    {
        if (Input.GetButtonDown("Fire1") && P_IsDead == false && P_UpgradedWhip == false && P_IsRight == true)
        {
            P_Anim.SetBool("AttackRight", true);
            P_Anim.SetBool("AttackLeft", false);
            P_Anim.SetBool("WalkingRight", false);
            P_Anim.SetBool("WalkingLeft", false);
            P_IsAttack = true;
        }
        if (Input.GetButtonDown("Fire1") && P_IsDead == false && P_UpgradedWhip == false && P_IsRight == false)
        {
            P_Anim.SetBool("AttackLeft", true);
            P_Anim.SetBool("AttackRight", false);
            P_Anim.SetBool("WalkingRight", false);
            P_Anim.SetBool("WalkingLeft", false);
            P_IsAttack = true;
        }
        if (Input.GetButtonDown("Fire1") && P_IsDead == false && P_UpgradedWhip == true && P_IsRight == true)
        {
            P_Anim.SetBool("SpecialAttackRight", true);
            P_Anim.SetBool("AttackLeft", false);
            P_Anim.SetBool("WalkingRight", false);
            P_Anim.SetBool("WalkingLeft", false);
            P_IsAttack = true;
        }
        if (Input.GetButtonDown("Fire1") && P_IsDead == false && P_UpgradedWhip == true && P_IsRight == false)
        {
            P_Anim.SetBool("SpecialAttackLeft", true);
            P_Anim.SetBool("SpecialAttackRight", false);
            P_Anim.SetBool("WalkingRight", false);
            P_Anim.SetBool("WalkingLeft", false);
            P_IsAttack = true;
        }

        if (P_IsAttack == true)
        {
            P_AttackTimer -= Time.deltaTime;
            P_Anim.SetBool("WalkingRight", false);
            P_Anim.SetBool("WalkingLeft", false);
        }
        if (P_AttackTimer <= 0)
        {
            P_IsAttack = false;
            P_AttackTimer = P_AttackTimerInitial;
            P_Anim.SetBool("AttackLeft", false);
            P_Anim.SetBool("AttackRight", false);
            P_Anim.SetBool("SpecialAttackLeft", false);
            P_Anim.SetBool("SpecialAttackRight", false);
        }
    }
    #endregion
    #region UI
    public void P_HandleUI()
    {
        HealthBar.value = P_CurrentHealth / P_TotalHealth;
        HeartText.text = "Hearts : " + P_HeartCounter.ToString();
        LiveText.text = "Lives : " + P_Lives.ToString();
        if (P_IsPause == true)
        {
            PauseText.enabled = true;
        }
        else
        {
            PauseText.enabled = false;
        }
    }
    #endregion
    #region Pause
    public void P_Pause()
    {
        if (Input.GetButtonDown("Submit") && P_IsPause == false)
        {
            P_IsPause = true;
            Time.timeScale = 0;
        }
        else if (Input.GetButtonDown("Submit") && P_IsPause == true)
        {
            P_IsPause = false;
            Time.timeScale = 1;
        }
    }
    #endregion
    #region Collisions
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            P_IsJumping = false;
            P_Anim.SetBool("IsJumpingRight", false);
            P_Anim.SetBool("IsJumpingLeft", false);
            P_HasBeenHit = false;
            P_Anim.SetBool("HitRight", false);
            P_Anim.SetBool("HitLeft", false);
            P_Anim.SetBool("Grounded", true);
            this.gameObject.layer = 0;
            P_IsGrounded = true;
            P_IsOnStair = false;
            P_Anim.SetBool("OnStair", false);
            P_Anim.SetBool("OnStairIdle", false);
            P_Anim.SetBool("OnStairLeft", false);
            P_Anim.SetBool("OnStairIdleLeft", false);
        }
        else if (col.gameObject.CompareTag("Platform"))
        {
            P_IsJumping = false;
            P_Anim.SetBool("IsJumpingRight", false);
            P_Anim.SetBool("IsJumpingLeft", false);
            P_HasBeenHit = false;
            P_Anim.SetBool("HitRight", false);
            P_Anim.SetBool("HitLeft", false);
            P_Anim.SetBool("Grounded", true);
            P_Anim.SetBool("OnStair", false);
            P_Anim.SetBool("OnStairIdle", false);
            P_Anim.SetBool("OnStairLeft", false);
            P_Anim.SetBool("OnStairIdleLeft", false);
        }
        else
        {
            P_Anim.SetBool("Grounded", false);
            P_IsGrounded = false;
        }
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Projectile"))
        {
            P_HandleHealth();
        }
        if (col.gameObject.CompareTag("knifePickup"))
        {
            P_Knife = true;
            P_Cross = false;
            P_Watch = false;
            P_HolyWater = false;
            P_Axe = false;
            Destroy(col.gameObject);

        }
        if (col.gameObject.CompareTag("WhipPickup"))
        {

            if (P_IsRight == true)
            {
                P_Anim.SetBool("WhipPickUpRight", true);
                P_Anim.SetBool("WhipPickupLeft", false);
            }
            if (P_IsRight == false)
            {
                P_Anim.SetBool("WhipPickUpRight", false);
                P_Anim.SetBool("WhipPickupLeft", true);
            }
            P_UpgradedWhip = true;
            P_UpgradedWhipTimer = true;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("SmallHeart"))
        {
            P_HeartCounter += 1;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("BigHeart"))
        {
            P_HeartCounter += 5;
            Destroy(col.gameObject);
        }
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if ((col.CompareTag("StairTrigger") && Input.GetKey(KeyCode.UpArrow)))
        {
            P_StairRight = true;
            this.gameObject.layer = 12;
        }
        else if (col.CompareTag("StairTriggerDown") && Input.GetKey(KeyCode.DownArrow))
        {
            P_StairRight = true;
            this.gameObject.layer = 12;
        }

        if (col.CompareTag("StairTriggerLeft") && Input.GetKey(KeyCode.UpArrow))
        {
            P_StairRight = false;
            this.gameObject.layer = 12;
        }
        else if (col.CompareTag("StairTriggerDownLeft") && Input.GetKey(KeyCode.DownArrow))
        {
            P_StairRight = false;
            this.gameObject.layer = 12;
        }

        if (col.CompareTag("SpecialStair") && Input.GetKey(KeyCode.UpArrow))
        {
            P_StairRight = true;
            this.gameObject.layer = 18;
        }
        else if (col.CompareTag("SpecialStairDown") && Input.GetKey(KeyCode.DownArrow))
        {
            P_StairRight = true;
            this.gameObject.layer = 18;
        }

        if (col.CompareTag("SpecialStairLeft") && Input.GetKey(KeyCode.UpArrow))
        {
            P_StairRight = false;
            this.gameObject.layer = 18;
        }
        else if (col.CompareTag("SpecialStairDownLeft") && Input.GetKey(KeyCode.DownArrow))
        {
            P_StairRight = false;
            this.gameObject.layer = 18;
        }


    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Stair"))
        {
            P_IsOnStair = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && P_IsAttack == true)
        {
            P_Ps.Play();
        }
        if (col.CompareTag("Candle") && P_IsAttack == true)
        {
            P_Ps2.Play();
        }

    }
    #endregion 
}
