using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController PlayerController;
    public Transform Cam;

    public float PlayerSpeed = 6f;

    public float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // horizontal input
        float v = Input.GetAxisRaw("Vertical"); // vertical input
        Vector3 direction = new Vector3(h, 0f, v).normalized; // the input direction

        if (direction.magnitude >= 0.1f) // if any inputs are more then 0.1
        {
            float TargetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y; // get the target movement angle (where the player should face)
            float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothTime); // smoothing the rotation to the target angle
            transform.rotation = Quaternion.Euler(0f, Angle, 0f); //rotate to the appropiate angle

            Vector3 MoveDir = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward; // getting camera pos from target angles for movement
            PlayerController.Move(MoveDir.normalized * PlayerSpeed * Time.deltaTime); // move player in appropiate direction
        }
    }
}
