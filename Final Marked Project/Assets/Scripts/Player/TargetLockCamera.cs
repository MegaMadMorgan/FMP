using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLockCamera : MonoBehaviour
{
    public bool targetLockCam;
    public CinemachineVirtualCameraBase freeCam;
    public CinemachineVirtualCamera zTargetCam;
    public CinemachineFreeLook freeLook;

    private void Awake()
    {
        freeLook = freeCam.gameObject.GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        if (targetLockCam)
        {
            freeCam.gameObject.SetActive(false);
            freeLook.m_RecenterToTargetHeading.m_enabled = true;
            zTargetCam.gameObject.SetActive(true);
        }

        else if (!targetLockCam)
        {
            freeCam.gameObject.SetActive(true);
            freeLook.m_RecenterToTargetHeading.m_enabled = false;
            zTargetCam.gameObject.SetActive(false);
        }
    }
}
