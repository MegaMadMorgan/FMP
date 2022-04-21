using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseSensitivity : MonoBehaviour
{
    public CinemachineFreeLook cineCam;
    public float Sensitivity;
    public GameObject pause;

    public void Start()
    {
        Sensitivity = OptionsMenu.MosSen;
    }

    void FixedUpdate()
    {
        Sensitivity = OptionsMenu.MosSen;
        cineCam.m_XAxis.m_MaxSpeed = 2 * Sensitivity; 
        cineCam.m_YAxis.m_MaxSpeed = Sensitivity / 33;
    }
}
