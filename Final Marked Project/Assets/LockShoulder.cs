using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockShoulder : MonoBehaviour
{
    public CinemachineVirtualCamera vc;
    public Transform LeftShoulder;
    public Transform RightShoulder;
    public bool Shoulder = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
    }
}
