using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //public CharacterController PlayerController;
    public Transform Cam;
    public Rigidbody PlayerBody;

    public bool Attacking = false;

    public float PlayerSpeed = 6f;

    public float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // horizontal input
        float v = Input.GetAxisRaw("Vertical"); // vertical input
        Vector3 direction = new Vector3(h, 0f, v).normalized; // the input direction

        if (direction.magnitude >= 0.1f && Attacking == false) // if any inputs are more then 0.1
        {
            ////character controller
            float TargetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y; // get the target movement angle (where the player should face)
            float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothTime); // smoothing the rotation to the target angle
            transform.rotation = Quaternion.Euler(0f, Angle, 0f); //rotate to the appropiate angle

            Vector3 MoveDir = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward; // getting camera pos from target angles for movement

            //rigidbody
            Vector3 MoveVector = transform.TransformDirection(direction) * PlayerSpeed;
            PlayerBody.velocity = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward * PlayerSpeed;

            //PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);


            //Vector3 MoveDir = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward; // getting camera pos from target angles for movement


            //PlayerController.Move(MoveDir.normalized * PlayerSpeed * Time.deltaTime); // move player in appropiate direction
        }
        else
        {
            Vector3 VelocityChange = PlayerBody.velocity;
            VelocityChange.x = 0f;
            VelocityChange.z = 0f;
            PlayerBody.velocity = VelocityChange;
        }
    }
}
