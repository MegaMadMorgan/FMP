using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float MaxHealth = 100;
    public float Health = 100;
    public Image HealthBar;

    private float PowerMeterMax = 4;
    public float PowerMeter = 0;
    public Image PowerMeterBar;

    public float Super2Timer;
    public float Super3Timer;
    public float Super4Timer;
    public float SpressTimer = 0;

    public float PlayerDamagedTimer;

    public Animator PlayerAnimator;

    public bool hit1;
    public int attackhit = 0;
    public float taptimer = 0;
    public float attackfullstring;

    public float speed;
    public float JumpForce;

    public bool nomoves = true;

    public float CheckDistance;
    public Transform GroundCheck;
    public LayerMask GroundMask;

    public Transform PlayerMesh;

    public bool canJump;
    public bool canMove;

    public float TurnSmoothTime = 0.1f;

    public bool LockedOn;

    public float AttackTime = 0;
    public float AttackRepeatTimer = 0;
    public float Attack2Held = 0;
    public bool Attack2Charging;
    public float AttackChargeTime;

    public float AirTime;
    public float UppercutTimer = 0;
    public bool PrepareUppercut = false;
    public int RepeatTimer = 0;
    public float LungeTime = 0;

    public float attackstringtimer = 0;
    public float attackstringdelaytimer = 0.01f;
    public bool AttackStringOn;
    GameObject clone;

    public float KickTimer = 0.6f;
    public bool kick = false;

    public float DodgeTimer = 0.6f;
    public bool dodge = false;

    Vector3 playerPos;
    Vector3 playerDirection;
    Quaternion playerRotation;
    Vector3 spawnPos;

    public GameObject KickHB;

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

    [Header("Step Climb")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.6f;
    //[SerializeField] float stepSmooth = 0.2f;


    private Camera Cam;
    public Vector3 camForward;
    public Vector3 camRight;

    public bool CollidingWithItem = false;
    public int WeaponActiveNum = 0;

    public GameObject CameraShoulders;

    public GameObject WAMHAttack1;
    public bool SCA2 = false;

    #region items
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

    public GameObject Explosion;

    public float bottleShatterHP = 5;

    private void Awake()
    {
        controls = new PlayerActions();
        PlayerAnimator.applyRootMotion = false;
        Cam = Camera.main;
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);

        // throwaway line for the beta
        WeaponActiveNum = 2;
    }

    private void OnEnable()
    {
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
        Movement.Disable();

        a2.Disable();
    }

    private void Update()
    {



        Rigidbody rb = GetComponent<Rigidbody>();
        if (PauseMenu.GameIsPaused == false)
        {
            #region Health Bar & Power Meter
            HealthBar.fillAmount = Health / MaxHealth;
            PowerMeterBar.fillAmount = PowerMeter / PowerMeterMax;
            if (PowerMeter >= PowerMeterMax)
            {
                PowerMeter = 4;
            }
            if (Health >= MaxHealth)
            {
                Health = 100;
            }

            if (PowerMeter > 3.96 && PowerMeter != 4)
            {
                PowerMeter = 4;
            }
            #endregion

            #region clarity, attackstrings, attack cooldowns and canceling
            taptimer -= Time.deltaTime;
            attackfullstring -= Time.deltaTime;
            LockedOn = GetComponent<EnemyLockOn>().LockOn;

            if ((PlayerAnimator.GetInteger("Anim") == 0 || PlayerAnimator.GetInteger("Anim") == 1) && Super2Timer <= 0.1f)
            {
                nomoves = true;
            }
            else
            {
                nomoves = false;
            }

            if (AirTime >= 0)
            {
                AirTime -= Time.deltaTime;
            }

            if (AttackStringOn == true)
            {
                attackstringtimer -= Time.deltaTime;
                attackstringdelaytimer -= Time.deltaTime;

                if (attackstringtimer <= 0)
                {
                    AttackStringOn = false;
                    attackstringtimer = 0;
                    attackstringdelaytimer = 0.01f;

                }
            }

            if (AttackTime > 0)
            {
                AttackTime -= Time.deltaTime;
            }
            else
            {
                attackhit = 0;
            }
            if (AttackRepeatTimer > 0)
            {
                AttackRepeatTimer -= Time.deltaTime;
            }
            #endregion

            #region movement

            Vector2 MoveVec = Movement.ReadValue<Vector2>();

            Vector2 inputVector = controls.PlayerCon.Movement.ReadValue<Vector2>();
            //Movement(new Vector3(inputVector.x, 0.0f, inputVector.y));

            


            float HorizontalInput = MoveVec.x;
            float VerticalInput = MoveVec.y;

            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            if (Super2Timer >= 0.1f)
            {
                forward *= 6;
            }
            else
            {
                forward *= 1;
            }

            Vector3 MoveDirection = forward * VerticalInput + right * HorizontalInput;
            Vector3 DMoveDirection = forward;

            if (AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PrepareUppercut == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
            {

                //rb.velocity = new Vector3(MoveDirection.x * speed, rb.velocity.y, MoveDirection.z * speed);

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

                Physics.gravity = new Vector3(0, -30F, 0);



                if (MoveDirection != new Vector3(0, 0, 0))
                {
                    PlayerMesh.rotation = Quaternion.LookRotation(MoveDirection);
                    PlayerAnimator.SetInteger("Anim", 1);
                }
                else { PlayerAnimator.SetInteger("Anim", 0); }
            }

            //stepClimb();
            #endregion

            #region TakingDamage

            PlayerDamagedTimer -= Time.deltaTime;

            if (PlayerDamagedTimer >= 0.0001)
            {
                rb.velocity = new Vector3(-PlayerMesh.forward.x * 3, rb.velocity.y, -PlayerMesh.forward.z * 3);
            }

            #endregion

            #region weapon active number
            switch (WeaponActiveNum)
            {
                case 0: // done
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
                case 1: // done
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
                    AttackChargeTime = 0.25f;
                    #endregion
                    break;
                case 2: // done
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
                case 3: // done
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
                case 4: // done-ish
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
                case 5: // done
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
                case 6: // done but not unique
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
                case 7: // done
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
                case 8: // done
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
                case 9: // done
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
                case 10: // done
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
                case 11: // done
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
                case 12: // done
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
                case 13: // done
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
                case 14: // done
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
                case 15: // done
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
                case 16: // done
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
                case 17: // knock back isnt there!
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
                case 18: // done
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
                case 19: // done
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
                case 20: // done
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
                case 21: //done
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
                case 22: //done
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
            camForward = Cam.transform.forward;
            camRight = Cam.transform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();
            #endregion

            #region super attack 1
            s1.started += ctx =>
            {
                if (SpressTimer <= 0.001f && PowerMeter >= 1 && AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
                {
                    PowerMeter -= 1;
                    Health += 10;
                    SpressTimer = 0.2f;
                }
            };

            if (SpressTimer >= 0.001)
            {
                SpressTimer -= Time.deltaTime;
            }
            #endregion

            #region super attack 2
            s2.started += ctx =>
            {
                if (SpressTimer <= 0.001f && PowerMeter >= 2 && AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
                {
                    Super2Timer = 5;
                    PowerMeter -= 1;
                    SpressTimer = 0.2f;
                    if (gameObject.GetComponent<EnemyLockOn>().temp == false)
                    {
                        gameObject.GetComponent<EnemyLockOn>().temp = true;
                    }
                }
            };

            if (Super2Timer > 0.001)
            {
                Super2Timer -= Time.deltaTime;
                if (gameObject.GetComponent<EnemyLockOn>().temp == false)
                {
                    gameObject.GetComponent<EnemyLockOn>().temp = true;
                }
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    if (Vector3.Distance(this.transform.position, this.gameObject.GetComponent<EnemyLockOn>().targetingCone.transform.position) > 2)
                    {
                        //rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 4f, rb.velocity.y, PlayerMesh.forward.z * speed * 4f);
                        //PlayerAnimator.SetInteger("Anim", 1);
                    }
                    else
                    {
                        PlayerAnimator.SetInteger("Anim", 22);
                    }
                }
            } // set lock on to be active
            #endregion

            #region super attack 3
            s3.started += ctx =>
            {
                if (SpressTimer <= 0.001f && PowerMeter >= 3 && AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
                {
                    Super3Timer = 2;
                    PowerMeter -= 3;
                    SpressTimer = 0.2f;
                }
            };

            if (Super3Timer >= 0.001)
            {
                Instantiate(PINEAPPLE, new Vector3(Random.Range(this.transform.position.x - 10, this.transform.position.x + 10), this.transform.position.y + 20, Random.Range(this.transform.position.z - 10, this.transform.position.z + 10)), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
                Super3Timer -= Time.deltaTime;
                if (Super3Timer >= 1.45)
                {
                    PlayerAnimator.SetInteger("Anim", 23);
                }
            }
            #endregion

            #region super attack 4
            s4.started += ctx =>
            {
                if (SpressTimer <= 0.001f && PowerMeter >= 4 && AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
                {
                    Super4Timer = 5;
                    PowerMeter -= 4;
                    SpressTimer = 0.2f;
                    if (gameObject.GetComponent<EnemyLockOn>().temp == false)
                    {
                        gameObject.GetComponent<EnemyLockOn>().temp = true;
                    }
                }
            };

            if (Super4Timer >= 0.001)
            {
                Super4Timer -= Time.deltaTime;
                if (Super4Timer >= 1.45)
                {
                    PlayerAnimator.SetInteger("Anim", 24);
                }
            }
            #endregion

            #region dodge

            if (dodge == false)
            {
                DodgeTimer = 0.6f;
            }
            else
            {
                DodgeTimer -= Time.deltaTime;
                rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.5f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.5f);
                if (DodgeTimer <= 0.25f)
                {
                    PlayerAnimator.SetInteger("Anim", 0);
                }
                if (DodgeTimer <= 0)
                {
                    dodge = false;
                }
            }

            #endregion dodge

            #region Kick

            if (kick == false)
            {
                KickTimer = 0.6f;
            }
            else
            {
                KickTimer -= Time.deltaTime;
                if (LockedOn == true)
                {
                    Vector3 lockedenemy = GetComponent<EnemyLockOn>().targetingConePivot.transform.position - this.transform.position;
                    lockedenemy.y = 0;
                    PlayerMesh.rotation = Quaternion.LookRotation(lockedenemy);
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.25f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.25f);
                }
                else
                {
                    rb.velocity = new Vector3(PlayerMesh.forward.x * speed * 1.25f, rb.velocity.y, PlayerMesh.forward.z * speed * 1.25f);
                }

                if (KickTimer <= 0.45 && KickTimer >= 0.35 && GameObject.Find("KickHB(Clone)") == null)
                {
                    Instantiate(KickHB, spawnPos, playerRotation);
                }

                if (KickTimer <= 0.15)
                {
                    PlayerAnimator.SetInteger("Anim", 0);
                }

                if (KickTimer <= 0)
                {
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
                        PlayerAnimator.SetInteger("Anim", 67);
                        AttackTime = 0.4f;
                        AttackRepeatTimer = 0.4f;
                        attackfullstring = 0.4f;
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
                    if (BB.activeSelf == true && attackhit == 0 && Attack2Charging == false)
                    {
                        Attack2Charging = true;
                        PlayerAnimator.SetInteger("Anim", 5);
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

                if ((((PlayerAnimator.GetInteger("Anim") == 32 && attackhit == 2) || (PlayerAnimator.GetInteger("Anim") == 32 && attackhit == 1)) && WeaponActiveNum == 18) || (((PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 4 && AirTime >= 0.01) || (PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 3) || (PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 2) || (PlayerAnimator.GetInteger("Anim") == 35 && attackhit == 1)) && WeaponActiveNum == 19) || (((PlayerAnimator.GetInteger("Anim") == 46 && attackhit == 3) || (PlayerAnimator.GetInteger("Anim") == 46 && attackhit == 2) || (PlayerAnimator.GetInteger("Anim") == 46 && attackhit == 1)) && WeaponActiveNum == 9) || (((PlayerAnimator.GetInteger("Anim") == 49 && attackhit == 3) || (PlayerAnimator.GetInteger("Anim") == 49 && attackhit == 2) || (PlayerAnimator.GetInteger("Anim") == 49 && attackhit == 1)) && WeaponActiveNum == 15))
                {
                    PrepareUppercut = true;
                }

                if ((AirTime >= 0.1 && PlayerAnimator.GetInteger("Anim") != 32 && PlayerAnimator.GetInteger("Anim") != 34 && WeaponActiveNum == 18) || (AirTime >= 0.1 && PlayerAnimator.GetInteger("Anim") != 35 && PlayerAnimator.GetInteger("Anim") != 36 && WeaponActiveNum == 19) || (AirTime >= 0.1 && PlayerAnimator.GetInteger("Anim") != 46 && PlayerAnimator.GetInteger("Anim") != 48 && WeaponActiveNum == 9) || (AirTime >= 0.1 && PlayerAnimator.GetInteger("Anim") != 49 && PlayerAnimator.GetInteger("Anim") != 51 && WeaponActiveNum == 15))
                {
                    PrepareUppercut = true;
                }
            };

            if (AirTime <= 0) { RepeatTimer = 0; }
            UppercutTimer -= Time.deltaTime;

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

            if (UppercutTimer <= 0)
            {
                PrepareUppercut = false;
            }

            if (Attack2Charging == true)
            {
                Attack2Held += Time.deltaTime;

                a2.canceled += ctx =>
                {
                    if (ctx.interaction is HoldInteraction && (Attack2Held < AttackChargeTime) && (PlayerAnimator.GetInteger("Anim") == 5 || PlayerAnimator.GetInteger("Anim") == 8 || PlayerAnimator.GetInteger("Anim") == 11 || PlayerAnimator.GetInteger("Anim") == 14 || PlayerAnimator.GetInteger("Anim") == 17 || PlayerAnimator.GetInteger("Anim") == 20 || PlayerAnimator.GetInteger("Anim") == 28 || PlayerAnimator.GetInteger("Anim") == 31 || PlayerAnimator.GetInteger("Anim") == 34 || PlayerAnimator.GetInteger("Anim") == 37 || (PlayerAnimator.GetInteger("Anim") == 39 && Attack2Held >= 0.15) || PlayerAnimator.GetInteger("Anim") == 42 || PlayerAnimator.GetInteger("Anim") == 45 || PlayerAnimator.GetInteger("Anim") == 48 || PlayerAnimator.GetInteger("Anim") == 51 || PlayerAnimator.GetInteger("Anim") == 54 || PlayerAnimator.GetInteger("Anim") == 57 || PlayerAnimator.GetInteger("Anim") == 60 || PlayerAnimator.GetInteger("Anim") == 63))
                    {
                        Attack2Charging = false;
                        Attack2Held = 0;
                        attack2();
                    }

                    if (Attack2Held < AttackChargeTime && SCA2 == false && PlayerAnimator.GetInteger("Anim") == 66)
                    {
                        SCA2 = true;
                    }
                };
            }

            if (Attack2Held > AttackChargeTime && SCA2 == true)
            {
                attack2();
                Attack2Charging = false;
                Attack2Held = 0;
                SCA2 = false;
            }

            if (Attack2Held > AttackChargeTime && (PlayerAnimator.GetInteger("Anim") == 5 || PlayerAnimator.GetInteger("Anim") == 8 || PlayerAnimator.GetInteger("Anim") == 11 || PlayerAnimator.GetInteger("Anim") == 14 || PlayerAnimator.GetInteger("Anim") == 17 || PlayerAnimator.GetInteger("Anim") == 20 || PlayerAnimator.GetInteger("Anim") == 28 || PlayerAnimator.GetInteger("Anim") == 31 || PlayerAnimator.GetInteger("Anim") == 34 || PlayerAnimator.GetInteger("Anim") == 37 || PlayerAnimator.GetInteger("Anim") == 39 || PlayerAnimator.GetInteger("Anim") == 42 || PlayerAnimator.GetInteger("Anim") == 45 || PlayerAnimator.GetInteger("Anim") == 48 || PlayerAnimator.GetInteger("Anim") == 51 || PlayerAnimator.GetInteger("Anim") == 54 || PlayerAnimator.GetInteger("Anim") == 57 || PlayerAnimator.GetInteger("Anim") == 60 || PlayerAnimator.GetInteger("Anim") == 63 || PlayerAnimator.GetInteger("Anim") == 66) && SCA2 == false)
            {
                attack3();
                Attack2Charging = false;
                Attack2Held = 0;
                SCA2 = false;
            }

            #endregion

            #region Baseball Bat

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

            #region Assault Rifle

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

    //void stepClimb()
    //{
    //    //forward
    //    RaycastHit hitLower;
    //    if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
    //    {
    //        RaycastHit hitUpper;
    //        if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
    //        {
    //            Rigidbody rb = GetComponent<Rigidbody>();
    //            rb.position -= new Vector3(0f, -stepSmooth, 0f);
    //        }
    //    }

    //    //forward left
    //    RaycastHit hitLower45;
    //    if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
    //    {
    //        RaycastHit hitUpper45;
    //        if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
    //        {
    //            Rigidbody rb = GetComponent<Rigidbody>();
    //            rb.position -= new Vector3(0f, -stepSmooth, 0f);
    //        }
    //    }

    //    //forward right
    //    RaycastHit hitLowerSub45;
    //    if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerSub45, 0.1f))
    //    {
    //        RaycastHit hitUpperSub45;
    //        if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperSub45, 0.2f))
    //        {
    //            Rigidbody rb = GetComponent<Rigidbody>();
    //            rb.position -= new Vector3(0f, -stepSmooth, 0f);
    //        }
    //    }
    //}

    public void attack1()
    {
        if (PauseMenu.GameIsPaused == false && Health >= 0.0001)
        {
            if (Attack2Charging == false && Super2Timer < 0.001 && PlayerDamagedTimer <= 0)
            {
                Vector3 playerPos = this.transform.position;
                Vector3 playerDirection = this.transform.forward;
                Quaternion playerRotation = this.transform.rotation;
                Vector3 spawnPos = playerPos + playerDirection * 1;

                #region BaseBall Bat

                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && BB.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 4 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.4f;
                    AttackRepeatTimer = 1.4f;
                    attackfullstring = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 4);
                    attackhit = 1;
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

                // removed cause it didnt feel satisfying
                //if (BB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 4 && attackhit == 3 && taptimer <= 0)
                //{
                //    attackhit = 4;
                //    AttackTime += 0.3f;
                //    AttackRepeatTimer += 0.3f;
                //    taptimer = 0.1f;
                //}
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
            if (BB.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                AttackTime = 1f;
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 6);
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
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
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
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 0.7f;
                AttackRepeatTimer = 0.3f;
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (D.activeSelf == true && dodge != true && kick != true && AttackTime <= 0)
            {
                Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                Attack2Charging = false;
                PlayerDamagedTimer = 0.75f;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 20;
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
            Vector3 playerPos = this.transform.position;
            Vector3 playerDirection = this.transform.forward;
            Quaternion playerRotation = this.transform.rotation;
            Vector3 spawnPos = playerPos + playerDirection * 1;

            if (BB.activeSelf == true && dodge != true && kick != true)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                AttackTime = 1.2f;
                AttackRepeatTimer = 0.3f;
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
    } // lol

    public void Dodge()
    {
        if (nomoves == true && dodge == false && PauseMenu.GameIsPaused == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
        {
            dodge = true;
            PlayerAnimator.SetInteger("Anim", 3);
        }
    }

    public void Kicking()
    {
        if (nomoves == true && kick == false && PauseMenu.GameIsPaused == false && PlayerDamagedTimer <= 0 && Health >= 0.0001)
        {
            kick = true;
            PlayerAnimator.SetInteger("Anim", 2);
        }
    }

    public void CamSwitch()
    {
        if (CameraShoulders.GetComponent<LockShoulder>().Shoulder == true)
        {
            CameraShoulders.GetComponent<LockShoulder>().Shoulder = false;
        }

        //if (CameraShoulders.GetComponent<LockShoulder>().Shoulder == false)
        //{
        //    CameraShoulders.GetComponent<LockShoulder>().Shoulder = true;
        //}
    }


    void OnTriggerEnter(Collider collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (collision.tag == "PlayerDamage" && PlayerDamagedTimer <= 0.75f && PlayerAnimator.GetInteger("Anim") != 25 && dodge == false && Health >= 0.0001)
        {
            if (collision.name == "BouncerAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 20;
            }

            if (collision.name == "ChaserAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 35;
            }

            if (collision.name == "DefenderAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 20;
            }

            if (collision.name == "DropPodAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 35;
            }

            if (collision.name == "HeavyAHB")
            {
                PlayerDamagedTimer = 0.75f;
                Attack2Charging = false;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 25);
                Health -= 15;
            }
        }
    }
}
