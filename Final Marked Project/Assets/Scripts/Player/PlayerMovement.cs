using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform Cam;

    public Animator PlayerAnimator;

    public float speed;
    public float JumpForce;

    public float CheckDistance;
    public Transform GroundCheck;
    public LayerMask GroundMask;

    public Transform PlayerMesh;

    public bool canJump;
    public bool canMove;

    public float TurnSmoothTime = 0.1f;

    public float AttackCooldown = 0;
    public float AttackCancel = 0;

    public GameObject Attack1;
    public GameObject Attack2;

    private InputAction Movement;
    PlayerActions controls;

    public bool CollidingWithItem = false;
    public int WeaponActiveNum;


    #region items
    public GameObject AR;
    public GameObject BB;
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
    }

    private void OnEnable()
    {
        Movement = controls.PlayerCon.Movement;
        Movement.Enable();
    }

    private void OnDisable()
    {
        Movement.Disable();
    }

    void FixedUpdate()
    {
        Vector2 MoveVec = Movement.ReadValue<Vector2>();

        Vector2 inputVector = controls.PlayerCon.Movement.ReadValue<Vector2>();
        //Movement(new Vector3(inputVector.x, 0.0f, inputVector.y));


        Cursor.lockState = CursorLockMode.Locked;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (AttackCooldown <= 0)
        {
            //float HorizontalInput = Input.GetAxis("Horizontal");
            //float VerticalInput = Input.GetAxis("Vertical");
            float HorizontalInput = MoveVec.x;
            float VerticalInput = MoveVec.y;
            //Vector3 direction = new Vector3(HorizontalInput, 0f, VerticalInput).normalized;

            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            Vector3 MoveDirection = forward * VerticalInput + right * HorizontalInput;

            rb.velocity = new Vector3(MoveDirection.x * speed, rb.velocity.y, MoveDirection.z * speed);

            Physics.gravity = new Vector3(0, -30F, 0);

            if (MoveDirection != new Vector3(0, 0, 0))
            {
                PlayerMesh.rotation = Quaternion.LookRotation(MoveDirection);
                PlayerAnimator.SetBool("Moving", true);
            }
            else { PlayerAnimator.SetBool("Moving", false); }
        }

    }


    private void Update()
    {
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        if (AttackCancel > 0)
        {
            AttackCancel -= Time.deltaTime;
        }

        switch(WeaponActiveNum)
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
                #region Stop Sign Pizza Varient Active
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
    }

    public void attack1()
    {
        Vector3 playerPos = this.transform.position;
        Vector3 playerDirection = this.transform.forward;
        Quaternion playerRotation = this.transform.rotation;
        Vector3 spawnPos = playerPos + playerDirection * 1;

        if (GameObject.Find("AttackTest1(Clone)") == null && AttackCooldown <= 0)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Instantiate(Attack1, spawnPos, playerRotation);
            AttackCooldown = 0.5f;
            AttackCancel = 0.3f;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public void attack2()
    {
        Vector3 playerPos = this.transform.position;
        Vector3 playerDirection = this.transform.forward;
        Quaternion playerRotation = this.transform.rotation;
        Vector3 spawnPos = playerPos + playerDirection * 1;

        if (GameObject.Find("AttackTest2(Clone)") == null && AttackCooldown <= 0)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Instantiate(Attack2, spawnPos, playerRotation);
            AttackCooldown = 0.5f;
            AttackCancel = 0.3f;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    //private void OnTriggerStay(Collider collision)
    //{
    //    if (collision.tag == "ItemHitBox")
    //    {
    //        CollidingWithItem = true;
    //    }
    //    else 
    //    {
    //        CollidingWithItem = false;
    //    }
    //}

            public void Move(Vector2 direction)
    {

    }
}
