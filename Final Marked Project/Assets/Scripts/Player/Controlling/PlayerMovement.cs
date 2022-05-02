using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // initialise variables

    // health
    [Header("Health")]
    private float MaxHealth = 100;
    public float Health = 100;
    public Image HealthBar;

    //power
    [Header("Power")]
    private float PowerMeterMax = 4;
    public float PowerMeter = 0;
    public Image PowerMeterBar;

    // supers
    [Header("Supers")]
    public float Super2Timer;
    public float Super3Timer;
    public float Super4Timer;
    public float SpressTimer = 0;

    // damaged
    [Header("Else")]
    public float PlayerDamagedTimer;
    bool knockback = false;

    // get animator
    public Animator PlayerAnimator;

    // attack strings
    [Header("Attack input checks")]
    public bool hit1;
    public int attackhit = 0;
    public float taptimer = 0;
    public float attackfullstring;

    // movement
    [Header("movement")]
    public float speed;
    public float JumpForce;

    // disabling moves
    [Header("Allowing Moves")]
    public bool nomoves = true;

    // checking ground
    [Header("Ground Collision")]
    public float CheckDistance;
    public Transform GroundCheck;
    public LayerMask GroundMask;

    // geting playermesh's transform
    [Header("Extras")]
    public Transform PlayerMesh;

    // if you're allowed to move
    public bool canJump;
    public bool canMove;
    
    // turning
    public float TurnSmoothTime = 0.1f;

    // lock on check
    public bool LockedOn;

    // attacking
    [Header("Attacking")]
    public float AttackTime = 0;
    public float AttackRepeatTimer = 0;
    public float Attack2Held = 0;
    public bool Attack2Charging;
    public float AttackChargeTime;

    // air combos
    [Header("Air Combos")]
    public float AirTime;
    public float UppercutTimer = 0;
    public bool PrepareUppercut = false;
    public int RepeatTimer = 0;
    public float LungeTime = 0;

    // attacking strings
    [Header("Attack Strings")]
    public float attackstringtimer = 0;
    public float attackstringdelaytimer = 0.01f;
    public bool AttackStringOn;
    GameObject clone;

    // kicking
    [Header("Kick")]
    public float KickTimer = 0.6f;
    public bool kick = false;

    // dodging
    [Header("Dodger")]
    public float DodgeTimer = 0.6f;
    public bool dodge = false;

    // player positioning
    Vector3 playerPos;
    Vector3 playerDirection;
    Quaternion playerRotation;
    Vector3 spawnPos;

    // kick hitbox
    public GameObject KickHB;

    // new input system
    private InputAction Movement;
    private InputAction a1;
    private InputAction a2;
    private InputAction s1;
    private InputAction s2;
    private InputAction s3;
    private InputAction s4;
    private InputAction InteractThrow;
    private InputAction VC;
    PlayerActions controls;

    [Header("Camera")]
    private Camera Cam;
    public Vector3 camForward;
    public Vector3 camRight;

    [Header("Items")]
    public bool CollidingWithItem = false;
    public int WeaponActiveNum = 0;

    [Header("Else Again")]
    public GameObject CameraShoulders;

    public GameObject WAMHAttack1;
    public bool SCA2 = false;

    public GameObject Explosion;

    public float bottleShatterHP = 5;

    #region items
    [Header("Items")]
    public GameObject AR;
    public GameObject BB;
    public GameObject BBO;
    public GameObject B;
    public GameObject C;
    public GameObject FF;
    public GameObject FP;
    public GameObject GC;
    public GameObject GCF;
    public GameObject KK;
    public GameObject KKF;
    public GameObject LDLS;
    public GameObject S;
    public GameObject SBB;
    public GameObject SBBO;
    public GameObject SH;
    public GameObject SS;
    public GameObject SSPV;
    public GameObject UB;
    public GameObject WAMH;
    public GameObject D;
    public GameObject M;
    public GameObject V;
    public GameObject Sc;
    public GameObject ScF;
    public GameObject F;
    public GameObject FFl;
    public GameObject SC;
    public GameObject SCF;

    public GameObject SB;

    public GameObject PINEAPPLE;
    public GameObject PIZZA;
    #endregion

    private void Awake()
    {
        // set variables
        controls = new PlayerActions();
        PlayerAnimator.applyRootMotion = false;
        Cam = Camera.main;
        WeaponActiveNum = 2;
    }

    private void OnEnable()
    {
        // set new control scheme inputs
        Movement = controls.PlayerCon.Movement;
        a1 = controls.PlayerCon.Attack1;
        a2 = controls.PlayerCon.Attack2;
        s1 = controls.PlayerCon.Super1;
        s2 = controls.PlayerCon.Super2;
        s3 = controls.PlayerCon.Super3;
        s4 = controls.PlayerCon.Super4;
        InteractThrow = controls.PlayerCon.InteractThrow;
        VC = controls.PlayerCon.ViewChange;
        Movement.Enable();
        a1.Enable();
        a2.Enable();
        s1.Enable();
        s2.Enable();
        s3.Enable();
        s4.Enable();
        InteractThrow.Enable();
        VC.Enable();
    }

    private void OnDisable()
    {
        // set things to disable when appropiate
        Movement.Disable();
        a2.Disable();
    }

    private void Update()
    {
        // get rigid body under rb
        Rigidbody rb = GetComponent<Rigidbody>();

        // if game isnt paused
        if (PauseMenu.GameIsPaused == false)
        {
            #region Health Bar & Power Meter

            // set health bar fill amount
            HealthBar.fillAmount = Health / MaxHealth;

            // set power bar fill amount
            PowerMeterBar.fillAmount = PowerMeter / PowerMeterMax;

            // make sure health and power dont go over the max
            if (PowerMeter >= PowerMeterMax)
            {
                PowerMeter = 4;
            }
            if (Health >= MaxHealth)
            {
                Health = 100;
            }

            // make sure power meter is visualised properly 
            if (PowerMeter > 3.96 && PowerMeter != 4)
            {
                PowerMeter = 4;
            }
            #endregion

            #region clarity, attackstrings, attack cooldowns and canceling

            // make timers count down
            taptimer -= Time.deltaTime;
            attackfullstring -= Time.deltaTime;

            // set lock on as a bool
            LockedOn = GetComponent<EnemyLockOn>().LockOn;

            // check if stunned or doing second super
            if ((PlayerAnimator.GetInteger("Anim") == 0 || PlayerAnimator.GetInteger("Anim") == 1) && Super2Timer <= 0.1f)
            {
                // you cannot do attacks
                nomoves = true;
            }
            else
            {
                // you can do attacks
                nomoves = false;
            }

            // airtime countdown
            if (AirTime >= 0)
            {
                AirTime -= Time.deltaTime;
            }

            // attack string count down
            if (AttackStringOn == true)
            {
                attackstringtimer -= Time.deltaTime;
                attackstringdelaytimer -= Time.deltaTime;

                // results for if timer is less then zero
                if (attackstringtimer <= 0)
                {
                    AttackStringOn = false;
                    attackstringtimer = 0;
                    attackstringdelaytimer = 0.01f;
                }
            }

            // attack time count down
            if (AttackTime > 0)
            {
                AttackTime -= Time.deltaTime;
            }
            else
            {
                attackhit = 0;
            }

            // attack repeat timer count down
            if (AttackRepeatTimer > 0)
            {
                AttackRepeatTimer -= Time.deltaTime;
            }
            else
            {
                AttackRepeatTimer = 0;
            }
            #endregion

            #region movement

            // get movement inputs
            Vector2 MoveVec = Movement.ReadValue<Vector2>();

            // set movements to an axis
            float HorizontalInput = MoveVec.x;
            float VerticalInput = MoveVec.y;

            // get directions from camera
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            // set directions y to be zero
            forward.y = 0;
            right.y = 0;

            // normalise the directions
            forward.Normalize();
            right.Normalize();

            // set second super forwards movement speed
            if (Super2Timer >= 0.1f)
            {
                forward *= 6;
            }
            else
            {
                forward *= 1;
            }

            // set movement direction
            Vector3 MoveDirection = forward * VerticalInput + right * HorizontalInput;
            Vector3 DMoveDirection = forward;

            // super 2 movement and normal movement
            if (AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PrepareUppercut == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
            {
                if (Super2Timer >= 0.1f)
                {
                    if (Vector3.Distance(this.transform.position, this.gameObject.GetComponent<EnemyLockOn>().targetingCone.transform.position) > 2)
                    {
                        rb.velocity = new Vector3(MoveDirection.x * speed * 1.2f, rb.velocity.y, MoveDirection.z * speed * 1.2f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(MoveDirection.x * speed * 0.1f, rb.velocity.y, MoveDirection.z * speed * 0.1f);
                    }

                }
                else
                {
                    rb.velocity = new Vector3(MoveDirection.x * speed, rb.velocity.y, MoveDirection.z * speed);
                }

                // setting gravity
                Physics.gravity = new Vector3(0, -30F, 0);

                // make player rotate and animate appropiately
                if (MoveDirection != new Vector3(0, 0, 0))
                {
                    PlayerMesh.rotation = Quaternion.LookRotation(MoveDirection);
                    PlayerAnimator.SetInteger("Anim", 1);
                }
                else { PlayerAnimator.SetInteger("Anim", 0); }
            }
            #endregion

            #region TakingDamage

            // make timer count down
            PlayerDamagedTimer -= Time.deltaTime;

            // set up movement in being hit
            if (PlayerDamagedTimer >= 0.0001 && knockback == true)
            {
                rb.velocity = new Vector3(-PlayerMesh.forward.x * 3, rb.velocity.y, -PlayerMesh.forward.z * 3);
            }

            // set knockback bool to false when not stunned
            if (PlayerDamagedTimer <= 0)
            {
                knockback = false;
            }
            #endregion

            #region weapon active number
            // make changes appropiate to weapon number being changed
            switch (WeaponActiveNum)
            {
                case 0:
                    #region all items inactive
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    #endregion
                    break;
                case 1:
                    #region Assault Rifle Active
                    AR.SetActive(true);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    // set time to charge until able to heavy attack 
                    AttackChargeTime = 0.25f;
                    #endregion
                    break;
                case 2:
                    #region Baseball Bat Active
                    AR.SetActive(false);
                    BB.SetActive(true);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.55f;
                    #endregion
                    break;
                case 3:
                    #region Bottle Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(true);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.55f;
                    #endregion
                    break;
                case 4:
                    #region Cleaver Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(true);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    bottleShatterHP = 5;
                    AttackChargeTime = 0.2f;
                    #endregion
                    break;
                case 5:
                    #region Foam Finger Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(true);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.4f;
                    #endregion
                    break;
                case 6:
                    #region Frying Pan Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(true);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.45f;
                    #endregion
                    break;
                case 7:
                    #region Golf Club Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(true);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.80f;
                    #endregion
                    break;
                case 8:
                    #region Kitchen Knife Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(true);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.5f;
                    #endregion
                    break;
                case 9:
                    #region Legally Distinct Laser Sword Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(true);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.1f;
                    #endregion
                    break;
                case 10:
                    #region Sledgehammer Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(true);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.3f;
                    #endregion
                    break;
                case 11:
                    #region Spiked Baseball Bat Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(true);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 1f;
                    #endregion
                    break;
                case 12:
                    #region Squeaky Hammer Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(true);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.3f;
                    #endregion
                    break;
                case 13:
                    #region Stop Sign Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(true);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.3f;
                    #endregion
                    break;
                case 14:
                    #region Stop Sign Pizza Variant Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(true);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    #endregion
                    break;
                case 15:
                    #region Uber Blade Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(true);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.1f;
                    #endregion
                    break;
                case 16:
                    #region Whack-A-Mole Hammer Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(true);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.45f;
                    #endregion
                    break;
                case 17:
                    #region Dynamite Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(true);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    #endregion
                    break;
                case 18:
                    #region Mirror Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(true);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.1f;
                    #endregion
                    break;
                case 19:
                    #region Volt Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(true);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.1f;
                    #endregion
                    break;
                case 20:
                    #region Scepter Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(true);
                    F.SetActive(false);
                    SC.SetActive(false);
                    AttackChargeTime = 0.1f;
                    #endregion
                    break;
                case 21:
                    #region Fish Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(true);
                    SC.SetActive(false);
                    AttackChargeTime = 0.2f;
                    #endregion
                    break;
                case 22:
                    #region Saw-Cleaver Active
                    AR.SetActive(false);
                    BB.SetActive(false);
                    B.SetActive(false);
                    C.SetActive(false);
                    FF.SetActive(false);
                    FP.SetActive(false);
                    GC.SetActive(false);
                    KK.SetActive(false);
                    LDLS.SetActive(false);
                    S.SetActive(false);
                    SBB.SetActive(false);
                    SH.SetActive(false);
                    SS.SetActive(false);
                    SSPV.SetActive(false);
                    UB.SetActive(false);
                    WAMH.SetActive(false);
                    D.SetActive(false);
                    M.SetActive(false);
                    V.SetActive(false);
                    Sc.SetActive(false);
                    F.SetActive(false);
                    SC.SetActive(true);
                    AttackChargeTime = 0.6f;
                    #endregion
                    break;
            }
            #endregion

            #region camera details
            // set camera forwards variables
            camForward = Cam.transform.forward;
            camRight = Cam.transform.right;

            // set camera forwards y axis to be zero
            camForward.y = 0f;
            camRight.y = 0f;

            // normalise the variables
            camForward.Normalize();
            camRight.Normalize();
            #endregion

            #region super attack 1
            s1.started += ctx =>
            {
                // if 1 is pressed heal
                if (SpressTimer <= 0.001f && PowerMeter >= 1 && AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
                {
                    PowerMeter -= 1;
                    Health += 10;
                    SpressTimer = 0.2f;
                    FindObjectOfType<SoundManager>().PlaySound("Heal");
                }
            };

            // heal time counts down
            if (SpressTimer >= 0.001)
            {
                SpressTimer -= Time.deltaTime;
            }
            #endregion

            #region super attack 2
            s2.started += ctx =>
            {
                // if 2 is pressed go in to headbutt frenzy
                if (SpressTimer <= 0.001f && PowerMeter >= 2 && AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
                {
                    Super2Timer = 5;
                    PowerMeter -= 1;
                    SpressTimer = 0.2f;
                    FindObjectOfType<SoundManager>().PlaySound("Heal");
                    if (gameObject.GetComponent<EnemyLockOn>().temp == false)
                    {
                        gameObject.GetComponent<EnemyLockOn>().temp = true;
                    }
                }
            };

            if (Super2Timer > 0.001)
            {
                // count down super timer
                Super2Timer -= Time.deltaTime;

                // force lock on to be true
                if (gameObject.GetComponent<EnemyLockOn>().temp == false)
                {
                    gameObject.GetComponent<EnemyLockOn>().temp = true;
                }


                if (LockedOn == true)
                {
                    // face enemy
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                    // headbutt enemy
                    if (!(Vector3.Distance(this.transform.position, this.gameObject.GetComponent<EnemyLockOn>().targetingCone.transform.position) > 2))
                    {
                        PlayerAnimator.SetInteger("Anim", 22);
                    }
                }
            }
            #endregion

            #region super attack 3
            s3.started += ctx =>
            {
                // if 3 pressed rain pinapples (do appropite things here)
                if (SpressTimer <= 0.001f && PowerMeter >= 3 && AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
                {
                    Super3Timer = 2;
                    PowerMeter -= 3;
                    SpressTimer = 0.2f;
                    FindObjectOfType<SoundManager>().PlaySound("Heal");
                }
            };

            if (Super3Timer >= 0.001)
            {
                // make pinapples in an area around player
                Instantiate(PINEAPPLE, new Vector3(Random.Range(this.transform.position.x - 10, this.transform.position.x + 10), this.transform.position.y + 20, Random.Range(this.transform.position.z - 10, this.transform.position.z + 10)), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
                
                // countdown super timer
                Super3Timer -= Time.deltaTime;

                // summon pinapple animation
                if (Super3Timer >= 1.45)
                {
                    PlayerAnimator.SetInteger("Anim", 23);
                }
            }
            #endregion

            #region super attack 4
            s4.started += ctx =>
            {
                // press 4 to pizza throw down
                if (SpressTimer <= 0.001f && PowerMeter >= 4 && AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
                {
                    Super4Timer = 5;
                    PowerMeter -= 4;
                    SpressTimer = 0.2f;
                    FindObjectOfType<SoundManager>().PlaySound("Heal");
                    if (gameObject.GetComponent<EnemyLockOn>().temp == false)
                    {
                        gameObject.GetComponent<EnemyLockOn>().temp = true;
                    }
                }
            };

            if (Super4Timer >= 0.001)
            {
                // count down super timer
                Super4Timer -= Time.deltaTime;

                // pizza throw animation
                if (Super4Timer >= 1.45)
                {
                    PlayerAnimator.SetInteger("Anim", 24);
                }
            }
            #endregion

            #region dodge

            if (dodge == false)
            {
                // keep dodge timer at 0.6
                DodgeTimer = 0.6f;
            }
            else
            {
                // countdown timer
                DodgeTimer -= Time.deltaTime;

                //move forwards
                rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.5f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.5f);

                //set animation in dodge time
                if (DodgeTimer <= 0.25f)
                {
                    PlayerAnimator.SetInteger("Anim", 0);
                }

                // make sure dodge bool is false when dodge is done
                if (DodgeTimer <= 0)
                {
                    dodge = false;
                }
            }

            #endregion dodge

            #region Kick

            if (kick == false)
            {
                // keep kick timer at 0.6
                KickTimer = 0.6f;
            }
            else
            {
                // countdown timer
                KickTimer -= Time.deltaTime;


                if (LockedOn == true)
                {
                    // if lockon is true kick in enemy direction
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.25f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.25f);
                }
                else
                {
                    // otherwise kick forwards
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.25f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.25f);
                }

                if (KickTimer <= 0.45 && KickTimer >= 0.35 && GameObject.Find("KickHB(Clone)") == null)
                {
                    // make kick hitbox
                    Instantiate(KickHB, spawnPos, playerRotation);
                }

                if (KickTimer <= 0.15)
                {
                    // end kick animation
                    PlayerAnimator.SetInteger("Anim", 0);
                }

                if (KickTimer <= 0)
                {
                    // end kick animation and set kick to false
                    kick = false;
                    PlayerAnimator.SetInteger("Anim", 0);
                }
            }

            #endregion

            #region throwing item
            if (CollidingWithItem == false)
            {
                InteractThrow.started += ctx =>
                {
                    if (AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001 && PlayerAnimator.GetInteger("Anim") != 67 && WeaponActiveNum != 0 && CollidingWithItem == false)
                    {
                        // play throw animation
                        PlayerAnimator.SetInteger("Anim", 67);

                        // set time the throw will last for
                        AttackTime = 0.4f;
                        AttackRepeatTimer = 0.4f;
                        attackfullstring = 0.4f;

                        // target locked on enemy if lock on is turned on!
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                };
            }
            #endregion

            #region Heavy Attacks and cancels

            a2.started += ctx =>
            {
                if (ctx.interaction is HoldInteraction && AttackTime <= 0 && nomoves == true && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
                {
                    //charging

                    // check if weapon is active and charging is false and not in an attack string while holding attack 2
                    if (BB.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        // set charging to true
                        Attack2Charging = true;

                        // play attack charge animation
                        PlayerAnimator.SetInteger("Anim", 5);

                        // face enemy
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (SS.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 8);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (SBB.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 11);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (SSPV.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 14);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (AR.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 17);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (B.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 20);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (C.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 8);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (FF.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 28);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (D.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 31);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (M.activeSelf == true && attackhit == 0 && AirTime <= 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 34);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if (V.activeSelf == true && attackhit == 0 && AirTime <= 0 && Attack2Charging == false && AttackRepeatTimer <= 0)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 37);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                    if ((WAMH.activeSelf == true || FP.activeSelf == true) && attackhit == 0 && AirTime <= 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 39);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }

                    if (S.activeSelf == true && attackhit == 0 && AirTime <= 0)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 42);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }

                    if (SH.activeSelf == true && attackhit == 0 && AirTime <= 0)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 45);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }

                    if (LDLS.activeSelf == true && attackhit == 0 && AirTime <= 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 48);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }

                    if (UB.activeSelf == true && attackhit == 0 && AirTime <= 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 51);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }

                    if (KK.activeSelf == true && attackhit == 0 && AirTime <= 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 54);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }

                    if (GC.activeSelf == true && attackhit == 0 && AirTime <= 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 57);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }

                    if (F.activeSelf == true && attackhit == 0 && AirTime <= 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 60);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }

                    if (Sc.activeSelf == true && attackhit == 0 && AirTime <= 0 && Attack2Charging == false && AttackRepeatTimer <= 0)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 63);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }

                    if (SC.activeSelf == true && attackhit == 0 && AirTime <= 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        SCA2 = false;
                        PlayerAnimator.SetInteger("Anim", 66);
                        if (LockedOn == true)
                        {
                            Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                            lockedenemy.y = 0;
                            PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                        }
                    }
                }

                // check if able to uppercut on ground
                if ((((PlayerAnimator.GetInteger("Anim") == 32 && attackhit == 2) || (PlayerAnimator.GetInteger("Anim") == 32 && attackhit == 1)) && WeaponActiveNum == 18) || (((PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 4 && AirTime >= 0.01) || (PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 3) || (PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 2) || (PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 1)) && WeaponActiveNum == 19) || (((PlayerAnimator.GetInteger("Anim") == 46 && attackhit == 3) || (PlayerAnimator.GetInteger("Anim") == 46 && attackhit == 2) || (PlayerAnimator.GetInteger("Anim") == 46 && attackhit == 1)) && WeaponActiveNum == 9) || (((PlayerAnimator.GetInteger("Anim") == 49 && attackhit == 3) || (PlayerAnimator.GetInteger("Anim") == 49 && attackhit == 2) || (PlayerAnimator.GetInteger("Anim") == 49 && attackhit == 1)) && WeaponActiveNum == 15))
                {
                    PrepareUppercut = true;
                }

                // check if able to uppercut in air
                if ((AirTime >= 0.1 && PlayerAnimator.GetInteger("Anim") != 32 && PlayerAnimator.GetInteger("Anim") != 34 && WeaponActiveNum == 18) || (AirTime >= 0.1 && PlayerAnimator.GetInteger("Anim") != 35 && PlayerAnimator.GetInteger("Anim") != 36 && WeaponActiveNum == 19) || (AirTime >= 0.1 && PlayerAnimator.GetInteger("Anim") != 46 && PlayerAnimator.GetInteger("Anim") != 48 && WeaponActiveNum == 9) || (AirTime >= 0.1 && PlayerAnimator.GetInteger("Anim") != 49 && PlayerAnimator.GetInteger("Anim") != 51 && WeaponActiveNum == 15))
                {
                    PrepareUppercut = true;
                }
            };

            // if airtime is zero, combo repeat timer is zero
            if (AirTime <= 0) { RepeatTimer = 0; }

            // count down uppercut timer
            UppercutTimer -= Time.deltaTime;

            // if prepare uppercut is true, at next oppertunity in the combo play uppercut animation and set uppercut timer, to last around the same time as the rest of the animation
            #region uppercuts
            if (PrepareUppercut == true)
            {
                //mirror
                if (attackfullstring <= 0.8 && PlayerAnimator.GetInteger("Anim") != 34 && attackhit == 1 && AirTime <= 0 && attackfullstring >= 0.01 && WeaponActiveNum == 18)
                {
                    PlayerAnimator.SetInteger("Anim", 34);
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                if (attackfullstring <= 0.6 && PlayerAnimator.GetInteger("Anim") != 34 && AirTime <= 0 && attackfullstring >= 0.01 && WeaponActiveNum == 18)
                {
                    PlayerAnimator.SetInteger("Anim", 34);
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                //mirror in-air
                if (attackfullstring <= 0.8 && PlayerAnimator.GetInteger("Anim") != 33 && PlayerAnimator.GetInteger("Anim") != 34 && attackhit == 1 && AirTime >= 0.1 && attackfullstring >= 0.01 && WeaponActiveNum == 18)
                {
                    PlayerAnimator.SetInteger("Anim", 33);
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                if (attackfullstring <= 0.6 && PlayerAnimator.GetInteger("Anim") != 33 && PlayerAnimator.GetInteger("Anim") != 34 && AirTime >= 0.1 && attackfullstring >= 0.01 && WeaponActiveNum == 18)
                {
                    PlayerAnimator.SetInteger("Anim", 33);
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                if (attackfullstring <= 0.01 && WeaponActiveNum == 18)
                {
                    PlayerAnimator.SetInteger("Anim", 33);
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                //volt
                if (attackfullstring <= 1.5f && PlayerAnimator.GetInteger("Anim") != 36 && attackhit == 1 && AirTime <= 0 && attackfullstring >= 0.01 && WeaponActiveNum == 19)
                {
                    PlayerAnimator.SetInteger("Anim", 36);
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    }
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                if (attackfullstring <= 1.2f && PlayerAnimator.GetInteger("Anim") != 36 && attackhit == 2 && AirTime <= 0 && attackfullstring >= 0.01 && WeaponActiveNum == 19)
                {
                    PlayerAnimator.SetInteger("Anim", 36);
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    }
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                if (attackfullstring <= 0.7f && PlayerAnimator.GetInteger("Anim") != 36 && attackhit == 3 && AirTime <= 0 && attackfullstring >= 0.01 && WeaponActiveNum == 19)
                {
                    PlayerAnimator.SetInteger("Anim", 36);
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    }
                        UppercutTimer = 0.4f;
                }

                //volt in-air
                if (attackfullstring <= 1.5f && PlayerAnimator.GetInteger("Anim") != 36 && attackhit == 1 && AirTime >= 0.01 && attackfullstring >= 0.01 && WeaponActiveNum == 19)
                {
                    PlayerAnimator.SetInteger("Anim", 37);
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    }
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                if (attackfullstring <= 1.2f && PlayerAnimator.GetInteger("Anim") != 36 && attackhit == 2 && AirTime >= 0.01 && attackfullstring >= 0.01 && WeaponActiveNum == 19)
                {
                    PlayerAnimator.SetInteger("Anim", 37);
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    }
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                if (attackfullstring <= 0.7f && PlayerAnimator.GetInteger("Anim") != 36 && attackhit == 3 && AirTime >= 0.01 && attackfullstring >= 0.01 && WeaponActiveNum == 19)
                {
                    PlayerAnimator.SetInteger("Anim", 37);
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    }
                    UppercutTimer = 0.4f;
                }

                if (attackfullstring <= 0.2f && PlayerAnimator.GetInteger("Anim") != 36 && attackhit == 4 && AirTime >= 0.01 && attackfullstring >= 0.01 && WeaponActiveNum == 19)
                {
                    PlayerAnimator.SetInteger("Anim", 37);
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    }
                    UppercutTimer = 0.4f;
                }

                if (attackfullstring <= 0.01 && WeaponActiveNum == 19)
                {
                    PlayerAnimator.SetInteger("Anim", 37);
                    if (UppercutTimer >= 0.41)
                    {
                        UppercutTimer = 0.4f;
                    }
                }

                //LDLS
                if (attackfullstring <= 0.58 && PlayerAnimator.GetInteger("Anim") != 48 && attackhit == 1 && AirTime <= 0 && attackfullstring >= 0.01 && WeaponActiveNum == 9)
                {
                    PlayerAnimator.SetInteger("Anim", 48);
                    UppercutTimer = 0.4f;
                }

                if (attackfullstring <= 0.33 && PlayerAnimator.GetInteger("Anim") != 48 && attackhit == 2 && AirTime <= 0 && attackfullstring >= 0.01 && WeaponActiveNum == 9)
                {
                    PlayerAnimator.SetInteger("Anim", 48);
                    UppercutTimer = 0.4f;
                }

                //LDLS in-air
                if (attackfullstring <= 0.58 && PlayerAnimator.GetInteger("Anim") != 47 && PlayerAnimator.GetInteger("Anim") != 48 && attackhit == 1 && AirTime >= 0.1 && attackfullstring >= 0.01 && WeaponActiveNum == 9)
                {
                    PlayerAnimator.SetInteger("Anim", 47);
                    UppercutTimer = 1.2f;
                }

                if (attackfullstring <= 0.33 && PlayerAnimator.GetInteger("Anim") != 47 && PlayerAnimator.GetInteger("Anim") != 48 && AirTime >= 0.1 && attackfullstring >= 0.01 && WeaponActiveNum == 9 && attackhit == 2)
                {
                    PlayerAnimator.SetInteger("Anim", 47);
                    UppercutTimer = 1.2f;
                }

                if (attackfullstring <= 0.01 && PlayerAnimator.GetInteger("Anim") != 47 && PlayerAnimator.GetInteger("Anim") != 48 && AirTime >= 0.1 && attackfullstring >= 0.01 && WeaponActiveNum == 9 && attackhit == 3)
                {
                    PlayerAnimator.SetInteger("Anim", 47);
                    UppercutTimer = 1.2f;
                }

                if (attackfullstring <= 0.01 && PlayerAnimator.GetInteger("Anim") != 47 && PlayerAnimator.GetInteger("Anim") != 48 && WeaponActiveNum == 9 && AirTime >= 0.1)
                {
                    PlayerAnimator.SetInteger("Anim", 47);
                    UppercutTimer = 1.2f;
                }

                //Uber-Blade
                if (attackfullstring <= 0.9 && PlayerAnimator.GetInteger("Anim") != 51 && attackhit == 1 && attackfullstring >= 0.01 && WeaponActiveNum == 15)
                {
                    PlayerAnimator.SetInteger("Anim", 51);
                    UppercutTimer = 0.4f;
                }

                if (attackfullstring <= 0.5 && PlayerAnimator.GetInteger("Anim") != 51 && attackhit == 2 && attackfullstring >= 0.01 && WeaponActiveNum == 15)
                {
                    PlayerAnimator.SetInteger("Anim", 51);
                    UppercutTimer = 0.4f;
                }
            }
            #endregion

            // make uppercut false if timer is up
            if (UppercutTimer <= 0)
            {
                PrepareUppercut = false;
            }


            if (Attack2Charging == true)
            {
                Attack2Held += Time.deltaTime;

                a2.canceled += ctx =>
                {
                    // if charging is true and canceled the charge, do attack 2,
                    if (ctx.interaction is HoldInteraction && (Attack2Held < AttackChargeTime) && (PlayerAnimator.GetInteger("Anim") == 5 || PlayerAnimator.GetInteger("Anim") == 8 || PlayerAnimator.GetInteger("Anim") == 11 || PlayerAnimator.GetInteger("Anim") == 14 || PlayerAnimator.GetInteger("Anim") == 17 || PlayerAnimator.GetInteger("Anim") == 20 || PlayerAnimator.GetInteger("Anim") == 28 || PlayerAnimator.GetInteger("Anim") == 31 || PlayerAnimator.GetInteger("Anim") == 34 || PlayerAnimator.GetInteger("Anim") == 37 || (PlayerAnimator.GetInteger("Anim") == 39 && Attack2Held >= 0.15) || PlayerAnimator.GetInteger("Anim") == 42 || PlayerAnimator.GetInteger("Anim") == 45 || PlayerAnimator.GetInteger("Anim") == 48 || PlayerAnimator.GetInteger("Anim") == 51 || PlayerAnimator.GetInteger("Anim") == 54 || PlayerAnimator.GetInteger("Anim") == 57 || PlayerAnimator.GetInteger("Anim") == 60 || PlayerAnimator.GetInteger("Anim") == 63))
                    {
                        Attack2Charging = false;
                        Attack2Held = 0;
                        attack2();
                    }
                    
                    // if saw cleaver do unique attack 2 (switching active object, but same animation)
                    if (Attack2Held < AttackChargeTime && SCA2 == false && PlayerAnimator.GetInteger("Anim") == 66)
                    {
                        SCA2 = true;
                    }
                };
            }

            // do sawcleaver attack 2
            if (Attack2Held > AttackChargeTime && SCA2 == true)
            {
                attack2();
                Attack2Charging = false;
                Attack2Held = 0;
                SCA2 = false;
            }

            // do attack 3
            if (Attack2Held > AttackChargeTime && (PlayerAnimator.GetInteger("Anim") == 5 || PlayerAnimator.GetInteger("Anim") == 8 || PlayerAnimator.GetInteger("Anim") == 11 || PlayerAnimator.GetInteger("Anim") == 14 || PlayerAnimator.GetInteger("Anim") == 17 || PlayerAnimator.GetInteger("Anim") == 20 || PlayerAnimator.GetInteger("Anim") == 28 || PlayerAnimator.GetInteger("Anim") == 31 || PlayerAnimator.GetInteger("Anim") == 34 || PlayerAnimator.GetInteger("Anim") == 37 || PlayerAnimator.GetInteger("Anim") == 39 || PlayerAnimator.GetInteger("Anim") == 42 || PlayerAnimator.GetInteger("Anim") == 45 || PlayerAnimator.GetInteger("Anim") == 48 || PlayerAnimator.GetInteger("Anim") == 51 || PlayerAnimator.GetInteger("Anim") == 54 || PlayerAnimator.GetInteger("Anim") == 57 || PlayerAnimator.GetInteger("Anim") == 60 || PlayerAnimator.GetInteger("Anim") == 63 || PlayerAnimator.GetInteger("Anim") == 66) && SCA2 == false)
            {
                attack3();
                Attack2Charging = false;
                Attack2Held = 0;
                SCA2 = false;
            }

            #endregion

            #region Baseball Bat
            
            // set appropiate weapon to active per attack
            if (BB.activeSelf == true && (PlayerAnimator.GetInteger("Anim") == 5 || PlayerAnimator.GetInteger("Anim") == 6))
            {
                BB.GetComponentInChildren<Renderer>().enabled = false;
                BBO.GetComponentInChildren<Renderer>().enabled = true;
            }
            else
            {
                BB.GetComponentInChildren<Renderer>().enabled = true;
                BBO.GetComponentInChildren<Renderer>().enabled = false;
            }

            // make attack go in correct direction
            if (PlayerAnimator.GetInteger("Anim") == 4)
            {
                if (attackfullstring >= 0.6)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                }
                else if (attackfullstring >= 0.06)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                }
                else if (attackfullstring >= 0.01)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * -0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * -0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * -0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * -0.75f);
                    }
                }
            }
            #endregion

            #region Spiked Baseball Bat

            if (SBB.activeSelf == true && (PlayerAnimator.GetInteger("Anim") == 11 || PlayerAnimator.GetInteger("Anim") == 12))
            {
                SBB.GetComponentInChildren<Renderer>().enabled = false;
                SBBO.GetComponentInChildren<Renderer>().enabled = true;
            }
            else
            {
                SBB.GetComponentInChildren<Renderer>().enabled = true;
                SBBO.GetComponentInChildren<Renderer>().enabled = false;
            }

            if (PlayerAnimator.GetInteger("Anim") == 10)
            {
                if (attackfullstring >= 0.2)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                }
                else if (attackfullstring >= 0.15)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0f, rb.velocity.y, PlayerMesh.forward.z * speed * 0f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0f, rb.velocity.y, PlayerMesh.forward.z * speed * 0f);
                    }
                }
                else if (attackfullstring <= 0.15)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 2f, rb.velocity.y, PlayerMesh.forward.z * speed * 2f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 2f, rb.velocity.y, PlayerMesh.forward.z * speed * 2f);
                    }
                }
            }
            #endregion

            #region Stop Sign
            if (PlayerAnimator.GetInteger("Anim") == 7 && (SS.activeSelf == true || SSPV.activeSelf == true))
            {
                if (attackfullstring >= 0.01)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.5f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.5f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.5f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.5f);
                    }
                }
            }

            if (PlayerAnimator.GetInteger("Anim") == 8)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.75f);
                }
                else
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.75f);
                }
            }

            if (PlayerAnimator.GetInteger("Anim") == 13)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    if (attackfullstring <= 2.5)
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                }
                else
                {
                    if (attackfullstring <= 2.5)
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                }
            }
            if (PlayerAnimator.GetInteger("Anim") == 15 && AttackTime <= 0.35)
            {
                //rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.6f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.6f);
            }
            if (PlayerAnimator.GetInteger("Anim") == 14)
            {
                rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.35f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.35f);
            }
            #endregion

            #region Bottle
            if (B.activeSelf == true && bottleShatterHP <= 0)
            {
                B.GetComponentInChildren<Renderer>().enabled = false;
                SB.GetComponentInChildren<Renderer>().enabled = true;
            }
            else
            {
                B.GetComponentInChildren<Renderer>().enabled = true;
                SB.GetComponentInChildren<Renderer>().enabled = false;
            }

            if (PlayerAnimator.GetInteger("Anim") == 19)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
                if (attackfullstring <= 1f)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                }
            }

            if (PlayerAnimator.GetInteger("Anim") == 21)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
                if (AttackTime <= 0.8f)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.5f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.5f);
                }
            }
            #endregion

            #region Cleaver
            if (PlayerAnimator.GetInteger("Anim") == 4)
            {
                if (attackfullstring >= 0.6)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                }
                else if (attackfullstring >= 0.06)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                }
                else if (attackfullstring >= 0.01)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * -0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * -0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * -0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * -0.75f);
                    }
                }
            }
            #endregion

            #region Foam Finger
            if (PlayerAnimator.GetInteger("Anim") == 27)
            {
                if (attackfullstring >= 0.6)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                }
                else if (attackfullstring >= 0.06)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                }
                else if (attackfullstring >= 0.01)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * -0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * -0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * -0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * -0.75f);
                    }
                }
            }
            #endregion

            #region Dynamite
            if (PlayerAnimator.GetInteger("Anim") == 29 && AttackTime <= 0.65f && AttackTime >= 0.45f)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.75f);
                }
                else
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.75f);
                }
            }
            #endregion

            #region Mirror
            if (PlayerAnimator.GetInteger("Anim") == 32 && AirTime <= 0)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
                if (attackfullstring <= 1f)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                }
            }
            #endregion

            #region Volt
            if (PlayerAnimator.GetInteger("Anim") == 35 && AirTime <= 0)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
                if (attackfullstring >= 1.35f)
                {
                    rb.velocity = new Vector3(-PlayerMesh.right.x * speed * 0.6f, rb.velocity.y, -PlayerMesh.right.z * speed * 0.6f);
                }
                if (attackfullstring <= 1.35f && attackfullstring >= 1f)
                {
                    rb.velocity = new Vector3(PlayerMesh.right.x * speed * 1.2f, rb.velocity.y, PlayerMesh.right.z * speed * 1.2f);
                }
                if (attackfullstring <= 1f && attackfullstring >= 0.5f)
                {
                    rb.velocity = new Vector3(-PlayerMesh.right.x * speed * 0.5f, rb.velocity.y, -PlayerMesh.right.z * speed * 0.5f);
                }
                if (attackfullstring <= 0.5f && attackfullstring >= 0f)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.5f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.5f);
                }
            }

            if (PlayerAnimator.GetInteger("Anim") == 36)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
            }
            if (LungeTime >= 0.001)
            {
                rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.5f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.5f);
            }
            LungeTime -= Time.deltaTime;
            #endregion

            #region Whack-A-Mole Hammer
            if (PlayerAnimator.GetInteger("Anim") == 38 && AirTime <= 0)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
            }
            #endregion

            #region SledgeHammer
            if (PlayerAnimator.GetInteger("Anim") == 40 || PlayerAnimator.GetInteger("Anim") == 43)
            {
                //if (attackfullstring >= 0.6)
                //{
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                //}
            }
            #endregion

            #region Legally Distinct Laser Sword
            if (PlayerAnimator.GetInteger("Anim") == 46 && AirTime <= 0)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
                if (attackfullstring <= 1f)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                }
            }

            if (PlayerAnimator.GetInteger("Anim") == 47)
            {
                if (AttackTime <= 0.8f)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.5f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.5f);
                }
            }
            #endregion

            #region Uber-Blade
            if (PlayerAnimator.GetInteger("Anim") == 49 && AirTime <= 0)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
                if (attackfullstring <= 1f)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                }
            }

            if (PlayerAnimator.GetInteger("Anim") == 50)
            {
                if (AttackTime <= 0.8f)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.5f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.5f);
                }
            }
            #endregion

            #region kitchen knife
            if (KK.activeSelf == true && (PlayerAnimator.GetInteger("Anim") == 53 || PlayerAnimator.GetInteger("Anim") == 54))
            {
                KK.GetComponentInChildren<Renderer>().enabled = false;
                KKF.GetComponentInChildren<Renderer>().enabled = true;
            }
            else
            {
                KK.GetComponentInChildren<Renderer>().enabled = true;
                KKF.GetComponentInChildren<Renderer>().enabled = false;
            }

            if (PlayerAnimator.GetInteger("Anim") == 54)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
                if (Attack2Charging == true)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                }
            }
            #endregion

            #region Golf Club
            if (GC.activeSelf == true && (PlayerAnimator.GetInteger("Anim") == 57))
            {
                GC.GetComponentInChildren<Renderer>().enabled = false;
                GCF.GetComponentInChildren<Renderer>().enabled = true;
            }
            else
            {
                GC.GetComponentInChildren<Renderer>().enabled = true;
                GCF.GetComponentInChildren<Renderer>().enabled = false;
            }

            if (PlayerAnimator.GetInteger("Anim") == 55 && AirTime <= 0)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
                if (attackfullstring >= 0.01f)
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                }
            }

            if (PlayerAnimator.GetInteger("Anim") == 56)
            {
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                }
                else
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                }
            }
            #endregion

            #region Fish
            if (F.activeSelf == true && (PlayerAnimator.GetInteger("Anim") == 60))
            {
                F.GetComponentInChildren<Renderer>().enabled = false;
                FFl.GetComponentInChildren<Renderer>().enabled = true;
            }
            else
            {
                F.GetComponentInChildren<Renderer>().enabled = true;
                FFl.GetComponentInChildren<Renderer>().enabled = false;
            }
            #endregion

            #region Scepter
            if (Sc.activeSelf == true && (PlayerAnimator.GetInteger("Anim") == 63))
            {
                Sc.GetComponentInChildren<Renderer>().enabled = false;
                ScF.GetComponentInChildren<Renderer>().enabled = true;
            }
            else
            {
                Sc.GetComponentInChildren<Renderer>().enabled = true;
                ScF.GetComponentInChildren<Renderer>().enabled = false;
            }

            if (PlayerAnimator.GetInteger("Anim") == 61)
            {
                if (attackfullstring >= 0.6)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.75f);
                    }
                }
                else if (attackfullstring >= 0.06)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 0.4f, rb.velocity.y, PlayerMesh.forward.z * speed * 0.4f);
                    }
                }
                else if (attackfullstring >= 0.01)
                {
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);

                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * -0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * -0.75f);
                    }
                    else
                    {
                        rb.velocity = new Vector3(PlayerMesh.forward.x * speed * -0.75f, rb.velocity.y, PlayerMesh.forward.z * speed * -0.75f);
                    }
                }
            }
            #endregion

            #region SawCleaver
            if (SC.activeSelf == true && (PlayerAnimator.GetInteger("Anim") == 66) && Attack2Charging == false)
            {
                SC.GetComponentInChildren<Renderer>().enabled = false;
                SCF.GetComponentInChildren<Renderer>().enabled = true;
            }
            else
            {
                SC.GetComponentInChildren<Renderer>().enabled = true;
                SCF.GetComponentInChildren<Renderer>().enabled = false;
            }
            #endregion
        }
    }

    public void attack1()
    {
        if (PauseMenu.GameIsPaused == false && Health >= 0.0001)
        {
            if (Attack2Charging == false && Super2Timer < 0.001 && PlayerDamagedTimer <= 0)
            {
                // get hitbox spawn position
                Vector3 playerPos = this.transform.position;
                Vector3 playerDirection = this.transform.forward;
                Quaternion playerRotation = this.transform.rotation;
                Vector3 spawnPos = playerPos + playerDirection * 1;

                #region BaseBall Bat

                // check if able to attack, if so...
                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && BB.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 4 && attackhit == 0 && taptimer <= 0)
                {
                    // set attack time
                    AttackTime = 0.4f;

                    // set time until you can repeat attack if not doing string
                    AttackRepeatTimer = 1.4f;

                    // set timer until you are unable to do string of attacks
                    attackfullstring = 1.2f;

                    // set doing the attack string to true
                    AttackStringOn = true;

                    // set animation to be appropiate to attack
                    PlayerAnimator.SetInteger("Anim", 4);

                    // set what attack hit you are on, in the string
                    attackhit = 1;

                    // set time until you can press the attack button again
                    taptimer = 0.1f;
                }

                if (BB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 4 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.4f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }

                if (BB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 4 && attackhit == 2 && taptimer <= 0)
                {
                    attackhit = 3;
                    AttackTime += 0.4f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Spiked BaseBall Bat

                if (AttackTime <= 0 && SBB.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 10 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.4f;
                    AttackRepeatTimer = 1.4f;
                    attackfullstring = 1.8f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 10);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (SBB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 10 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.4f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }

                if (SBB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 10 && attackhit == 2 && taptimer <= 0)
                {
                    attackhit = 3;
                    AttackTime += 0.8f;
                    AttackRepeatTimer += 0.8f;
                    taptimer = 0.1f;
                }

                if (SBB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 10 && attackhit == 3 && taptimer <= 0)
                {
                    attackhit = 4;
                    AttackTime += 0.4f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Stop Sign

                if (AttackTime <= 0 && SS.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 7 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.85f;
                    AttackRepeatTimer = 0.1f;
                    attackfullstring = 3.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 7);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (SS.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 7 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.8f;
                    AttackRepeatTimer += 0.8f;
                    taptimer = 0.1f;
                }

                if (SS.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 7 && attackhit == 2 && taptimer <= 0)
                {
                    attackhit = 3;
                    AttackTime += 0.6f;
                    AttackRepeatTimer += 0.6f;
                    taptimer = 0.1f;
                }
                #endregion

                #region StopSign Pizza Varient

                if (AttackTime <= 0 && SSPV.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 13 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 1.2f;
                    AttackRepeatTimer = 1.2f;
                    attackfullstring = 3.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 13);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (SSPV.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 13 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.8f;
                    AttackRepeatTimer += 0.8f;
                    taptimer = 0.1f;
                }

                if (SSPV.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 13 && attackhit == 2 && taptimer <= 0)
                {
                    attackhit = 3;
                    AttackTime += 1f;
                    AttackRepeatTimer += 1f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Assault Rifle

                if (AttackTime <= 0 && AR.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 16 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.6f;
                    AttackRepeatTimer = 0.6f;
                    attackfullstring = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 16);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (AR.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 16 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.6f;
                    AttackRepeatTimer += 0.6f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Bottle

                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && (B.activeSelf == true || C.activeSelf == true) && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 19 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.5f;
                    AttackRepeatTimer = 0.5f;
                    attackfullstring = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 19);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if ((B.activeSelf == true || C.activeSelf == true) && PlayerAnimator.GetInteger("Anim") == 19 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.2f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }

                if ((B.activeSelf == true || C.activeSelf == true) && PlayerAnimator.GetInteger("Anim") == 19 && attackhit == 2 && taptimer <= 0)
                {
                    attackhit = 3;
                    AttackTime += 0.7f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Foam Finger

                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && FF.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 27 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.2f;
                    AttackRepeatTimer = 1f;
                    attackfullstring = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 27);
                    attackhit = 1;
                    taptimer = 0.05f;
                }

                if (FF.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 27 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.7f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Dynamite
                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && D.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 29 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 1.2f;
                    AttackRepeatTimer = 1f;
                    attackfullstring = 8f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 29);
                    attackhit = 1;
                    taptimer = 0.05f;
                }
                #endregion

                #region Mirror

                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && M.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 32 && attackhit == 0 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 34)
                {
                    AttackTime = 0.4f;
                    AttackRepeatTimer = 0.5f;
                    attackfullstring = 1.2f;
                    UppercutTimer = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 32);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (M.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 32 && attackhit == 1 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 34)
                {
                    attackhit = 2;
                    AttackTime += 0.15f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }

                if (M.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 32 && attackhit == 2 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 34)
                {
                    attackhit = 3;
                    AttackTime += 0.5f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Volt

                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && V.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 35 && attackhit == 0 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 37 && RepeatTimer <= 1)
                {
                    AttackTime = 0.4f;
                    AttackRepeatTimer = 0.5f;
                    attackfullstring = 1.75f;
                    UppercutTimer = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 35);
                    attackhit = 1;
                    taptimer = 0.1f;
                    RepeatTimer += 1;
                }

                if (V.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 1 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 37)
                {
                    attackhit = 2;
                    AttackTime += 0.15f;
                    AttackRepeatTimer += 0.15f;
                    taptimer = 0.1f;
                }

                if (V.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 2 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 37)
                {
                    attackhit = 3;
                    AttackTime += 0.5f;
                    AttackRepeatTimer += 0.5f;
                    taptimer = 0.1f;
                }

                if (V.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 3 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 37)
                {
                    attackhit = 4;
                    AttackTime += 0.7f;
                    AttackRepeatTimer += 0.7f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Whack-A-Mole Hammer
                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && WAMH.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 38 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.25f;
                    AttackRepeatTimer = 0.25F;
                    attackfullstring = 0.25F;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 38);
                    attackhit = 1;
                    taptimer = 0.05f;
                    Instantiate(WAMHAttack1, transform.position, this.transform.rotation);
                }
                #endregion

                #region SledgeHammer
                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && S.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 40 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 1.35f;
                    AttackRepeatTimer = 1.35f;
                    attackfullstring = 1.75f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 40);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (S.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 40 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.4f;
                    AttackRepeatTimer += 0.7f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Squeaky Hammer
                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && SH.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 43 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 1.35f;
                    AttackRepeatTimer = 1.35f;
                    attackfullstring = 1.75f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 43);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (SH.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 43 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.4f;
                    AttackRepeatTimer += 0.7f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Legally Distinct Laser Sword

                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && LDLS.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 46 && attackhit == 0 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 48)
                {
                    AttackTime = 0.5f;
                    AttackRepeatTimer = 0.5f;
                    attackfullstring = 1.08f;
                    UppercutTimer = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 46);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (LDLS.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 46 && attackhit == 1 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 48 && PrepareUppercut == false)
                {
                    attackhit = 2;
                    AttackTime += 0.25f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }

                if (LDLS.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 46 && attackhit == 2 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 48 && PrepareUppercut == false)
                {
                    attackhit = 3;
                    AttackTime += 0.33f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Uber-Blade
                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && UB.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 49 && attackhit == 0 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 51)
                {
                    AttackTime = 0.6f;
                    AttackRepeatTimer = 0.5f;
                    attackfullstring = 1.4f;
                    UppercutTimer = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 49);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (UB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 49 && attackhit == 1 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 51 && PrepareUppercut == false)
                {
                    attackhit = 2;
                    AttackTime += 0.35f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }

                if (UB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 49 && attackhit == 2 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 51 && PrepareUppercut == false)
                {
                    attackhit = 3;
                    AttackTime += 0.45f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Kitchen Knife
                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && KK.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 52 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 1f;
                    AttackRepeatTimer = 1F;
                    attackfullstring = 1F;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 52);
                    attackhit = 1;
                    taptimer = 0.05f;
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    }
                }
                #endregion

                #region Golf Club
                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && GC.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 55 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 1f;
                    AttackRepeatTimer = 1f;
                    attackfullstring = 2.11f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 55);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (GC.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 55 && attackhit == 1 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 48 && PrepareUppercut == false)
                {
                    attackhit = 2;
                    AttackTime += 0.5f;
                    AttackRepeatTimer += 0.65f;
                    taptimer = 0.1f;
                }

                if (GC.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 55 && attackhit == 2 && taptimer <= 0 && PlayerAnimator.GetInteger("Anim") != 48 && PrepareUppercut == false)
                {
                    attackhit = 3;
                    AttackTime += 0.66f;
                    AttackRepeatTimer += 0.8f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Frying Pan
                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && FP.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 38 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.25f;
                    AttackRepeatTimer = 0.25F;
                    attackfullstring = 0.25F;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 38);
                    attackhit = 1;
                    taptimer = 0.05f;
                    Instantiate(WAMHAttack1, transform.position, this.transform.rotation);
                }
                #endregion

                #region Fish
                if (AttackTime <= 0 && F.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 58 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.6f;
                    AttackRepeatTimer = 0.6f;
                    attackfullstring = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 58);
                    attackhit = 1;
                    taptimer = 0.1f;
                    if (LockedOn == true)
                    {
                        Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                        lockedenemy.y = 0;
                        PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    }
                }

                if (F.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 58 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.6f;
                    AttackRepeatTimer += 0.6f;
                    taptimer = 0.1f;
                }
                #endregion

                #region Scepter

                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && Sc.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 61 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.2f;
                    AttackRepeatTimer = 1f;
                    attackfullstring = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 61);
                    attackhit = 1;
                    taptimer = 0.05f;
                }

                if (Sc.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 61 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.7f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }
                #endregion

                #region SawCleaver

                if (AttackTime <= 0 && SC.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 64 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 1.2f;
                    AttackRepeatTimer = 1.2f;
                    attackfullstring = 2.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 64);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (SC.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 64 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.5f;
                    AttackRepeatTimer += 0.5f;
                    taptimer = 0.1f;
                }
                #endregion

            }
        }
    }


    public void attack2()
    {
        if (PauseMenu.GameIsPaused == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
        {
            // check if able to attack, if so...
            if (BB.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                // set attack time
                AttackTime = 1f;

                // set time until you can repeat attack if not doing string
                AttackRepeatTimer = 0.3f;

                // reset attack2held (since you canceled it to activate this move)
                Attack2Held = 0;

                // set animation to be appropiate to attack
                PlayerAnimator.SetInteger("Anim", 6);

                // set attack2charging to false (since you canceled it to activate this move)
                Attack2Charging = false;
            }

            if (SBB.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1f;
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 12);
                Attack2Charging = false;
            }

            if (SS.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1f;
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 9);
                Attack2Charging = false;
            }

            if (SSPV.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 15);
                Attack2Charging = false;
            }

            if (AR.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.6f;
                AttackRepeatTimer = 1.6f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 18);
                Attack2Charging = false;
            }

            if (B.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 1.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 21);
                Attack2Charging = false;
            }

            if (C.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 21);
                Attack2Charging = false;
            }

            if (FF.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                // set rigid body to rb
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.7f;
                AttackRepeatTimer = 0.3f;
                // set velocity to zero
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (D.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                // make explosion object (since the idea of this attack is you used the tnt wrongly)
                Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                Attack2Charging = false;
                // set length of time, the player will be in the damaged animation for
                PlayerDamagedTimer = 0.75f;
                PlayerAnimator.SetInteger("Anim", 25);
                // take away player health
                Health -= 15;
            }

            if (M.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 0.6f;
                AttackRepeatTimer = 0.8f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 33);
                Attack2Charging = false;
            }

            if (V.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 0.6f;
                AttackRepeatTimer = 0.8f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 36);
                Attack2Charging = false;
                // face locked on enemy
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
            }

            if (WAMH.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 0.25f;
                AttackRepeatTimer = 0.25f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 38);
                Attack2Charging = false;
                // make attack hitbox
                Instantiate(WAMHAttack1, transform.position, this.transform.rotation);
            }

            if (S.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 41);
                Attack2Charging = false;
            }

            if (SH.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 44);
                Attack2Charging = false;
            }

            if (LDLS.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 1.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 47);
                Attack2Charging = false;
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
            }

            if (UB.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 1.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 50);
                Attack2Charging = false;
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
            }

            if (KK.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 1.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 53);
                Attack2Charging = false;
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
            }

            if (GC.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 1.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 56);
                Attack2Charging = false;
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
            }

            if (FP.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 0.25f;
                AttackRepeatTimer = 0.2f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 38);
                Attack2Charging = false;
                Instantiate(WAMHAttack1, transform.position, this.transform.rotation);
            }

            if (F.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 0.8f;
                AttackRepeatTimer = 0.8f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 59);
                Attack2Charging = false;
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
            }

            if (Sc.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 1.2f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 62);
                Attack2Charging = false;
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                }
            }

            if (SC.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1.2f;
                AttackRepeatTimer = 1.2f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 65);
                Attack2Charging = false;
            }
        }
    }


    public void attack3()
    {
        if (PauseMenu.GameIsPaused == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
        {
            // get player pos, direction, rotation and the spawnpos of a hitbox
            Vector3 playerPos = this.transform.position;
            Vector3 playerDirection = this.transform.forward;
            Quaternion playerRotation = this.transform.rotation;
            Vector3 spawnPos = playerPos + playerDirection * 1;

            if (BB.activeSelf == true && dodge != true && kick != true)
            {
                // get rigidbody
                Rigidbody rb = GetComponent<Rigidbody>();

                // set time attack takes
                AttackTime = 1.2f;

                // set attack repeat timer
                AttackRepeatTimer = 0.3f;

                // set velocity to zero
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (SBB.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (SS.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.5f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (SSPV.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.5f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (AR.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.5f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (B.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.7f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (C.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.4f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (FF.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (D.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (M.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.25f;
                AttackRepeatTimer = 0.25f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (V.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.5f;
                AttackRepeatTimer = 7f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (WAMH.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.45f;
                AttackRepeatTimer = 0.25f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (S.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (SH.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 1f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (LDLS.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.4f;
                AttackRepeatTimer = 0.4f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (UB.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.3f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (KK.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.4f;
                AttackRepeatTimer = 0.4f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (GC.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.4f;
                AttackRepeatTimer = 0.4f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (FP.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.45f;
                AttackRepeatTimer = 0.25f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (F.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.85f;
                AttackRepeatTimer = 0.85f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (Sc.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.45f;
                AttackRepeatTimer = 3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (SC.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.65f;
                AttackRepeatTimer = 0.5f;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    public void Dodge()
    {
        // if pressing dodge button, dodge is true, and play appropiate animation
        if (nomoves == true && dodge == false && PauseMenu.GameIsPaused == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
        {
            dodge = true;
            PlayerAnimator.SetInteger("Anim", 3);
        }
    }

    public void Kicking()
    {
        // if pressing kick button, kick is true, and play appropiate animation
        if (nomoves == true && kick == false && PauseMenu.GameIsPaused == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
        {
            kick = true;
            PlayerAnimator.SetInteger("Anim", 2);
        }
    }

    public void CamSwitch()
    {
        // switch side of camera for lock on if this is pressed
        if (CameraShoulders.GetComponent<LockShoulder>().Shoulder == true)
        {
            CameraShoulders.GetComponent<LockShoulder>().Shoulder = false;
        }
    }


    void OnTriggerEnter(Collider collision)
    {
        // set rigidbody up as rb
        Rigidbody rb = GetComponent<Rigidbody>();

        // if colliding with attack that damages player
        if (collision.tag == "PlayerDamage" && PlayerDamagedTimer <= 0.75f && PlayerAnimator.GetInteger("Anim") != 25 && dodge == false && Health >= 0.0001)
        {
            if (collision.name == "BouncerAHB")
            {
                // set time to be in damaged animation for
                PlayerDamagedTimer = 0.75f;

                // set attack2charging to be false
                Attack2Charging = false;

                // set attack2held to be zero
                Attack2Held = 0;

                // set animation to stun animation
                PlayerAnimator.SetInteger("Anim", 25);

                // take away health
                Health -= 20;

                // allow knockback (e.g. move backwards after being hit)
                knockback = true;
            }
            if (collision.name == "BouncerV2AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 30;
                knockback = true;
            }
            if (collision.name == "BouncerV3AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 40;
                knockback = true;
            }

            if (collision.name == "TeleporterAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 20;
                knockback = true;
            }
            if (collision.name == "TeleporterV2AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 20;
                knockback = true;
            }
            if (collision.name == "TeleporterV3AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 20;
                knockback = true;
            }

            if (collision.name == "ChaserAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 35;
                knockback = true;
            }
            if (collision.name == "ChaserV2AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 45;
                knockback = true;
            }
            if (collision.name == "ChaserV3AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 55;
                knockback = true;
            }

            if (collision.name == "DefenderAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 20;
                knockback = true;
            }
            if (collision.name == "DefenderV2AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 30;
                knockback = true;
            }
            if (collision.name == "DefenderV3AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 40;
                knockback = true;
            }

            if (collision.name == "DropPodAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 35;
                knockback = true;
            }
            if (collision.name == "DropPodV2AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 45;
                knockback = true;
            }
            if (collision.name == "DropPodV3AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 55;
                knockback = true;
            }

            if (collision.name == "BiggerAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 35;
                knockback = true;
            }
            if (collision.name == "BiggerV2AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 45;
                knockback = true;
            }
            if (collision.name == "BiggerV3AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 55;
                knockback = true;
            }

            if (collision.name == "HeavyAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 15;
                knockback = true;
            }
            if (collision.name == "HeavyV2AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 25;
                knockback = true;
            }
            if (collision.name == "HeavyV3AHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 35;
                knockback = true;
            }

            if (collision.name == "FireBallAHB" || collision.name == "FireBallAHB(Clone)")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 15;
                knockback = false;
            }
        }
    }
}
