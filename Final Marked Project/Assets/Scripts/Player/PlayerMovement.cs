using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovement : MonoBehaviour
{
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

    public float AttackCooldown = 0;
    public float AttackCancel = 0;
    public float Attack2Held = 0;
    public bool Attack2Charging;
    public float AttackChargeTime = 0.55f;

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
    PlayerActions controls;

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
    public GameObject SH;
    public GameObject SS;
    public GameObject SSPV;
    public GameObject UB;
    public GameObject WAMH;
    public GameObject D;
    #endregion

    private void Awake()
    {
        controls = new PlayerActions();
        PlayerAnimator.applyRootMotion = false;
        Cam = Camera.main;
    }

    private void OnEnable()
    {
        Movement = controls.PlayerCon.Movement;
        a1 = controls.PlayerCon.Attack1;
        a2 = controls.PlayerCon.Attack2;
        Movement.Enable();
        a1.Enable();
        a2.Enable();
    }

    private void OnDisable()
    {
        Movement.Disable();
        a2.Disable();
    }

    private void Update()
    {
        #region clarity, attackstrings, attack cooldowns and canceling
        taptimer -= Time.deltaTime;
        attackfullstring -= Time.deltaTime;
        LockedOn = GetComponent<EnemyLockOn>().LockOn;

        if (PlayerAnimator.GetInteger("Anim") == 0 || PlayerAnimator.GetInteger("Anim") == 1)
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

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        else
        {
            attackhit = 0;
        }
        if (AttackCancel > 0)
        {
            AttackCancel -= Time.deltaTime;
        }

        #endregion

        #region movement

        Vector2 MoveVec = Movement.ReadValue<Vector2>();

        Vector2 inputVector = controls.PlayerCon.Movement.ReadValue<Vector2>();
        //Movement(new Vector3(inputVector.x, 0.0f, inputVector.y));


        Cursor.lockState = CursorLockMode.Locked;

        Rigidbody rb = GetComponent<Rigidbody>();


        float HorizontalInput = MoveVec.x;
        float VerticalInput = MoveVec.y;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 MoveDirection = forward * VerticalInput + right * HorizontalInput;
        Vector3 DMoveDirection = forward;

        if (AttackCooldown <= 0 && Attack2Charging == false && dodge == false && kick == false)
        {

            rb.velocity = new Vector3(MoveDirection.x * speed, rb.velocity.y, MoveDirection.z * speed);

            Physics.gravity = new Vector3(0, -30F, 0);

            if (MoveDirection != new Vector3(0, 0, 0))
            {
                PlayerMesh.rotation = Quaternion.LookRotation(MoveDirection);
                PlayerAnimator.SetInteger("Anim", 1);
            }
            else { PlayerAnimator.SetInteger("Anim", 0); }
        }
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
                AttackChargeTime = 0.55f;
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
            if (ctx.interaction is HoldInteraction && AttackCooldown <= 0 && nomoves == true && Attack2Charging == false)
            {
                //charging
                Attack2Charging = true;
                if (BB.activeSelf == true)
                {
                    PlayerAnimator.SetInteger("Anim", 5);
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
                if (ctx.interaction is HoldInteraction && AttackCancel <= 0 && Attack2Held < AttackChargeTime && PlayerAnimator.GetInteger("Anim") == 5)
                {
                    Attack2Charging = false;
                    Attack2Held = 0;
                    attack2();
                }
            };
        }

        if (Attack2Held > AttackChargeTime)
        {
            attack3();
            Attack2Charging = false;
            Attack2Held = 0;
        }

        #endregion

        #region BaseballBat

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
    }

    public void attack1()
    {
        if (Attack2Charging == false)
        {
            Vector3 playerPos = this.transform.position;
            Vector3 playerDirection = this.transform.forward;
            Quaternion playerRotation = this.transform.rotation;
            Vector3 spawnPos = playerPos + playerDirection * 1;

            if (GameObject.FindWithTag("PlayerAttack") == null && AttackCooldown <= 0 && BB.activeSelf == true && dodge != true && kick != true && PlayerAnimator.GetInteger("Anim") != 4 && attackhit == 0 && taptimer <= 0)
            {
                AttackCooldown = 0.4f;
                AttackCancel = 0.3f;
                attackfullstring = 1.2f;
                AttackStringOn = true;
                PlayerAnimator.SetInteger("Anim", 4);
                attackhit = 1;
                taptimer = 0.1f;
            }

            if (BB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 4 && attackhit == 1 && taptimer <= 0)
            {
                attackhit = 2;
                AttackCooldown += 0.4f;
                AttackCancel += 0.3f;
                taptimer = 0.1f;
            }

            if (BB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 4 && attackhit == 2 && taptimer <= 0)
            {
                attackhit = 3;
                AttackCooldown += 0.4f;
                AttackCancel += 0.3f;
                taptimer = 0.1f;
            }

            if (BB.activeSelf == true && PlayerAnimator.GetInteger("Anim") == 4 && attackhit == 3 && taptimer <= 0)
            {
                attackhit = 4;
                AttackCooldown += 0.3f;
                AttackCancel += 0.3f;
                taptimer = 0.1f;
            }
        }
    }


    public void attack2()
    {
        if (GameObject.FindWithTag("PlayerAttack") == null && BB.activeSelf == true && dodge != true && kick != true)
        {
            AttackCooldown = 1f;
            AttackCancel = 0.3f;
            Attack2Held = 0;
            PlayerAnimator.SetInteger("Anim", 6);
            Attack2Charging = false;
        }
    }


    public void attack3()
    {
        Vector3 playerPos = this.transform.position;
        Vector3 playerDirection = this.transform.forward;
        Quaternion playerRotation = this.transform.rotation;
        Vector3 spawnPos = playerPos + playerDirection * 1;

        if (GameObject.FindWithTag("PlayerAttack") == null && BB.activeSelf == true && dodge != true && kick != true)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            AttackCooldown = 0.8f;
            AttackCancel = 0.3f;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public void Dodge()
    {
        if (nomoves == true && dodge == false)
        {
            dodge = true;
            PlayerAnimator.SetInteger("Anim", 3);
        }
    }

    public void Kicking()
    {
        if (nomoves == true && kick == false)
        {
            kick = true;
            PlayerAnimator.SetInteger("Anim", 2);
        }
    }


}
