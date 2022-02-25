using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform Cam;

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
    //[Space] [SerializeField] private InputActionAsset playerControls;
    PlayerActions controls;
    private void Awake()
    {
        controls = new PlayerActions();
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
        Debug.Log("Movement Values " + Movement.ReadValue<Vector2>());
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
            }
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

    public void Move(Vector2 direction)
    {

    }
}
