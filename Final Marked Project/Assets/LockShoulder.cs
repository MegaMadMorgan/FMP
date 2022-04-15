using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using Cinemachine;


public class LockShoulder : MonoBehaviour
{
    public CinemachineVirtualCamera vc;
    public Transform LeftShoulder;
    public Transform RightShoulder;
    public bool Shoulder = true;
    bool switched = false;
    float reset = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Shoulder == true)
        {
            vc.Follow = LeftShoulder;
        }

        if (Shoulder == false)
        {
            vc.Follow = RightShoulder;
        }

        reset -= Time.deltaTime;

        if (reset <= 0)
        {
            switched = false;
        }
    }

    public void SwitchCamera()
    {
        if (Shoulder == true && switched == false)
        {
            Shoulder = false;
            switched = true;
            reset = 1f;
        }

        if (Shoulder == false && switched == false)
        {
            Shoulder = true;
            switched = true;
            reset = 1f;
        }
        Debug.Log("Switch");
    }
}


