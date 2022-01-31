using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform Cam;

    public float speed;
    public float JumpForce;

    [Space(15)]
    public float CheckDistance;
    public Transform GroundCheck;
    public LayerMask GroundMask;

    [Space(15)]
    public Transform PlayerMesh;

    [Space(15)]
    public bool canJump;
    public bool canMove;

    public float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;

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

        if (MoveDirection != new Vector3(0, 0, 0))
        {
            //float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothTime);

            PlayerMesh.rotation = Quaternion.LookRotation(MoveDirection);
        }
    }
}
