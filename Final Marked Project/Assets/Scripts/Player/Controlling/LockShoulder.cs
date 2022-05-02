using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using Cinemachine;


public class LockShoulder : MonoBehaviour
{
    // initialise variables
    public CinemachineVirtualCamera vc;
    public Transform LeftShoulder;
    public Transform RightShoulder;
    public bool Shoulder = true;
    bool switched = false;
    float reset = 0f;

    void Update()
    {
        // set which side the camera is on
        if (Shoulder == true)
        {
            vc.Follow = LeftShoulder;
        }

        if (Shoulder == false)
        {
            vc.Follow = RightShoulder;
        }

        // lessen reset timer
        reset -= Time.deltaTime;

        // if timer is zero or less, you can switch again
        if (reset <= 0)
        {
            switched = false;
        }
    }

    public void SwitchCamera()
    {
        // checks which side and changes camera side
        if (Shoulder == true && switched == false)
        {
            Shoulder = false;
            switched = true;
            reset = 1f;
        }

        // checks which side and changes camera side
        if (Shoulder == false && switched == false)
        {
            Shoulder = true;
            switched = true;
            reset = 1f;
        }
    }
}


