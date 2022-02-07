using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //float TurnSmoothVelocity;

    public float AttackCooldown = 0;
    public float AttackCancel = 0;

    public GameObject Attack1;

    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Rigidbody rb = GetComponent<Rigidbody>();

        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(HorizontalInput, 0f, VerticalInput).normalized;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 MoveDirection = forward * VerticalInput + right * HorizontalInput;

        rb.velocity = new Vector3(MoveDirection.x * speed, rb.velocity.y, MoveDirection.z * speed);

        //float TargetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;

        Physics.gravity = new Vector3(0, -30F, 0);

        if (MoveDirection != new Vector3(0, 0, 0))
        {
            //float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothTime);

            PlayerMesh.rotation = Quaternion.LookRotation(MoveDirection);
        }
    }

    private void Update()
    {
        Vector3 playerPos = this.transform.position;
        Vector3 playerDirection = this.transform.forward;
        Quaternion playerRotation = this.transform.rotation;
        Vector3 spawnPos = playerPos + playerDirection * 1;

        if (Input.GetKeyDown("space") && GameObject.Find("AttackTest(Clone)") == null && AttackCooldown <= 0)
        {
            Instantiate(Attack1, spawnPos, playerRotation);
            AttackCooldown = 0.5f;
            AttackCancel = 0.3f;
        }

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        if (AttackCancel > 0)
        {
            AttackCancel -= Time.deltaTime;
        }
    }
}
