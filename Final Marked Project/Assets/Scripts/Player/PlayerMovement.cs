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

    //public Transform Cam;

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

    #region items
    public GameObject AR;
    public GameObject BB;
    public GameObject BBO;
    public GameObject B;
    public GameObject C;
    public GameObject FF;
    public GameObject FP;
    public GameObject GC;
    public GameObject KK;
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
    public GameObject SB;

    public GameObject PINEAPPLE;
    public GameObject PIZZA;
    #endregion

    public float bottleShatterHP = 5;

    private void Awake()
    {
        controls = new PlayerActions();
        PlayerAnimator.applyRootMotion = false;
        Cam = Camera.main;
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
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
        Movement.Enable();
        a1.Enable();
        a2.Enable();
        s1.Enable();
        s2.Enable();
        s3.Enable();
        s4.Enable();
    }

    private void OnDisable()
    {
        Movement.Disable();
        
        a2.Disable();
    }

    private void Update()
    {
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

            Rigidbody rb = GetComponent<Rigidbody>();


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

            if (AttackTime <= 0 && Attack2Charging == false && dodge == false && kick == false)
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

            #region weapon active number

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
                if (SpressTimer <= 0.001f && PowerMeter >= 1)
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
                if (SpressTimer <= 0.001f && PowerMeter >= 2)
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
                if (SpressTimer <= 0.001f && PowerMeter >= 3)
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
                if (SpressTimer <= 0.001f && PowerMeter >= 4)
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

            #region Heavy Attack

            a2.started += ctx =>
            {
                if (ctx.interaction is HoldInteraction && AttackTime <= 0 && nomoves == true && Attack2Charging == false && dodge == false && kick == false)
                {
                //charging
                if (BB.activeSelf == true && attackhit == 0)
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
                    if (SS.activeSelf == true && attackhit == 0)
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
                    if (SBB.activeSelf == true && attackhit == 0)
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
                    if (SSPV.activeSelf == true && attackhit == 0)
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
                    if (AR.activeSelf == true && attackhit == 0)
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
                    if (B.activeSelf == true && attackhit == 0)
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
                }
            };

            if (Attack2Charging == true)
            {
                Attack2Held += Time.deltaTime;

                a2.canceled += ctx =>
                {
                    if (ctx.interaction is HoldInteraction && (Attack2Held < AttackChargeTime) && (PlayerAnimator.GetInteger("Anim") == 5 || PlayerAnimator.GetInteger("Anim") == 8 || PlayerAnimator.GetInteger("Anim") == 11 || PlayerAnimator.GetInteger("Anim") == 14 || PlayerAnimator.GetInteger("Anim") == 17 || PlayerAnimator.GetInteger("Anim") == 20))
                    {
                        Attack2Charging = false;
                        Attack2Held = 0;
                        attack2();
                    }
                };
            }

            if (Attack2Held > AttackChargeTime && (PlayerAnimator.GetInteger("Anim") == 5 || PlayerAnimator.GetInteger("Anim") == 8 || PlayerAnimator.GetInteger("Anim") == 11 || PlayerAnimator.GetInteger("Anim") == 14 || PlayerAnimator.GetInteger("Anim") == 17 || PlayerAnimator.GetInteger("Anim") == 20))
            {
                attack3();
                Attack2Charging = false;
                Attack2Held = 0;
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
        if (PauseMenu.GameIsPaused == false)
        {
            if (Attack2Charging == false && Super2Timer < 0.001)
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

                #region BaseBall Bat

                if (AttackTime <= 0 && AttackRepeatTimer <= 0 && B.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 19 && attackhit == 0 && taptimer <= 0)
                {
                    AttackTime = 0.5f;
                    AttackRepeatTimer = 0.5f;
                    attackfullstring = 1.2f;
                    AttackStringOn = true;
                    PlayerAnimator.SetInteger("Anim", 19);
                    attackhit = 1;
                    taptimer = 0.1f;
                }

                if (B.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 19 && attackhit == 1 && taptimer <= 0)
                {
                    attackhit = 2;
                    AttackTime += 0.2f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }

                if (B.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 19 && attackhit == 2 && taptimer <= 0)
                {
                    attackhit = 3;
                    AttackTime += 0.7f;
                    AttackRepeatTimer += 0.4f;
                    taptimer = 0.1f;
                }
                #endregion
            }
        }
    }


    public void attack2()
    {
        if (PauseMenu.GameIsPaused == false)
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
                AttackRepeatTimer = 0.3f;
                Attack2Held = 0;
                PlayerAnimator.SetInteger("Anim", 21);
                Attack2Charging = false;
                //Rigidbody rb = GetComponent<Rigidbody>();
                //rb.AddForce(0, 8.5f, 0, ForceMode.Impulse);
            }
        }
    }


    public void attack3()
    {
        if (PauseMenu.GameIsPaused == false)
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
        }
    }

    public void Dodge()
    {
        if (nomoves == true && dodge == false && PauseMenu.GameIsPaused == false)
        {
            dodge = true;
            PlayerAnimator.SetInteger("Anim", 3);
        }
    }

    public void Kicking()
    {
        if (nomoves == true && kick == false && PauseMenu.GameIsPaused == false)
        {
            kick = true;
            PlayerAnimator.SetInteger("Anim", 2);
        }
    }


}
